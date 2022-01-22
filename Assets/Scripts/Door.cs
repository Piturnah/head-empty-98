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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            roomInfo.CompletedRoom();
        }

        if (roomInfo.completed)
        {
            if (Time.time - instantiatedTime >= timeTilDoorsOpen)
            {
                opened = true;
                gameObject.GetComponent<MeshRenderer>().material = openedMat;
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
