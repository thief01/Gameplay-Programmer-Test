using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ShipWeapon : MonoBehaviour
{
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private float attackSpeed = 2;
    
    private float reloadTime = 0;
    void Update()
    {
        if (reloadTime <= 0)
        {
            Fire();
        }

        reloadTime -= Time.deltaTime;
    }

    private void Fire()
    {
        reloadTime = 1/attackSpeed;
        var bullet = Pool<Bullet>.Instance.GetObject();

        bullet.transform.position = muzzlePoint.position;
        bullet.transform.rotation = muzzlePoint.rotation;
        bullet.InitBullet();
    }
}
