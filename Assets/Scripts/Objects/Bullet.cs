using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float LIFE_TIME = 3;
    
    [SerializeField] private float speed;
    
    private Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(DestroyPooledObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        DestroyPooledObject();
    }

    public void InitBullet()
    {
        rigidbody.velocity = transform.right * speed;
        StartCoroutine(LifeTime());
    }

    public void DestroyPooledObject()
    {
        StopAllCoroutines();
        rigidbody.velocity = Vector2.zero;
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
