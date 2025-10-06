using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlatorm : MonoBehaviour
{
    public float lifeTime = 3f;
    private float currentLifeTime = 0f;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        currentLifeTime += Time.deltaTime;
        
        if (currentLifeTime>lifeTime) Destroy(gameObject);
    }
}
