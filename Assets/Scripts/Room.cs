using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = System.Random;

public class Room : MonoBehaviour
{
    // Last byte used for room info
    int seed;

    bool[] doors = new bool[4];

    // Generate a new seed taking the door that the room is being entered from
    public static int GenerateSeed(int fromDoorIndex)
    {
        int enabledDoor = (int)Mathf.Pow(2, 3 - (fromDoorIndex + 2) % 4);
        Random r = new Random();
        return r.Next() | enabledDoor;
    }

    void GenerateRoom()
    {
        BitArray bits = new BitArray(new int[] { seed });
        for (int i = 0; i < 4; i++)
        {
            doors[i] = bits[i];
        }
    }

    public void SetSeed(int seed)
    {
        this.seed = seed;
        GenerateRoom();
    }
}
