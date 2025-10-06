using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTrigger2 : MonoBehaviour
{
    public GameObject panel;
    public GameObject Aim;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Data.enableSummonSoldier = true;
        
        panel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        panel.SetActive(false);
        Destroy(panel);
        Destroy(gameObject);
    }
}
