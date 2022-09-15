using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Ship ship;

    private void Awake()
    {
        ship = GetComponent<Ship>();
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        Vector2 direction = new Vector2();

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        
        ship.Move(-direction);
    }
}
