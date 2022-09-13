using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rigidbody2D;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rigidbody2D.velocity = transform.forward * speed;
        StartCoroutine(LifeTime());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        DestroyPooledObject();
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
        while (delta < 2)
        {
            yield return null;
            delta += Time.deltaTime;
        }
        DestroyPooledObject();
    }
}
