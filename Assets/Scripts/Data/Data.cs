using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Data
{
    public class shadow
    {
        public enum Type
        {
            block, controller, soldier
        };
        public Vector3Int place;
        public Vector3 worldPlace;
        public Type type;
        public controllerWithKey controller;
        public shadow(Vector3Int _place, Type _type, Tilemap tilemap)
        {
            place = _place;
            type = _type;
            worldPlace = tilemap.CellToWorld(_place);
        }//block
        public shadow(Type _type, ControllerSystem.Controller _controller,int num)
        {
            controller = new controllerWithKey(keys[num],_controller,num);
            worldPlace = _controller.independent.transform.position;
            type = _type;
        }//controller
    }
    public class controllerWithKey
    {
        public controllerWithKey(KeyCode _key,ControllerSystem.Controller _cont,int _num)
        {
            key = _key;
            controller = _cont;
            num=_num;
            Debug.Log($"Controller assigned to key {key}");
        }
        public int num;
        public KeyCode key;
        public ControllerSystem.Controller controller;
    }
    public static KeyCode[] keys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
    public static bool[] occupied = new bool[9];
    public static int maxShadows = 1;
    public static int maxPieces = 4;
    public static int currentShadows = 1;
    public static int currentPieces = 3;
    public static void AddPiece()
    {
        currentPieces++;
        if(currentPieces == maxPieces)
        {
            maxShadows++;
            currentShadows++;
            currentPieces = 0;
        }
    }
    public static bool
        enablePlacingBlocks = true,
        enableSummonController = true,
        enableSummonSoldier = true;
    public static List<shadow> shadows = new List<shadow>();
    public static void Restart()
    {
        shadows.Clear();
        currentShadows = maxShadows;
        //shadows.RemoveAt(1);
    }
    public static shadow NearestShadow(Vector3 pos, Tilemap tilemap)
    {
        if (shadows.Count == 0) return null;
        shadow nearest = shadows[0];
        float minDist = Mathf.Infinity;
        foreach (var s in shadows)
        {
            float dist = Vector3.Distance(pos, s.worldPlace);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = s;
            }
        }
        return nearest;
    }
}
