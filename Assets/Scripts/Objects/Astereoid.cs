using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Astereoid : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rigidbody2D;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        do
        {
            rigidbody2D.velocity = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * speed;
        } while (rigidbody2D.velocity.magnitude == 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        DestroyPooledObject();
    }

    public void DestroyPooledObject()
    {
        rigidbody2D.velocity = Vector2.zero;
        Pool<Astereoid>.Instance.BackToPool(this);
    }
}
