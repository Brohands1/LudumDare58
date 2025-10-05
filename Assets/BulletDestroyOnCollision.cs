using UnityEngine;

public class BulletDestroyOnCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy bullet when it hits *anything*
        Destroy(gameObject);
    }

    // Optional: also destroy on trigger (in case your bullet uses triggers)
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
