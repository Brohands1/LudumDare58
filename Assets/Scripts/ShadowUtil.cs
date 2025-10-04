using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static ControllerSystem;

public class ShadowUtil : MonoBehaviour
{
    [Header("Keys")]
    public KeyCode placeBlockKey = KeyCode.E;
    public KeyCode removeBlockKey = KeyCode.R;
    public static KeyCode summonControllerKey = KeyCode.LeftControl;
    [Header("Others")]
    public Tilemap tilemap;
    public TileBase tile;
    public float maxRemoveDistance = 5f;
    void Update()
    {
        if (Data.currentShadows > 1)
        {
            if (Input.GetKeyDown(placeBlockKey) && Data.enablePlacingBlocks)
            {
                createBlock();
            }
            if (Input.GetKeyDown(summonControllerKey) && Data.enableSummonController)
            {
                createController();
            }

        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach(var shadow in Data.shadows)
            {
                if(shadow.type==Data.shadow.Type.controller&&shadow.controller.key==KeyCode.Alpha1)
                {
                    shadow.controller.controller.independent.Active = !shadow.controller.controller.independent.Active;
                    shadow.controller.controller.dependent.changeTo(shadow.controller.controller.independent.Active);
                    break;
                }
            }
        }
        if (Input.GetKeyDown(removeBlockKey))
        {
            RemoveShadow();
        }
        
    }
    void createBlock()
    {
        Vector3 temp = transform.position;
        Vector3Int cellPosition = tilemap.WorldToCell(temp);
        cellPosition.y--;
        if (!tilemap.HasTile(cellPosition))
        {
            //·ÅÖÃ·½¿éÂß¼­
            tilemap.SetTile(cellPosition, tile);
            Data.shadows.Add(new Data.shadow(cellPosition, Data.shadow.Type.block,tilemap));
            Debug.Log($"Create block shadow{cellPosition}");
            Data.currentShadows--;
        }
    }
    void createController()
    {
        
    }
    void RemoveShadow()
    {
        Data.shadow nearest = Data.NearestShadow(transform.position, tilemap);
        if (nearest != null&& Vector3.Distance(transform.position, nearest.worldPlace)<maxRemoveDistance)
        {
            switch(nearest.type)
            {
                case Data.shadow.Type.controller:
                {
                    Data.occupied[nearest.controller.num] = false;
                    nearest.controller.controller.independent.Shadowed=false;
                    Debug.Log("Remove controller shadow");
                    break;
                }
                case Data.shadow.Type.soldier:
                    return;
                case Data.shadow.Type.block:
                {
                    tilemap.SetTile(nearest.place, null);
                    break;
                }
            }
            Data.shadows.Remove(nearest);
            Debug.Log(Data.shadows.Count);
            Data.currentShadows++;
        }
    }
}