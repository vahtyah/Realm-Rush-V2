using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

    Node startNode;
    Node destinateNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    [SerializeField] Node node;

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grids = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager) grids = gridManager.Grids;
    }

    private void Start()
    {
        startNode = gridManager.Grids[startCoordinates];
        destinateNode = gridManager.Grids[destinationCoordinates];
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        gridManager.ResetNode();
        BreadthFirstSearch();
        return BuildPath();
    }

    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;
            if (grids.ContainsKey(neighborCoords))
                neighbors.Add(grids[neighborCoords]);
        }

        foreach (Node neighbor in neighbors)
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                //print(neighbor.coordinates + " " + neighbor.isWalkable + "Explore");
                print("Explo");
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
    }

    private void BreadthFirstSearch()
    {
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;
        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode.coordinates == destinationCoordinates) isRunning = false;
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node curentNode = destinateNode;

        path.Add(curentNode);
        curentNode.isPath = true;

        while (curentNode.connectedTo != null)
        {
            curentNode = curentNode.connectedTo;
            path.Add(curentNode);
            curentNode.isPath = true;
        }

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grids.ContainsKey(coordinates))
        {
            bool previousState = grids[coordinates].isWalkable;

            grids[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grids[coordinates].isWalkable = previousState;
            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }
}
