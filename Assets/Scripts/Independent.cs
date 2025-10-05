using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Independent : MonoBehaviour//À­¸Ë
{
    public SpriteRenderer sr;
    public bool Active = false;
    public bool Shadowed=false;
    private void Update()
    {
        if (Active)
        {
            sr.color = new Color(1, 1, 1, 1);
        }
        else
        {
            sr.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
