using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow1Script : MonoBehaviour
{
    public GameObject player;
    private ShadowCounter shadowCounter;
    public int shadowNumber;

    // Start is called before the first frame update
    void Start()
    {
        
        if (player != null)
        {
            shadowCounter = player.GetComponent<ShadowCounter>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shadowCounter != null)
        {
            if (shadowCounter.shadowCount >= shadowNumber)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
