/*
Name: Maciej Majchrzak
G-Number: G00332746
Reference: https://unity3d.com/learn/tutorials/s/procedural-cave-generation-tutorial
NOTE: This was developed by following one of the tutorial videos from the above link
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapGenerator : MonoBehaviour {
    //  Variables
    public int width;   //  width of the map
    public int height;  //  height of the map

    public string seed; //  seed
    public bool useRandomSeed;  //  generate random seed each time

    [Range(0, 100)] //  in the range of 1 to 100
    public int randomFillPercent;   //  filling the map

    // Tile = 0 means empty tile, tile = 1 means wall tile
    int[,] map; //  2D array of integers - grid of integers

    void Start() {
        GenerateMap();  //  generate map
    }   //  Start()

    void Update() {
        //  Everytime we press left mouse button we generate a new map
        if (Input.GetMouseButtonDown(0))
            GenerateMap();
    }   //  Update()

    //  Generates the map
    void GenerateMap() {
        map = new int[width, height];   //  size of the map
        RandomFillMap();    //  call fill map

        //  Smoothing iterations
        for (int i = 0; i < 5; i++)
            SmoothMap();

        //  Checking regions
        ProcessMap();

        //  Creating a border around the map
        int borderSize = 1;
        int[,] borderedMap = new int[width + borderSize * 2, height + borderSize * 2];  //  border of the map

        //  Loop through all elements in the borderedMap
        for (int x = 0; x < borderedMap.GetLength(0); x++) {
            for (int y = 0; y < borderedMap.GetLength(1); y++) {
                //  Check what's inside the map
                if (x >= borderSize && x < width + borderSize && y >= borderSize && y < height + borderSize)
                    borderedMap[x, y] = map[x - borderSize, y - borderSize];    //  set bordered map
                else
                    borderedMap[x, y] = 1;  //  if inside bordered area
            }   //  inner for
        }   //  for

        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        meshGen.GenerateMesh(borderedMap, 1);
    }   //  GenerateMap()

    //  Process map, removing unnecessary walls and rooms
    void ProcessMap() {
        //  Removing walls
        List<List<Coord>> wallRegions = GetRegions(1);  //  list of coordinates with type of region, wall
        int wallThresholdSize = 50; //  any wall with less than 50 tiles will get removed

        foreach (List<Coord> wallRegion in wallRegions) {   //   go through regions
            if (wallRegion.Count < wallThresholdSize) { //  check if more than 50 wall tiles
                foreach (Coord tile in wallRegion) {    //  check each tile in that region
                    map[tile.tileX, tile.tileY] = 0;    //  set to empty space
                }   //  foreach
            }   //  if
        }   //  foreach

        //  Removing rooms
        List<List<Coord>> roomRegions = GetRegions(0);  //  0 for rooms
        int roomThresholdSize = 50; //  rooms threshold
        List<Room> survivingRooms = new List<Room>();

        foreach (List<Coord> roomRegion in roomRegions) {
            if (roomRegion.Count < roomThresholdSize) {
                foreach (Coord tile in roomRegion) {
                    map[tile.tileX, tile.tileY] = 1;    //  set to wall
                }   //  foreach
            }   //  if
            else {
                survivingRooms.Add(new Room(roomRegion, map));
            }   //  else
        }   //  foreach

        ConnectClosestRooms(survivingRooms);
    }   //  ProcessMap()

    //  Find and connect closest rooms
    void ConnectClosestRooms(List<Room> allRooms) {
        //  Variables
        int bestDistance = 0;   //  best distance between the rooms

        //  Which tiles resulted in best distance
        Coord bestTileA = new Coord();
        Coord bestTileB = new Coord();

        //  Which rooms tiles come from
        Room bestRoomA = new Room();
        Room bestRoomB = new Room();

        //  Found possible connection
        bool possibleConnectionFound = false;

        //  Go through all of the rooms
        foreach (Room roomA in allRooms) {
            possibleConnectionFound = false;    //  reset the value of possible connection

            foreach (Room roomB in allRooms) {
                if (roomA == roomB)
                    continue;   //  skip to the next b
                if (roomA.IsConnected(roomB)) {
                    possibleConnectionFound = false;
                    break;  //  go to the next room a
                }   //  if

                //  Checking the distance between the rooms
                for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++) {
                    for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB++) {
                        Coord tileA = roomA.edgeTiles[tileIndexA];
                        Coord tileB = roomB.edgeTiles[tileIndexB];

                        //  Calculate the distance between the rooms
                        int distanceBetweenRooms = (int)(Mathf.Pow(tileA.tileX - tileB.tileX, 2) + Mathf.Pow(tileA.tileY - tileB.tileY, 2));

                        //  Check the shortest route
                        if (distanceBetweenRooms < bestDistance || !possibleConnectionFound) {
                            bestDistance = distanceBetweenRooms;
                            possibleConnectionFound = true; //  found possible connection

                            bestTileA = tileA;
                            bestTileB = tileB;

                            bestRoomA = roomA;
                            bestRoomB = roomB;
                        }   //  if
                    }   // inner for
                }   //  for
            }   //  foreach

            if (possibleConnectionFound)    //  if found possible connection
                CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);  //  create passage
        }   //  foreach
    }   //  ConnectClosestRooms()

    //  Create passage between rooms
    void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB) {
        Room.ConnectRooms(roomA, roomB);    //  mark as connected
        Debug.DrawLine(CoordToWorldPoint(tileA), CoordToWorldPoint(tileB), Color.green, 100);   //  draw debug line
    }   //  CreatePassage()

    //  Returns world position
    Vector3 CoordToWorldPoint(Coord tile) {
        // Far left of the map for x, rise for y
        return new Vector3(-width / 2 + .5f + tile.tileX, 2, -height / 2 + .5f + tile.tileY);
    }   //  CoordToWorldPoint()

    //  Return list of regions of certain tileType
    List<List<Coord>> GetRegions(int tileType) {
        List<List<Coord>> regions = new List<List<Coord>>();
        int[,] mapFlags = new int[width, height];   //  to check if already checked

        //  Loop through the map
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (mapFlags[x, y] == 0 && map[x, y] == tileType) { //  if not checked && right tile type
                    List<Coord> newRegion = GetRegionTiles(x, y);   //  new list of coordinates
                    regions.Add(newRegion); //  add new region to the list of regions

                    //  Mark each tile in the region as checked
                    foreach (Coord tile in newRegion) {
                        mapFlags[tile.tileX, tile.tileY] = 1;
                    }   //  foreach()
                }   //  if
            }   //  inner for
        }   //  for

        return regions; //  return list of regions
    }   //  GetRegions()

    //  Method returning list of coordinates
    List<Coord> GetRegionTiles(int startX, int startY) {
        //  Variables
        List<Coord> tiles = new List<Coord>();  //  storing tiles
        int[,] mapFlags = new int[width, height];   //  2D integer array, already checked tiles
        int tileType = map[startX, startY]; //  type of tile, wall or empty

        Queue<Coord> queue = new Queue<Coord>();    //  store coordinates
        queue.Enqueue(new Coord(startX, startY));   //  add new coordinate, starting coordinate
        mapFlags[startX, startY] = 1;   //  looked at that tile

        while (queue.Count > 0) {   //  while something left in the queue
            Coord tile = queue.Dequeue();   //  first item in the queue, and then remove it
            tiles.Add(tile);    //  add to the tiles list

            //  Checking all the adjacent tiles
            for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++) {
                for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++) {
                    if (IsInMapRange(x, y) && (y == tile.tileY || x == tile.tileX))
                    {   //  check if the tile is within the map range && check diagonal
                        if (mapFlags[x, y] == 0 && map[x, y] == tileType) { //  check if haven't been checked && right type of tile
                            mapFlags[x, y] = 1; //  tile checked
                            queue.Enqueue(new Coord(x, y)); //  new coordinate
                        }   //  inner if
                    }   //  if
                }   //  inner for
            }   //  for
        }   //  while

        return tiles;   //  return list of coordinates
    }   //  GetRegionTiles()

    //  Check if its in the range of the map
    bool IsInMapRange(int x, int y) {
        return x >= 0 && x < width && y >= 0 && y < height;
    }   //  IsInMapRange()

    //  Fills the map
    void RandomFillMap() {
        if (useRandomSeed)
            seed = Time.time.ToString();    //  different value each time

        //  Pseudo random number generator
        System.Random pseudoRandom = new System.Random(seed.GetHashCode()); //  convert seed strin to integer

        //  Loop to go through each tile in the map
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                //  Getting rid of holes in the edges of the map
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    map[x, y] = 1;  //  wall
                else
                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;    //  if its lower than the percent then create wall, if its greater then leave it as a blank tile
            }   //  inner for
        }   //  for
    }   //  RandomFillMap()

    // Smooth the map
    void SmoothMap() {
        //  Loop through each tile
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                //  If its greater than 4, its going to be a wall
                if (neighbourWallTiles > 4)
                    map[x, y] = 1;  
                // If its less than 4, its going to be inside the map
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;

            }   //  inner for
        }   //  for
    }   //  SmoothMap()

    //  Returns number of surrounding walls
    int GetSurroundingWallCount(int gridX, int gridY) {
        int wallCount = 0;  //  count of walls

        //  Looping through three by three grid centered at gridX and gridY
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
                //  Check if inside the map
                if (IsInMapRange(neighbourX, neighbourY)) {
                    // Don't want to consider current tile(original tile)
                    //  If its a wall increase wall Count
                    if (neighbourX != gridX || neighbourY != gridY)
                        wallCount += map[neighbourX, neighbourY];
                }
                //  Outside the map, encourage the growth of walls around the edges of the map
                else
                    wallCount++;
            }   //  inner for
        }   //  for

        return wallCount;
    }   //  GetSurroundingWallCount()

    //  Coordinates struct
    struct Coord {
        //  Variables
        public int tileX;
        public int tileY;

        //  Constructor
        public Coord(int x, int y) {
            tileX = x;
            tileY = y;
        }   //  Coord()
    }   //  Coord

    //  Room class
    class Room {
        //  Variables
        public List<Coord> tiles;   //  storing all tiles in the room
        public List<Coord> edgeTiles;   //  edge of the room, so instead searching all tiles only edges of the rooms will be searched
        public List<Room> connectedRooms;   //  connected rooms  
        public int roomSize;    //  room size

        //  Empty constructor
        public Room() {}

        //  Constructor with parameters
        public Room(List<Coord> roomTiles, int[,] map) {
            tiles = roomTiles;
            roomSize = tiles.Count;
            connectedRooms = new List<Room>();

            edgeTiles = new List<Coord>();

            //  Checking if at the edge of the room
            foreach (Coord tile in tiles) { //  each of tiles in the room
                for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++) {
                    for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++) {
                        if (x == tile.tileX || y == tile.tileY) {   //  exclude diagonal
                            if (map[x, y] == 1) //  if a wall
                                edgeTiles.Add(tile);    //  add to edgeTiles list
                        }   //  if
                    }   //  inner for
                }   //  for
            }   //  foreach
        }   //  Room

        //  Adding to connected rooms list
        public static void ConnectRooms(Room roomA, Room roomB) {
            roomA.connectedRooms.Add(roomB);
            roomB.connectedRooms.Add(roomA);
        }   //  ConnectRooms()

        //  Check if connected
        public bool IsConnected(Room otherRoom) {
            return connectedRooms.Contains(otherRoom);  //  if connected connectedRooms contains the otherRoom
        }   //  IsConnected()
    }

}   //  MapGenerator