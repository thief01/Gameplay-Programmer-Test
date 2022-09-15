using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float forceMultipliter;
    [SerializeField] private float rotationSpeed;
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void Move(Vector2 direction)
    {
        float rotation = direction.x * rotationSpeed;
        transform.eulerAngles += new Vector3(0, 0, rotation);
        
        float force = direction.y * forceMultipliter * Time.deltaTime;

        Vector3 flyingDirection = Vector3.zero;
        if (force < 0)
        {
            flyingDirection = transform.right;
        }
        else if(force > 0)
        {
            flyingDirection = -transform.right;
        }

        flyingDirection *= maxSpeed * Time.deltaTime;
        
        Vector2 velocity = rigidbody2D.velocity;
        if (((Vector2)flyingDirection + velocity).magnitude <= maxSpeed)
        {
            rigidbody2D.velocity += (Vector2)flyingDirection;
        }
    }
}
