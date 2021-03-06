using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = System.Random;

public class Controller : MonoBehaviour
{
    Assets assets;
 
    // seed: [fromDoorIndex, parentSeed, index]
    static Dictionary<int, int[]> rooms = new Dictionary<int, int[]>();
    static Dictionary<int, bool> roomComplete = new Dictionary<int, bool>();

    private void Start()
    {
        Random r = new Random();
        int initialBeforeSeed = r.Next() | 1;
        int initialSeed = Room.GenerateChildSeed(initialBeforeSeed, 0);
        VisitedRoom(initialBeforeSeed, 2, initialSeed);
        VisitedRoom(initialSeed, 0, initialBeforeSeed);

        assets = FindObjectOfType<Assets>();
        RoomObj newRoom = Instantiate(assets.roomPrefab).GetComponent<RoomObj>();
        newRoom.setRoomObj(new Room(initialSeed, initialBeforeSeed, 2));

        levelIsOver = false;
    }

    public static int[] GetFromInfo(int seed)
    {
        return rooms[seed].ToArray();
    }

    public static void VisitedRoom(int newRoomSeed, int fromDoor, int parentSeed)
    {
        rooms[newRoomSeed] = new int[] { (fromDoor + 2) % 4, parentSeed, rooms.Count() };
    }

    public static void CompleteRoom(int seed)
    {
        roomComplete[seed] = true;
    }

    public static bool IsRoomComplete(int seed)
    {
        return roomComplete.TryGetValue(seed, out bool value);
    }

    // Game Managment
    public static bool levelIsOver;

    public static void GameLost()
    {
        GameFinished();
        Application.Quit();
    }

    static void GameFinished()
    {
        levelIsOver = false;
    }
}
