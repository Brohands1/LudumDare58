using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;   // assign in Inspector
    public float bulletSpeed = 500000f;   // make it faster
    public Transform firePoint;       // optional, for gun tip (assign if you have one)

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Get mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Ensure z = 0 (for 2D)
        mousePos.z = 0f;

        // Where the bullet starts (player center or firePoint)
        Vector3 startPos = firePoint ? firePoint.position : transform.position;

        // Direction from start to cursor
        Vector2 direction = (mousePos - startPos).normalized;
        startPos = transform.position + (Vector3)(direction * 0.5f);


        // Create bullet
        GameObject bullet = Instantiate(bulletPrefab, startPos, Quaternion.identity);

        // Get rigidbody and apply velocity
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
       
        rb.velocity = direction * bulletSpeed;
        Debug.Log(rb.velocity);
    }
}
