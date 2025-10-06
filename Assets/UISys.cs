using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISys : MonoBehaviour
{
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
        yield return null;
        Data.UIRefreshNeeded = false;
    }
}
