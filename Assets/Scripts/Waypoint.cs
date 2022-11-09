using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefabs;
    [SerializeField] bool isPlaceable;
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
