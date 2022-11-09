using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Node currentSearchNode;
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grids;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager) grids = gridManager.Grids;
    }

    private void Start()
    {
        ExploreNeighbors();
    }

    private void ExploreNeighbors()
    {

        List<Node> neighbors = new List<Node>();
        

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;
               print(neighborCoords);
            if (grids.ContainsKey(neighborCoords))
            {
                neighbors.Add(grids[neighborCoords]);
                grids[neighborCoords].isExplored = true;
                grids[currentSearchNode.coordinates].isPath = true;
            }
        }
    }
}
