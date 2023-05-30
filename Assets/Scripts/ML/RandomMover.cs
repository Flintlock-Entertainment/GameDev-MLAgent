using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomMover : TargetMover
{
    [SerializeField] private int limXMin;
    [SerializeField] private int limXMax;
    [SerializeField] private int limYMin;
    [SerializeField] private int limYMax;
    private void Update()
    {
        if (atTarget)
        {
            ChangeTarget();
        }
    }

    public void ChangeTarget()
    {
        Vector3 newTarget;
        TileBase tile;
        do
        {
            newTarget = new Vector3(Random.Range(limXMin, limXMax), Random.Range(limYMin, limYMax), 0);
            newTarget += transform.GetComponentInParent<EnvController>().GetFloorPosition();
            var location = tilemap.WorldToCell(newTarget);
            tile = tilemap.GetTile(location);
        } while (!allowedTiles.Contain(tile));
        SetTarget(newTarget);
    }
}
