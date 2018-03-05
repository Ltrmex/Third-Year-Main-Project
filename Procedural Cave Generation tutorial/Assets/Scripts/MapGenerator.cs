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

        foreach (List<Coord> roomRegion in roomRegions) {
            if (roomRegion.Count < roomThresholdSize) {
                foreach (Coord tile in roomRegion) {
                    map[tile.tileX, tile.tileY] = 1;    //  set to wall
                }   //  foreach
            }   //  if
        }   //  foreach
    }   //  ProcessMap()

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

    //  Draw map
    void OnDrawGizmos() {
        //  If map isn't empty
        if (map != null) {
            //  Loop through each tile
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    //  Set color of each tile. Black for wall, white for empty
                    Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;

                    //  Vector for the tile
                    Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);

                    //  Draw gizmo at the position
                    Gizmos.DrawCube(pos, Vector3.one);
                }   //  inner for
            }   //  for
        }   //  if
    }   //  OnDrawGizmos()

}   //  MapGenerator