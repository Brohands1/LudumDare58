using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowOnMap : MonoBehaviour
{
    void Update()
    {
        if(Vector3.Distance(transform.position,GameObject.FindGameObjectWithTag("Player").transform.position) < 1f)
        {
            Data.currentShadows++;
            Data.maxShadows++;
            Destroy(gameObject);
        }
    }
}