using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;   // assign in Inspector
    public float bulletForce = 20f;   // ������С�������ʼΪ10������Inspector����
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
        // ��ȡ����������е�λ�ã�zΪ������������ľ���
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mousePos.z = 0f;

        // �ӵ����
        Vector3 startPos = firePoint ? firePoint.position : transform.position;

        // ���㷽��
        Vector2 direction = (mousePos - startPos).normalized;

        // �����ӵ�
        GameObject bullet = Instantiate(bulletPrefab, startPos, Quaternion.identity);

        // ��ӳ���
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    }
}
