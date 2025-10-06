using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
public class SavePoint : MonoBehaviour
{
    public bool Unlcoked = false;
    void Update()
    {
        if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 3)
        {
            Touch();
        }
    }
    void Touch()
    {
        if(!Unlcoked) Unlcoked = true;
        SavePoints.currentSavePoint = this;
    }
}