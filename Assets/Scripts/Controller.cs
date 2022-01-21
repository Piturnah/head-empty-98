using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Assets assets;

    private void Start()
    {
        assets = FindObjectOfType<Assets>();
        RoomObj newRoom = Instantiate(assets.roomPrefab).GetComponent<RoomObj>();
        newRoom.setRoomObj(new Room(Room.GenerateSeed(0), 0));
    }
}
