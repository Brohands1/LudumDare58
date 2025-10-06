using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowOnMap : MonoBehaviour
{
    public GameObject playerObject;
    private ShadowCounter ShadowCounter;
    void Start()
    {
        ShadowCounter = playerObject.GetComponent<ShadowCounter>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 0.5f)
        {
            Data.AddPiece();
            Destroy(gameObject);
            ShadowCounter.shadowCount += 1;
            // Fix: Access the shadowCount field through an instance of ShadowCounter

        }
    }
}
