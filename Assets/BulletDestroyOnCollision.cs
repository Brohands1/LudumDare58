using UnityEngine;
using System.Collections;

public class BulletDestroyOnCollision : MonoBehaviour
{
    private Vector3 dir;
    public float speed = 13f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") Destroy(collision.gameObject);
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.position += dir * Time.deltaTime * speed;
    }
    public void Init(Vector3 _in)
    {
        dir= _in;
    }

}
