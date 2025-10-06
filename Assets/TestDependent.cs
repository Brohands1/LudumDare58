using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestDependent : Dependent
{
    public Tilemap tilemap;
    public TileBase tile;
    public override void changeToFalse()
    {
        tilemap.SetTile(new Vector3Int(0,0,0), tile);
        tilemap.SetTile(new Vector3Int(-9, 6, 0), null);
        tilemap.SetTile(new Vector3Int(-14, 6, 0), tile);
        
    }
    public override void changeToTrue()
    {
        tilemap.SetTile(new Vector3Int(0, 0, 0), null);
        tilemap.SetTile(new Vector3Int(-9, 6, 0), tile);
        tilemap.SetTile(new Vector3Int(-14, 6, 0), null);
    }
}
