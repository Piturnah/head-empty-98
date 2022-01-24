using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControl : MonoBehaviour
{
    private float explosionTime = 0.25f;
    private float speed = 40f;

    private void Update()
    {
        if (explosionTime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localScale += Vector3.one * speed * Time.deltaTime;
            explosionTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerManagement>().TakeDamage();
        }
    }
}
