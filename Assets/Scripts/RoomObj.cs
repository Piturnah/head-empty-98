using System.Collections;
using UnityEngine;
using Random = System.Random;

public class Room
{
    Random rand;

    // Last byte used for room info
    public int seed;

    // info about the *from room used to backtrack to previous rooms*
    public int[] fromRoomInfo;
    public bool completed;

    public bool[] doors = new bool[4];

    public int roomType;

    // Generate a new seed taking the door that the room is being entered from
    public static int GenerateChildSeed(int seed, int fromDoorIndex)
    {
        int enabledDoor = (int)Mathf.Pow(2, (fromDoorIndex + 2) % 4);
        Random r = new Random(seed + fromDoorIndex);
        return r.Next() | enabledDoor;
    }

    public bool DoorEnabled(int index)
    {
        return (seed >> index) % 2 == 1;
    }

    public void CompletedRoom()
    {
        Controller.CompleteRoom(seed);
        completed = true;
    }

    public Room(int seed, int parentSeed, int fromDoorIndex)
    {
        this.seed = seed;

        BitArray bits = new BitArray(new int[] { seed });
        for (int i = 0; i < 4; i++)
        {
            doors[i] = bits[i];
        }

        fromRoomInfo = Controller.GetFromInfo(this.seed);
        completed = Controller.IsRoomComplete(this.seed);
        rand = new Random(seed);
        roomType = rand.Next();
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
        if (doorIndex == room.fromRoomInfo[0]) // Going back
        {
            newRoom.setRoomObj(new Room(room.fromRoomInfo[1], room.seed, doorIndex));
        } else
        {
            int newSeed = Room.GenerateChildSeed(room.seed, doorIndex);
            Controller.VisitedRoom(newSeed, doorIndex, room.seed);
            newRoom.setRoomObj(new Room(newSeed, room.seed, doorIndex));
        }
        Vector3 offsetDir = new Vector3();
        offsetDir += Vector3.forward * (doorIndex == 0 || doorIndex == 1 ? 1 : -1);
        offsetDir += Vector3.right * (doorIndex == 1 || doorIndex == 2 ? 1 : -1);
        newRoom.transform.position = transform.position + offsetDir.normalized * roomWidth * 2;
        Destroy(gameObject);
    }

    public void setRoomObj(Room room)
    {
        assets = FindObjectOfType<Assets>();
        this.room = room;

        foreach (Transform wall in transform.Find("Walls").transform)
        {
            wall.GetComponent<MeshFilter>().mesh = assets.roomTypes[room.roomType % assets.roomTypes.Length].wallMesh;
            wall.GetComponent<MeshRenderer>().materials = assets.roomTypes[room.roomType % assets.roomTypes.Length].wallMaterials;
        }
    }
    public Room getRoom()
    {
        return room;
    }
}
