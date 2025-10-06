using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Independent : MonoBehaviour//À­¸Ë
{
    public Animator animator;
    public bool Active = false;
    public bool Shadowed=false;
    private void Update()
    {
        if (Active)
        {
            animator.SetBool("S", true);
        }
        else
        {
            animator.SetBool("S", false);
        }
    }
}
