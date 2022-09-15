using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float LIFE_TIME = 3;
    
    [SerializeField] private float speed;
    
    private Rigidbody2D rigidbody2D;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void OnCollisionEnter(Collision col)
    {
        DestroyPooledObject();
    }

    public void InitBullet()
    {
        rigidbody2D.velocity = transform.right * speed;
        StartCoroutine(LifeTime());
    }

    public void DestroyPooledObject()
    {
        StopAllCoroutines();
        rigidbody2D.velocity = Vector2.zero;
        Pool<Bullet>.Instance.BackToPool(this);
    }

    private IEnumerator LifeTime()
    {
        float delta = 0;
        while (delta < LIFE_TIME)
        {
            yield return null;
            delta += Time.deltaTime;
        }
        DestroyPooledObject();
    }
}
