using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    RoomObj room;

    private void Start()
    {
        room = GetComponentInParent<RoomObj>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            room.NewRoom(int.Parse(gameObject.name));
        }
    }
}
