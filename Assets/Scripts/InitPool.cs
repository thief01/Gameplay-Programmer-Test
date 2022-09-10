using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private GameObject astereoidPrefab;

    private void Awake()
    {
        Pool<Bullet>.Instance.InitPool(100, bulletPrefab);
        Pool<Astereoid>.Instance.InitPool(25600, astereoidPrefab);
    }
}
