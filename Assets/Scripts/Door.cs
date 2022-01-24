using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    RoomObj room;
    Room roomInfo;
    bool opened;

    float timeTilDoorsOpen = .5f;
    float instantiatedTime;

    public Animator anim;

    public Material openedMat;

    private void Start()
    {
        room = GetComponentInParent<RoomObj>();
        roomInfo = room.getRoom();
        gameObject.SetActive(roomInfo.DoorEnabled(int.Parse(gameObject.name)));
        instantiatedTime = Time.time;
    }

    private void Update()
    {
        if (roomInfo.completed)
        {
            if (Time.time - instantiatedTime >= timeTilDoorsOpen)
            {
                opened = true;
                anim.SetTrigger("open");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && opened)
        {
            room.NewRoom(int.Parse(gameObject.name));
        }
    }
}
