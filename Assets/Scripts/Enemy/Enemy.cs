using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    public Rigidbody2D rb;
    public float speed = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    float mutiplyer = 0;

    void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 15)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    mutiplyer = -1;
                }
                else
                {
                    mutiplyer = 1;
                }
            }
        }
        Vector2 nextPos = new Vector2(transform.position.x + mutiplyer * 0.7f, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(nextPos, Vector2.down, 1f);

        if (hit.collider == null || hit.rigidbody == null)
            mutiplyer = 0;
        rb.velocity = new Vector2(mutiplyer*speed, -1);
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
