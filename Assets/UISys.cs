using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISys : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Data.UIRefreshNeeded)
        {
            StartCoroutine(nextF());
        }
    }
    IEnumerator nextF()
    {
        yield return null;
        Data.UIRefreshNeeded = false;
    }
}
