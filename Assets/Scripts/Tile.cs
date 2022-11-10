using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefabs;
    [SerializeField] bool isPlaceable;
    [SerializeField] Node node;

    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
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
        if (isPlaceable)
        {
            bool isPlaced = towerPrefabs.CreateTower(towerPrefabs, transform.position);
            isPlaceable = !isPlaced;    
        }
    }
    public bool IsPlaceable { get => isPlaceable; }
}
