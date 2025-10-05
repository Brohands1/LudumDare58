using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dependent : MonoBehaviour//����
{
    void Start()
    {
        changeToFalse();
    }
    public void changeTo(bool state)
    {
        if (state)
        changeToTrue();
        else changeToFalse();
    }
    public virtual void changeToTrue()
    {
            return;
    }
    public virtual void changeToFalse()
    {
            return;
    }
}
