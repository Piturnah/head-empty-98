using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballControl : MonoBehaviour
{
    private const float timeToWait = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("frozenControl");
        }
    }

    public IEnumerator frozenControl()
    {
        PlayerManagement.frozen = true;
        yield return new WaitForSecondsRealtime(timeToWait);
        PlayerManagement.frozen = false;
    }
}
