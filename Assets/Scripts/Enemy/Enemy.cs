using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    public Rigidbody2D rb;
    public float speed = 2f;
    public bool active = true;
    Vector3 Orgin;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Orgin=transform.position;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") Data.Restart();

    }
    float mutiplyer = 0;

    void Update()
    {
        if (active)
        {
            
        transform.rotation= Quaternion.identity;
        if (player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 25)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    mutiplyer = -1;
                    transform.localScale=new Vector3(-1,1,1);
                }
                else
                {
                    mutiplyer = 1;
                    transform.localScale=new Vector3(1,1,1);
                }
            }
        }
        Vector2 nextPos = new Vector2(transform.position.x + mutiplyer * 0.7f, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(nextPos, Vector2.down, 1f);

        if (hit.collider == null || hit.rigidbody == null)
            mutiplyer = 0;
        rb.velocity = new Vector2(mutiplyer*speed, -6);
        }
    }
    public void reset()
    {
        transform.position = Orgin ;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 nextPos = new Vector2(transform.position.x + mutiplyer * 0.6f, transform.position.y);

        // »­³öÌ½²âÉäÏß
        Gizmos.DrawSphere(nextPos, 0.1f);
        Gizmos.DrawLine(nextPos, nextPos + Vector2.down * 1f);
    }
}
