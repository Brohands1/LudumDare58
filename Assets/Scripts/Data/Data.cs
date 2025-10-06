using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Data
{
    public class ShadowedController
    {
        public ShadowedController(ControllerSystem.Controller _cont,int _num)
        { 
            Debug.Log($"Create shadow with key {keys[_num]}");
            key = keys[_num];
            controller = _cont;
            num = _num;
        }
        public int num;
        public KeyCode key;
        public ControllerSystem.Controller controller;
    }
    public static List<ShadowedController> shadows = new List<ShadowedController>();
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
    public static void Restart()
    {
        shadows.Clear();
        occupied = new bool[9];
        currentShadows = maxShadows;
        //shadows.RemoveAt(1);
    }
}
