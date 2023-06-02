using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public int xPosition;
    public int yPosition;
    public int NeighboursCount { get { return _neighbours.Count; } }

    private List<Tuple<ROOM_DIRECTIONS, DungeonRoom>> _neighbours;
    public List<Tuple<ROOM_DIRECTIONS, DungeonRoom>> Neighbours { get { return _neighbours;  } }

    public RoomTypes type = RoomTypes.INVALID;

    public DungeonRoom(int x, int y)
    {
        xPosition = x;
        yPosition = y;
        _neighbours = new List<Tuple<ROOM_DIRECTIONS, DungeonRoom>>();
    }

    public bool HasNeighbourInDirection(ROOM_DIRECTIONS direction)
    {
        foreach (Tuple<ROOM_DIRECTIONS, DungeonRoom> n in _neighbours)
        {
            if (n.Item1 == direction)
                return true;
        }
        return false;
    }

    public void AddNeighbourInDirection(DungeonRoom room, ROOM_DIRECTIONS direction)
    {
        _neighbours.Add(new Tuple<ROOM_DIRECTIONS, DungeonRoom>(direction, room));
    }
}
