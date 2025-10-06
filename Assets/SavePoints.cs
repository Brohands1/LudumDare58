using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoints : MonoBehaviour
{
    public List<SavePoint> savePoints = new List<SavePoint>();
    public static SavePoint currentSavePoint = null;
}