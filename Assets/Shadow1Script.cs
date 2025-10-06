using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow1Script : MonoBehaviour
{
    public GameObject player;
    private ShadowCounter shadowCounter;
    public GameObject shadow1;
    public GameObject shadow2;
    public GameObject shadow3;


    // Start is called before the first frame update
    void Start()
    {
        shadowCounter = player.GetComponent<ShadowCounter>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (shadowCounter.shadowCount >= 1)
        {
            shadow1.SetActive(true);
        }
        else
        {
            shadow1.SetActive(false);
        }
        if (shadowCounter.shadowCount >= 2)
        {
            shadow2.SetActive(true);
        }
        else
        {
            shadow2.SetActive(false);
        }
        if (shadowCounter.shadowCount >= 3)
        {
            shadow3.SetActive(true);
        }
        else
        {
            shadow3.SetActive(false);
        }
    }
}
