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
    public MeshFilter walls;

    List<Vector3> vertices; //  list of Vector3
    List<int> triangles;    //  list of intergers

    //  Given set and vertex determine which vertex this triangle belongs to
    //  Dictionary takes a key and a value to get a list of triangles that vertex is a part of
    Dictionary<int, List<Triangle>> triangleDictionary = new Dictionary<int, List<Triangle>>();

    List<List<int>> outlines = new List<List<int>>();   //  storing outlines
    HashSet<int> checkedVertices = new HashSet<int>();  //   to make sure we don't have to check the same vertex again - hash set is faster for contain checks

    //  GenerateMesh method
    public void GenerateMesh(int[,] map, float squareSize) {
        triangleDictionary.Clear(); //  reset each time new gets created
        outlines.Clear();
        checkedVertices.Clear();

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

        CreateWallMesh();   //  create wall mesh
    }   //  GenerateMesh()

    //  Create wall mesh method 
    void CreateWallMesh() {

        CalculateMeshOutlines();    //  call CalculateMeshOutlines()

        List<Vector3> wallVertices = new List<Vector3>();   //  for wall vertices
        List<int> wallTriangles = new List<int>();  //  for wall of triangles
        Mesh wallMesh = new Mesh(); //  mesh   
        float wallHeight = 5;   //  height of walls

        foreach (List<int> outline in outlines) {   //  go through ever outline
            for (int i = 0; i < outline.Count - 1; i++) {   //  loop through each vertex in that outline
                int startIndex = wallVertices.Count;

                //  Add four vertices on the portion of the wall currently working on
                wallVertices.Add(vertices[outline[i]]); // left
                wallVertices.Add(vertices[outline[i + 1]]); // right
                wallVertices.Add(vertices[outline[i]] - Vector3.up * wallHeight); // bottom left
                wallVertices.Add(vertices[outline[i + 1]] - Vector3.up * wallHeight); // bottom right

                // First triangle
                wallTriangles.Add(startIndex + 0);  //  start atleft vertex
                wallTriangles.Add(startIndex + 2);  //  go to bottom left
                wallTriangles.Add(startIndex + 3);  //  go to bottom right

                //  Second Triangle
                wallTriangles.Add(startIndex + 3);  //  start at bottom right
                wallTriangles.Add(startIndex + 1);  //  go to top right
                wallTriangles.Add(startIndex + 0);  //  go to top left
            }   //  for
        }   //  foreach

        wallMesh.vertices = wallVertices.ToArray(); //  convert
        wallMesh.triangles = wallTriangles.ToArray();   //  convert 
        walls.mesh = wallMesh;  //  assign to mesh filter
    }   //  CreateWallMesh()

    //  Look through all sixteen possible cases
    void TriangulateSquare(Square square) {
        switch (square.configuration) {
            case 0:
                break;

            // 1 points:
            case 1:
                MeshFromPoints(square.centreLeft, square.centreBottom, square.bottomLeft);
                break;
            case 2:
                MeshFromPoints(square.bottomRight, square.centreBottom, square.centreRight);
                break;
            case 4:
                MeshFromPoints(square.topRight, square.centreRight, square.centreTop);
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
                //  None of these can be outside vertices
                checkedVertices.Add(square.topLeft.vertexIndex);
                checkedVertices.Add(square.topRight.vertexIndex);
                checkedVertices.Add(square.bottomRight.vertexIndex);
                checkedVertices.Add(square.bottomLeft.vertexIndex);
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

        Triangle triangle = new Triangle(a.vertexIndex, b.vertexIndex, c.vertexIndex);
        AddTriangleToDictionary(triangle.vertexIndexA, triangle);
        AddTriangleToDictionary(triangle.vertexIndexB, triangle);
        AddTriangleToDictionary(triangle.vertexIndexC, triangle);
    }   // CreateTriangle() 

    //  Adding triangle to dictionary
    void AddTriangleToDictionary(int vertexIndexKey, Triangle triangle) {
        //  Check if its already in the triangleDictionary
        if (triangleDictionary.ContainsKey(vertexIndexKey)) //  if already contains as key
            triangleDictionary[vertexIndexKey].Add(triangle);
        else {  //  else create new list of triangles
            List<Triangle> triangleList = new List<Triangle>();
            triangleList.Add(triangle);
            triangleDictionary.Add(vertexIndexKey, triangleList);
        }   //  else
    }   //  AddTriangleToDictionary()

    //  Go through every single vertex in the map and check if its outline vertex
    //  If it is its going to follow this outline all the way around until it meets itself again to add it the oulines list
    void CalculateMeshOutlines() {
        for (int vertexIndex = 0; vertexIndex < vertices.Count; vertexIndex++) {
            if (!checkedVertices.Contains(vertexIndex)) {
                int newOutlineVertex = GetConnectedOutlineVertex(vertexIndex);
                if (newOutlineVertex != -1) {   //  if one exists
                    checkedVertices.Add(vertexIndex);   //  add vertex index

                    List<int> newOutline = new List<int>(); //  for new outline
                    newOutline.Add(vertexIndex);
                    outlines.Add(newOutline);   //  add new outline to list of outlines
                    FollowOutline(newOutlineVertex, outlines.Count - 1);    //  follow outline
                    outlines[outlines.Count - 1].Add(vertexIndex);
                }   //  inner if
            }   //  if
        }   //  for
    }   //  CalculateMeshOutlines()

    //  Method for following the outline
    void FollowOutline(int vertexIndex, int outlineIndex) {
        outlines[outlineIndex].Add(vertexIndex);    //  add vertex index to the list of the outlines
        checkedVertices.Add(vertexIndex);   //   to not check the same again
        int nextVertexIndex = GetConnectedOutlineVertex(vertexIndex);   //  next vertex index

        if (nextVertexIndex != -1)  //  if not continue to follow the outline
            FollowOutline(nextVertexIndex, outlineIndex);
    }   //  FollowOutline()

    //  Find connected vertex which forms outside edge
    int GetConnectedOutlineVertex(int vertexIndex) {
        //  List of triangles which contain vertexIndex
        List<Triangle> trianglesContainingVertex = triangleDictionary[vertexIndex];

        //  Loop through each of the triangles
        for (int i = 0; i < trianglesContainingVertex.Count; i++) {
            Triangle triangle = trianglesContainingVertex[i];

            //  Look at each of the vertices
            for (int j = 0; j < 3; j++) {
                int vertexB = triangle[j];

                //  Check if they from an outside edge
                if (vertexB != vertexIndex && !checkedVertices.Contains(vertexB)) { 
                    if (IsOutlineEdge(vertexIndex, vertexB))    //  if outline edge 
                        return vertexB; //  return vertexB
                }   //  if
            }   //  inner for
        }   //  for

        return -1;  //  if not return -1
    }   //  GetConnectedOutlineVertex()

    //  Check if its outline edge or not
    //  If vertex A and vertex B share one common triangle it means it is an outside edge
    bool IsOutlineEdge(int vertexA, int vertexB) {
        //  Variables
        List<Triangle> trianglesContainingVertexA = triangleDictionary[vertexA];    //  list of triangles which vertex A belongs to
        int sharedTriangleCount = 0;

        for (int i = 0; i < trianglesContainingVertexA.Count; i++) {
            if (trianglesContainingVertexA[i].Contains(vertexB)) {  //  check
                sharedTriangleCount++;  //  increase count
                if (sharedTriangleCount > 1)    //  if shared break
                    break;
            }   //  if
        }   //  for
        return sharedTriangleCount == 1;    //  if one shared triangle return true, else false
    }   //  IsOutlineEdge()

    //  Triangle struct
    struct Triangle {
        //  Variables
        public int vertexIndexA;
        public int vertexIndexB;
        public int vertexIndexC;
        int[] vertices;

        //  Constructor
        public Triangle(int a, int b, int c) {
            vertexIndexA = a;
            vertexIndexB = b;
            vertexIndexC = c;

            vertices = new int[3];
            vertices[0] = a;
            vertices[1] = b;
            vertices[2] = c;
        }   //  Triangle()

        //  Indexer
        public int this[int i] {    //  for accessing vertices like a array
            get { return vertices[i]; }
        }

        //  Check if triangle contains certain vertexIndex
        public bool Contains(int vertexIndex) {
            return vertexIndex == vertexIndexA || vertexIndex == vertexIndexB || vertexIndex == vertexIndexC;   //  check
        }   //  Contains()
    }   //  Triangle

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