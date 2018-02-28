/*
Name: Maciej Majchrzak
G-Number: G00332746
Reference: https://unity3d.com/learn/tutorials/s/procedural-cave-generation-tutorial
NOTE: This was developed by following one of the tutorial videos from the above link
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshGenerator : MonoBehaviour {
    //  Variables
    public SquareGrid squareGrid;
    List<Vector3> vertices; //  list of Vector3
    List<int> triangles;    //  list of intergers

    //  GenerateMesh method
    public void GenerateMesh(int[,] map, float squareSize) {
        squareGrid = new SquareGrid(map, squareSize);   //  passing in map and squareSize

        vertices = new List<Vector3>();
        triangles = new List<int>();

        //  Go through each square in SquareGrid
        for (int x = 0; x < squareGrid.squares.GetLength(0); x++) {
            for (int y = 0; y < squareGrid.squares.GetLength(1); y++) {
                TriangulateSquare(squareGrid.squares[x, y]);    //  for each square TriangulateSquare
            }   //  inner for
        }   //  for

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //  Convert from list to array
        mesh.vertices = vertices.ToArray(); 
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();
    }   //  GenerateMesh()
 
    //  Look through all sixteen possible cases
    void TriangulateSquare(Square square) {
        switch (square.configuration) {
            case 0:
                break;

            // 1 points:
            case 1:
                MeshFromPoints(square.centreBottom, square.bottomLeft, square.centreLeft);
                break;
            case 2:
                MeshFromPoints(square.centreRight, square.bottomRight, square.centreBottom);
                break;
            case 4:
                MeshFromPoints(square.centreTop, square.topRight, square.centreRight);
                break;
            case 8:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreLeft);
                break;

            // 2 points:
            case 3:
                MeshFromPoints(square.centreRight, square.bottomRight, square.bottomLeft, square.centreLeft);
                break;
            case 6:
                MeshFromPoints(square.centreTop, square.topRight, square.bottomRight, square.centreBottom);
                break;
            case 9:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreBottom, square.bottomLeft);
                break;
            case 12:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreLeft);
                break;
            case 5:
                MeshFromPoints(square.centreTop, square.topRight, square.centreRight, square.centreBottom, square.bottomLeft, square.centreLeft);
                break;
            case 10:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.bottomRight, square.centreBottom, square.centreLeft);
                break;

            // 3 point:
            case 7:
                MeshFromPoints(square.centreTop, square.topRight, square.bottomRight, square.bottomLeft, square.centreLeft);
                break;
            case 11:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.bottomRight, square.bottomLeft);
                break;
            case 13:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreBottom, square.bottomLeft);
                break;
            case 14:
                MeshFromPoints(square.topLeft, square.topRight, square.bottomRight, square.centreBottom, square.centreLeft);
                break;

            // 4 point:
            case 15:
                MeshFromPoints(square.topLeft, square.topRight, square.bottomRight, square.bottomLeft);
                break;
        }   //  switch
    }   //  TriangulateSquare()

    //  Create mesh from points
    void MeshFromPoints(params Node[] points) { //  params for unknown number of points
        AssignVertices(points); //  assign vertices

        if (points.Length >= 3) //  one triangle
            CreateTriangle(points[0], points[1], points[2]);
        if (points.Length >= 4) //  means we will have two triangles
            CreateTriangle(points[0], points[2], points[3]);
        if (points.Length >= 5) //  create third triangle
            CreateTriangle(points[0], points[3], points[4]);
        if (points.Length >= 6) //  create fourth triangle
            CreateTriangle(points[0], points[4], points[5]);
    }   //  MeshFromPoints()

    //  AssignVertices method
    void AssignVertices(Node[] points) {
        for (int i = 0; i < points.Length; i++) {   //  go through all points
            if (points[i].vertexIndex == -1) {  //  see if point has vertexIndex
                points[i].vertexIndex = vertices.Count; //  if not set
                vertices.Add(points[i].position);   //  add new vertex to vertices list
            }   //  if
        }   //  for
    }   //  AssignVertices()

    //  Create triangles - mesh is structured out of group of triangles
    void CreateTriangle(Node a, Node b, Node c) {   //  triangle has 3 vertices
        //  Specify index of vertices
        triangles.Add(a.vertexIndex);
        triangles.Add(b.vertexIndex);
        triangles.Add(c.vertexIndex);
    }   // CreateTriangle() 

    //  Draw method
    void OnDrawGizmos() {
        /*
        if (squareGrid != null) {   //  if not null enter the loop
            for (int x = 0; x < squareGrid.squares.GetLength(0); x++) {
                for (int y = 0; y < squareGrid.squares.GetLength(1); y++) {

                    //  Drawing, set color depending if its active or not
                    Gizmos.color = (squareGrid.squares[x, y].topLeft.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].topLeft.position, Vector3.one * .4f);

                    Gizmos.color = (squareGrid.squares[x, y].topRight.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].topRight.position, Vector3.one * .4f);

                    Gizmos.color = (squareGrid.squares[x, y].bottomRight.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].bottomRight.position, Vector3.one * .4f);

                    Gizmos.color = (squareGrid.squares[x, y].bottomLeft.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].bottomLeft.position, Vector3.one * .4f);


                    Gizmos.color = Color.grey;
                    Gizmos.DrawCube(squareGrid.squares[x, y].centreTop.position, Vector3.one * .15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centreRight.position, Vector3.one * .15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centreBottom.position, Vector3.one * .15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centreLeft.position, Vector3.one * .15f);

                }   //  inner for
            }   //  for
        }   //  if
        */
    }   //  OnDrawGizmos()

    //  SquareGrid class - hold 2D array of squares
    public class SquareGrid {
        //  Variables
        public Square[,] squares;   //  2D array

        //  Constructor
        public SquareGrid(int[,] map, float squareSize) {
            //  Variables
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);
            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            //  2D array of ControlNodes
            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            //  Creating grid of ControlNodes
            for (int x = 0; x < nodeCountX; x++) {
                for (int y = 0; y < nodeCountY; y++) {
                    //  Calculate position
                    Vector3 pos = new Vector3(-mapWidth / 2 + x * squareSize + squareSize / 2, 0, -mapHeight / 2 + y * squareSize + squareSize / 2);
                    controlNodes[x, y] = new ControlNode(pos, map[x, y] == 1, squareSize);
                }   //  inner for
            }   //  for

            
            squares = new Square[nodeCountX - 1, nodeCountY - 1];

            //  Go through each each square
            //  Creating grid of squares out of ControlNodes
            for (int x = 0; x < nodeCountX - 1; x++) {
                for (int y = 0; y < nodeCountY - 1; y++) {
                    //  Top left, top right, bottom right, bottom left control nodes
                    squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x + 1, y + 1], controlNodes[x + 1, y], controlNodes[x, y]);
                }   //  inner for
            }   //  for

        }  //   SquareGrid() 
    }   //  SquareGrid

    //  Square class
    public class Square {
        //  Variables
        public ControlNode topLeft, topRight, bottomRight, bottomLeft;  //  reference to all four control nodes
        public Node centreTop, centreRight, centreBottom, centreLeft;   //  reference to mid point nodes
        public int configuration;

        //  Constructor
        public Square(ControlNode _topLeft, ControlNode _topRight, ControlNode _bottomRight, ControlNode _bottomLeft) {
            topLeft = _topLeft;
            topRight = _topRight;
            bottomRight = _bottomRight;
            bottomLeft = _bottomLeft;

            centreTop = topLeft.right;
            centreRight = bottomRight.above;
            centreBottom = bottomLeft.right;
            centreLeft = bottomLeft.above;

            //  Look at each of ControlNodes and check if they're active
            //  Find configuration for each square
            if (topLeft.active)
                configuration += 8; //  4th bit corresponds to 2^3 = 8
            if (topRight.active)
                configuration += 4; //  2^2 = 4
            if (bottomRight.active)
                configuration += 2; //  2^1 = 2
            if (bottomLeft.active)
                configuration += 1; //  2^0 = 1
        }   //  Square()
    }   //  Square

    //  Node class
    public class Node {
        //  Variables
        public Vector3 position;    //  track of position
        public int vertexIndex = -1;

        //  Constructor
        public Node(Vector3 _pos) {
            position = _pos;
        }   //  Node()
    }   //  Node

    //  ControlNode class
    public class ControlNode : Node {   //  inherit from node
        //  Variables
        public bool active; //  determine if its a wall or not
        public Node above, right;

        //  Constructor
        public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos) { //  inhirent _pos
            active = _active;

            //  Set nodes
            above = new Node(position + Vector3.forward * squareSize / 2f);
            right = new Node(position + Vector3.right * squareSize / 2f);
        }   //  ControlNode()

    }   //  ControlNode
}   //  MeshGenerator