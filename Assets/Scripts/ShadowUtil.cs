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
    public static float addShadowTimer = 4f;
    public float currentAddShadowTimer = 0f;
    public Transform PlatformPlace;
    public GameObject ShadowPlatform;
    void Update()
    {
        //Debug.Log(Data.currentShadows);
        if (Data.currentShadows > 1)
        {
            if (Input.GetKeyDown(placeBlockKey) && Data.enablePlacingBlocks)
            {
                createBlock();
            }
            //if (Input.GetKeyDown(summonControllerKey) && Data.enableSummonController)
            //{
            //    createController();
            //}
            
        }
        if (Data.currentShadows < Data.maxShadows)
        {
            currentAddShadowTimer += Time.deltaTime;
            //Debug.Log(currentAddShadowTimer);
            if (currentAddShadowTimer >= addShadowTimer)
            {
                Data.currentShadows++;
                currentAddShadowTimer = 0f;
                Debug.Log($"Regain a shadow, current shadows: {Data.currentShadows}");
            }
        }

        // handle numeric keys 1-9: toggle corresponding shadow with matching key
        for (int i = 1; i <= 9; i++)
        {
            KeyCode pressedKey = (KeyCode)((int)KeyCode.Alpha0 + i);
            if (Input.GetKeyDown(pressedKey))
            {
                foreach (var shadow in Data.shadows)
                {
                    if (shadow == null) continue;
                    if (shadow.key == pressedKey)
                    {
                        // safety checks before toggling
                        if (shadow.controller != null && shadow.controller.independent != null && shadow.controller.dependent != null)
                        {
                            if (Data.currentShadows > 1)
                            {
                                Data.currentShadows--;
                                shadow.controller.independent.Active = !shadow.controller.independent.Active;
                                foreach (var dep in shadow.controller.dependent)
                                {
                                    dep.changeTo(shadow.controller.independent.Active);
                                }
                            } 
                        }
                        break;
                    }
                }
            }
        }

        //if (Input.GetKeyDown(removeBlockKey))
        //{
        //    RemoveShadow();
        //}
        
    }
    void createBlock()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(PlatformPlace.position);
        if (!tilemap.HasTile(cellPosition)&&Data.currentShadows>1)
        {
            Data.currentShadows--;
            Instantiate(ShadowPlatform, PlatformPlace.position, Quaternion.identity);
        }
    }
}