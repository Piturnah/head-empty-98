using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAttack : MonoBehaviour
{
    private static RifleAttack _instance;
    public Transform firingPoint;
    public float bulletSpeed = 5f;

    private void Start()
    {
        _instance = this;
    }

    public static void Fire()
    {
        AudioManager.Play("LaserRifleShot");
        ((GameObject)Instantiate(Resources.Load("Bullet"), 
            _instance.firingPoint.position, 
            Quaternion.LookRotation(_instance.firingPoint.forward) * Quaternion.Euler(90,0,0))).GetComponent<BulletControl>().velocity = 
            _instance.firingPoint.forward * _instance.bulletSpeed;
    }

    private void OnDrawGizmos()
    {
        if(firingPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(firingPoint.position, 0.05f);
        }
    }
}
