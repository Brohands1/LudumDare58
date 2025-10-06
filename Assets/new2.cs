using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class new2 : Dependent
{
    public Tilemap tilemap;
    public override void changeToTrue()
    {
        tilemap.SetTile(new Vector3Int(95, 0, 0), null);
        tilemap.SetTile(new Vector3Int(96, 0, 0), null);
        tilemap.SetTile(new Vector3Int(97, 0, 0), null);
        tilemap.SetTile(new Vector3Int(98, 0, 0), null);
        tilemap.SetTile(new Vector3Int(99, 0, 0), null);
    }
}
