using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class new1 : Dependent
{
    public Tilemap tilemap;
    public override void changeToTrue()
    {
        tilemap.SetTile(new Vector3Int(95, 2, 0), null);
        tilemap.SetTile(new Vector3Int(96, 2, 0), null);
        tilemap.SetTile(new Vector3Int(97, 2, 0), null);
        tilemap.SetTile(new Vector3Int(98, 2, 0), null);
        tilemap.SetTile(new Vector3Int(99, 2, 0), null);
    }
}
