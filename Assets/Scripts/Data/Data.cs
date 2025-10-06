using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Data
{
    public static bool UIRefreshNeeded = false;
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
    public static int maxShadows = 2;
    public static int maxPieces = 4;
    public static int currentShadows = 1;
    public static int currentPieces = 3;
    public static float currentAddShadowTimer = 0f;
    public static void AddPiece()
    {
        currentPieces++;
        if(currentPieces == maxPieces)
        {
            maxShadows++;
            currentShadows++;
            currentPieces = 0;
            UIRefreshNeeded = true;
        }
    }
    public static bool
        enablePlacingBlocks = false,
        enableSummonController = false,
        enableSummonSoldier = false;
    public static void Restart()
    {
        ResetControllers();
        occupied = new bool[9];
        currentShadows = maxShadows;
        UIRefreshNeeded = true;
        currentAddShadowTimer = 0f;
        GameObject.FindGameObjectWithTag("Player").transform.position = SavePoints.currentSavePoint.transform.position;
        foreach(var flaot in GameObject.FindGameObjectsWithTag("Float"))
        {
            flaot.GetComponent<floatingPlatform>().reset();
        }
        foreach(var va in GameObject.FindGameObjectsWithTag("Controller"))
        {
            va.GetComponent<Independent>().Active=false;
        }
        foreach(var en in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            en.GetComponent<Enemy>().reset();
        }
        //shadows.RemoveAt(1);
    }
    public static void ResetControllers()
    {
        foreach (var shadow in shadows)
        {
            shadow.controller.independent.Shadowed = false;
            occupied[shadow.num] = false;
        }
        shadows.Clear();
    }
}
