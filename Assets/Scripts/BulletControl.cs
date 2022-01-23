using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public Vector3 velocity;
    public float maxBulletTime = 5f;

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
        if(maxBulletTime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            maxBulletTime -= Time.deltaTime;
        }
    }
}
