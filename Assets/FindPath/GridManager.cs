using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridsSize;
    public int UnityGridsSize { get => unityGridsSize; }

    Dictionary<Vector2Int, Node> grids = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grids { get => grids; }

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grids.ContainsKey(coordinates))
            return grids[coordinates];
        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        //print(coordinates + " " + grids.ContainsKey(coordinates) + " BlockNode");
        if (grids.ContainsKey(coordinates))
        {
            grids[coordinates].isWalkable = false;
            print(grids[coordinates].coordinates + "  " + grids[coordinates].isWalkable + "BlockNode");
        }

    }

    public void ResetNode()
    {
        foreach (KeyValuePair<Vector2Int,Node> entry in grids)
        {
            entry.Value.connectedTo = null;
            entry.Value.isPath = false;
            entry.Value.isExplored = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridsSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridsSize);
        return coordinates;
    }

    public Vector3 GetPositionFormCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridsSize;
        position.z = coordinates.y * unityGridsSize;
        return position;
    }

    private void CreateGrid()
    {
        print("CreateGrid");
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grids.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}
