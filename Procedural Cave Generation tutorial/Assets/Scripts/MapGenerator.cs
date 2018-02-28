/*
Name: Maciej Majchrzak
G-Number: G00332746
Reference: https://unity3d.com/learn/tutorials/s/procedural-cave-generation-tutorial
NOTE: This was developed by following one of the tutorial videos from the above link
*/
using UnityEngine;
using System.Collections;
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

        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        meshGen.GenerateMesh(map, 1);
    }   //  GenerateMap()

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
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
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