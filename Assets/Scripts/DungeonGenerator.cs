using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public static DungeonGenerator s;

    #region Attributes

    [Header("Seed")] 
    public string seed;
    public int currentSeed;
    const string glyphs =  "abcdefghijklmnopqrstuvwxyz0123456789";

    private int maxRooms;
    private int nCurrentRooms;

    private Queue<DungeonRoom> _pendingRooms;
    private List<DungeonRoom> _dungeonRooms;
    private List<GameObject> _dungeonRoomInstances;
    private List<GameObject> _propInstances;

    private List<GameObject> roomPrefabs;
    private List<GameObject> specialRoomsPrefabs;
    private List<GameObject> doorsPrefabs;
    public List<Enemy> enemyPrefabs;
    public Enemy bossPrefab;
    private List<Enemy> _enemyInstances;
    #endregion

    private void Awake()
    {
        s = this;

        LoadRoomPrefabs();
    }

    private void LoadRoomPrefabs()
    {
        string roomsPath = "Prefabs/Rooms/";
        string[] roomPrefabNames = { "Room_Boxes_One", "Room_Boxes_Two", "Room_Boxes_Three", "Room_Boxes_Four", "Room_Boxes_Five", "Room_Boxes_Six", "Room_Boxes_Seven", "Room_Boxes_Eight", "Room_Boxes_Nine" };
        roomPrefabs = new List<GameObject>();

        string specialRoomsPath = "Prefabs/Special Rooms/";
        string[] specialRoomsPrefabNames = { "Room_Start", "Room_Boss", "Room_Health_One", "Room_Health_Two"};
        specialRoomsPrefabs = new List<GameObject>();

        string doorsPath = "Prefabs/Doors/";
        string[] doorsPrefabsNames = { "Door_Top", "Door_Right", "Door_Bottom", "Door_Left"};
        doorsPrefabs = new List<GameObject>();

        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < roomPrefabNames.Length; ++i)
        {
            sb.Append(roomsPath).Append(roomPrefabNames[i]);
            GameObject room = Resources.Load<GameObject>(sb.ToString());
            if (!ReferenceEquals(room, null))
                roomPrefabs.Add(room);
            else
                Debug.LogError("Room prefab " + sb.ToString() + " could not be found in " + roomsPath);
            sb.Clear();
        }

        var srb = new System.Text.StringBuilder();
        for (int i = 0; i < specialRoomsPrefabNames.Length; ++i)
        {
            srb.Append(specialRoomsPath).Append(specialRoomsPrefabNames[i]);
            GameObject specialRoom = Resources.Load<GameObject>(srb.ToString());
            if (!ReferenceEquals(specialRoom, null))
                specialRoomsPrefabs.Add(specialRoom);
            else
                Debug.LogError("Special Room prefab " + srb.ToString() + " could not be found in " + specialRoomsPath);
            srb.Clear();
        }

        var sdb = new System.Text.StringBuilder();
        for (int i = 0; i < doorsPrefabsNames.Length; ++i)
        {
            sdb.Append(doorsPath).Append(doorsPrefabsNames[i]);
            GameObject door = Resources.Load<GameObject>(sdb.ToString());
            if (!ReferenceEquals(door, null))
                doorsPrefabs.Add(door);
            else
                Debug.LogError("Door prefab " + sdb.ToString() + " could not be found in " + doorsPath);
            sdb.Clear();
        }
    }

    void Start()
    {
        GenerateDungeon();
    }


    #region Dungeon Generation
    public void GenerateDungeon()
    {
        GetSeed();
        GenerateDungeonLayout();
        GenerateSpecialRooms();
        InstantiateDungeon();
        InstantiateDoors();
        SpawnBoss();
        SpawnEnemies();
    }

    private void GetSeed()
    {
        if (seed.Length == 0)
        {
            seed = "";

            for (int i = 0; i < 16; i++)
            {
                seed += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
            }
        }

        currentSeed = seed.GetHashCode();

        UnityEngine.Random.InitState(currentSeed);
    }

    private void GenerateDungeonLayout()
    {
        _dungeonRooms = new List<DungeonRoom>();
        maxRooms = GetDungeonMaxRoomCount();
        nCurrentRooms = 0;
        _pendingRooms = new Queue<DungeonRoom>();
        DungeonRoom startRoom = new DungeonRoom(0, 0);
        _pendingRooms.Enqueue(startRoom);
        _dungeonRooms.Add(startRoom);

        while (_pendingRooms.Count > 0)
        {
            nCurrentRooms++;
            DungeonRoom currentRoom = _pendingRooms.Dequeue();
            
            int nNeighbours = (nCurrentRooms + _pendingRooms.Count < maxRooms) ? UnityEngine.Random.Range(1, 4) : 0;
            for (int i = 0; i < nNeighbours; ++i)
            {
                if(currentRoom.NeighboursCount < 4) { 
                    ROOM_DIRECTIONS newNeighbourDirection = GetRandomNeighbourDirection(currentRoom);
                    (DungeonRoom, bool) newNeighbour = GenerateNeighbour(currentRoom, newNeighbourDirection);
                    DungeonRoom newNeighbourRoom = newNeighbour.Item1;
                    bool neighbourJustCreated = newNeighbour.Item2;
                    currentRoom.AddNeighbourInDirection(newNeighbourRoom, newNeighbourDirection);
                    if (neighbourJustCreated) 
                    {
                        _pendingRooms.Enqueue(newNeighbourRoom);
                        _dungeonRooms.Add(newNeighbourRoom);
                    }
                }
            }
        }

        Debug.Log(" === DUNGEON HAS BEEN GENERATED === ");
    }

    private bool IsThereRoomInPosition(int x, int y)
    {
        bool result = false;

        for (int i = 0; i < _dungeonRooms.Count; ++i)
        {
            if (_dungeonRooms[i].xPosition == x && _dungeonRooms[i].yPosition == y)
            {
                result = true;
                break;
            }
        }

        return result;
    }

    private DungeonRoom GetRoomInPosition(int x, int y)
    {
        for (int i = 0; i < _dungeonRooms.Count; ++i)
        {
            if (_dungeonRooms[i].xPosition == x && _dungeonRooms[i].yPosition == y)
            {
                return _dungeonRooms[i];
            }
        }
        return null;
    }

    private void InstantiateDungeon()
    {
        GameObject environmentParent = GameObject.Find("ENVIRONMENT");

        _dungeonRoomInstances = new List<GameObject>();
        foreach (DungeonRoom room in _dungeonRooms)
        {
            GameObject roomPrefab = null;
            Quaternion roomRotation = Quaternion.identity;
            if (room.type == RoomTypes.START)
            {
                roomPrefab = specialRoomsPrefabs[0];
            }
            else if (room.type == RoomTypes.BOSS)
            {
                roomPrefab = specialRoomsPrefabs[1];
            }
            else if (room.type == RoomTypes.HEALTH)
            {
                int r = UnityEngine.Random.Range(2, 4);
                roomPrefab = specialRoomsPrefabs[r];
            }
            else
            {
                int r = UnityEngine.Random.Range(0, 9);
                roomPrefab = roomPrefabs[r];
            }

            GameObject roomInstance = Instantiate(roomPrefab, new Vector3(room.xPosition * 18.9f, room.yPosition * 10.5f, 0), roomRotation);
            if (!ReferenceEquals(environmentParent, null))
                roomInstance.transform.parent = environmentParent.transform;
            _dungeonRoomInstances.Add(roomInstance);
        }
    }

    private void InstantiateDoors()
    {
        GameObject environmentParent = GameObject.Find("ENVIRONMENT");
        GameObject doorInstance = null;
        Quaternion roomRotation = Quaternion.identity;
        foreach (DungeonRoom room in _dungeonRooms)
        {
            foreach (Tuple<ROOM_DIRECTIONS, DungeonRoom> neighbour in room.Neighbours)
            {
                GameObject doorPrefab = null;
                switch (neighbour.Item1)
                {
                    case ROOM_DIRECTIONS.UP: 
                        doorPrefab = doorsPrefabs[0];
                        doorInstance = Instantiate(doorPrefab, new Vector3((room.xPosition * 18.9f), (room.yPosition * 10.5f) + 4.6f, 0), roomRotation);
                        break;
                    case ROOM_DIRECTIONS.RIGHT: 
                        doorPrefab = doorsPrefabs[1];
                        doorInstance = Instantiate(doorPrefab, new Vector3((room.xPosition * 18.9f) + 8.6f, (room.yPosition * 10.5f), 0), roomRotation);
                        break;
                    case ROOM_DIRECTIONS.DOWN:
                        doorPrefab = doorsPrefabs[2];
                        doorInstance = Instantiate(doorPrefab, new Vector3((room.xPosition * 18.9f), (room.yPosition * 10.5f) - 4.6f, 0), roomRotation); 
                        break;
                    case ROOM_DIRECTIONS.LEFT:
                        doorPrefab = doorsPrefabs[3];
                        doorInstance = Instantiate(doorPrefab, new Vector3((room.xPosition * 18.9f) - 8.6f, (room.yPosition * 10.5f), 0), roomRotation); 
                        break;
                    default: 
                        break;
                }

                if (!ReferenceEquals(environmentParent, null))
                doorInstance.transform.parent = environmentParent.transform;
            }

        }
    }

    private ROOM_DIRECTIONS GetRandomNeighbourDirection(DungeonRoom currentRoom)
    {
        bool found = false;
        ROOM_DIRECTIONS direction = ROOM_DIRECTIONS.UP;
        while (!found)
        {
            direction = GetRandomDirection();
            if (!currentRoom.HasNeighbourInDirection(direction))
                found = true;
        }
        return direction;
    }
    
    private ROOM_DIRECTIONS GetRandomDirection()
    {
        return (ROOM_DIRECTIONS)UnityEngine.Random.Range(0, 4);
    }
    
    private (DungeonRoom, bool) GenerateNeighbour(DungeonRoom currentRoom, ROOM_DIRECTIONS direction)
    {
        (DungeonRoom, bool) resultTuple;
        DungeonRoom result;
        bool roomCreated = false;
        (int, int)[] newRoomPositions = 
            {
                (currentRoom.xPosition, currentRoom.yPosition + 1),
                (currentRoom.xPosition + 1, currentRoom.yPosition),
                (currentRoom.xPosition, currentRoom.yPosition - 1),
                (currentRoom.xPosition - 1, currentRoom.yPosition)
            };

        (int, int) newPosition = newRoomPositions[(int)direction];
        if (IsThereRoomInPosition(newPosition.Item1, newPosition.Item2))
            result = GetRoomInPosition(newPosition.Item1, newPosition.Item2);
        else
        { 
            result = new DungeonRoom(newPosition.Item1, newPosition.Item2);
            roomCreated = true;
        }
        ROOM_DIRECTIONS oppositeDirection = (ROOM_DIRECTIONS)(((int)direction + 2) % 4);
        
        result.AddNeighbourInDirection(currentRoom, oppositeDirection);

        resultTuple.Item1 = result;
        resultTuple.Item2 = roomCreated;
        return resultTuple;
    }

    private int GetDungeonMaxRoomCount()
    {
        return Mathf.RoundToInt(3.33f + 1 + UnityEngine.Random.Range(5, 8));
    }

    private void GenerateSpecialRooms()
    {
        bool bossGenerated = false;
        _dungeonRooms[0].type = RoomTypes.START;

        for (int i = _dungeonRooms.Count -1; i >= 1 ; --i)
        {
            DungeonRoom room = _dungeonRooms[i];
            if (room.NeighboursCount == 1)
            {
                if (!bossGenerated)
                {
                    room.type = RoomTypes.BOSS;
                    bossGenerated = true;
                }
                else 
                {
                    RoomTypes roomType = GetRandomSpecialRoomType();
                    room.type = roomType;
                }
            }
        }
    }

    private RoomTypes GetRandomSpecialRoomType()
    {
        float rng = UnityEngine.Random.Range(0f, 1f);
        if (rng < 0.5f)
        {
            return RoomTypes.HEALTH;
        }
        else
        {
            return RoomTypes.NORMAL;
        }
    }

    #endregion

    #region Enemies

    private void SpawnBoss() {
        GameObject enemiesParent = GameObject.Find("ENEMIES");
        for (int i = 1; i < _dungeonRoomInstances.Count; ++i)
        {
            DungeonRoom room = _dungeonRooms[i];
            GameObject _room = _dungeonRoomInstances[i];
            if (room.type == RoomTypes.BOSS)
            {
                Transform spawn = _room.transform.Find("Spawnpoint");
                Enemy e = Instantiate(bossPrefab, spawn.position, Quaternion.identity);
                e.currRoom = _room.GetComponent<RoomsOnTriggerEnter>().instance;
                e.transform.parent = enemiesParent.transform;
            }
            
        }
    }

    private void SpawnEnemies()
    {
        _enemyInstances = new List<Enemy>();
        GameObject enemiesParent = GameObject.Find("ENEMIES");
        for (int i = 1; i < _dungeonRoomInstances.Count; ++i)
        {
            if (_dungeonRooms[i].NeighboursCount > 1) 
            {
                GameObject room = _dungeonRoomInstances[i];
                GameObject enemiesParentObject = new GameObject("Enemy Instances");
                enemiesParentObject.transform.parent = room.transform;
                enemiesParentObject.transform.localPosition = Vector3.zero;
                Transform enemySpawnsParent = room.transform.Find("Spawnpoints");
                if (!ReferenceEquals(enemySpawnsParent, null))
                {
                    List<Transform> enemySpawns = new List<Transform>(enemySpawnsParent.GetComponentsInChildren<Transform>());
                    enemySpawns.RemoveAt(0);

                    foreach (Transform spawn in enemySpawns)
                    {
                        if (UnityEngine.Random.Range(0f, 1f) <= 0.85f) 
                        {
                            Enemy e = Instantiate(GetRandomEnemyPrefab(), spawn.position, Quaternion.identity, enemiesParentObject.transform);
                            _enemyInstances.Add(e);
                            e.currRoom = room.GetComponent<RoomsOnTriggerEnter>().instance;
                            e.transform.parent = enemiesParent.transform;
                        }
                    }
                }
            }
        }
    }
    
    private Enemy GetRandomEnemyPrefab()
    {
        int enemyCount = enemyPrefabs.Count;
        return enemyPrefabs[UnityEngine.Random.Range(0, enemyCount)];
    }
}
#endregion
