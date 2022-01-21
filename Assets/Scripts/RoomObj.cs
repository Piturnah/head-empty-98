using System.Collections;
using UnityEngine;
using Random = System.Random;

public class Room
{
    // Last byte used for room info
    public int seed;
    // Used to backtrack to previous rooms
    public Room parent;

    public bool[] doors = new bool[4];

    // Generate a new seed taking the door that the room is being entered from
    public static int GenerateSeed(int fromDoorIndex)
    {
        int enabledDoor = (int)Mathf.Pow(2, 3 - (fromDoorIndex + 2) % 4);
        Random r = new Random();
        return r.Next() | enabledDoor;
    }

    public Room(int seed, int parentSeed)
    {
        this.seed = seed;
        BitArray bits = new BitArray(new int[] { seed });
        for (int i = 0; i < 4; i++)
        {
            doors[i] = bits[i];
        }
    }
}

public class RoomObj : MonoBehaviour
{
    Assets assets;

    Room room;

    const float roomWidth = 20.58705f;

    private void Start()
    {
        assets = FindObjectOfType<Assets>();
    }

    public void NewRoom(int doorIndex)
    {
        RoomObj newRoom = Instantiate(assets.roomPrefab).GetComponent<RoomObj>();
        newRoom.setRoomObj(new Room(Room.GenerateSeed(doorIndex), room.seed));
        Vector3 offsetDir = new Vector3();
        offsetDir += Vector3.forward * (doorIndex == 0 || doorIndex == 1 ? 1 : -1);
        offsetDir += Vector3.right * (doorIndex == 1 || doorIndex == 2 ? 1 : -1);
        newRoom.transform.position = transform.position + offsetDir.normalized * roomWidth * 2;
        Destroy(gameObject);
    }

    public void setRoomObj(Room room)
    {
        this.room = room;
    }
}
