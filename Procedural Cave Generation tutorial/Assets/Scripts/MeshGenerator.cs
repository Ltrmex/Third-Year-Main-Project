/*
Name: Maciej Majchrzak
G-Number: G00332746
Reference: https://unity3d.com/learn/tutorials/s/procedural-cave-generation-tutorial
NOTE: This was developed by following one of the tutorial videos from the above link
*/
using UnityEngine;
using System.Collections;

public class MeshGenerator : MonoBehaviour {
    //  Variables
    public SquareGrid squareGrid;

    //  GenerateMesh method
    public void GenerateMesh(int[,] map, float squareSize) {
        squareGrid = new SquareGrid(map, squareSize);   //  passing in map and squareSize
    }   //  GenerateMesh()

    //  Draw method
    void OnDrawGizmos() {
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