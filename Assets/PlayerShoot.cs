using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;   // assign in Inspector
    public float bulletForce = 20f;   // 冲量大小，建议初始为10，可在Inspector调整
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
        // 获取鼠标在世界中的位置，z为摄像机到场景的距离
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mousePos.z = 0f;

        // 子弹起点
        Vector3 startPos = firePoint ? firePoint.position : transform.position;

        // 计算方向
        Vector2 direction = (mousePos - startPos).normalized;

        // 创建子弹
        GameObject bullet = Instantiate(bulletPrefab, startPos, Quaternion.identity);

        // 添加冲量
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    }
}
