using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Astereoid : MonoBehaviour
{
    [SerializeField] private float speed;
    private SpriteRenderer spriteRenderer;
    private Vector2 velocity;
    private Rigidbody rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rigidbody2D.velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized *
                               Random.Range(0.01f, speed);
    }
    
    public void SetDirection(Vector2 direction)
    {
        rigidbody2D.velocity = direction * speed;
    }
    
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            GameManager.Instance.AddScore();
        }
        DestroyPooledObject();
    }

    public void DestroyPooledObject()
    {
        rigidbody2D.velocity = Vector2.zero;
        Pool<Astereoid>.Instance.BackToPool(this);
    }
}
