using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTrigger : MonoBehaviour
{
    public GameObject panel;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Data.enablePlacingBlocks = true;
        panel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        panel.SetActive(false);
        //Destroy(panel);
        //Destroy(gameObject);
    }
}
