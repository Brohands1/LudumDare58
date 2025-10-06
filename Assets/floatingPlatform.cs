using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingPlatform : Dependent
{
    public Transform placeIn;
    Vector3 startPlace, endPlace;
    public bool goingToEnd = false;
    public float speed = 20f;
    void Start()
    {
        startPlace = transform.position;
        endPlace = placeIn.position;
    }
    public override void changeToTrue()
    {
        goingToEnd=true;
    }
    public override void changeToFalse()
    {
        goingToEnd=false;
    }
    void Update()
    {
        if (goingToEnd)
        {
            transform.position= Vector3.MoveTowards(transform.position, endPlace,speed* Time.deltaTime);
        }else transform.position= Vector3.MoveTowards(transform.position, startPlace,speed* Time.deltaTime);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
