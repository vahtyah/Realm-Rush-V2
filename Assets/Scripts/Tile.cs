using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefabs;
    [SerializeField] bool isPlaceable;
    [SerializeField] Node node;

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceable)
            {
                //print(coordinates + "Tiles");
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void Start()
    {
        node = gridManager.GetNode(coordinates);

    }
    private void OnMouseDown()
    {
        print("onmousedown");
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isPlaced = towerPrefabs.CreateTower(towerPrefabs, transform.position);
            isPlaceable = !isPlaced;
            gridManager.BlockNode(coordinates);
        }
    }
    public bool IsPlaceable { get => isPlaceable; }
}
