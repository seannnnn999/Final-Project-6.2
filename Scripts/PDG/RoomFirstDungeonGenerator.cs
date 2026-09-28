using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4;

    [SerializeField]
    private int minRoomHeight = 4;

    [SerializeField]
    private int dungeonWidth = 20;
    
    [SerializeField]
    private int dungeonHeight = 20;

    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;

    [SerializeField]
    private bool randomWalkRooms = false;



    //public Nodes nodePrefab;
    //public List<Nodes> nodeList;
    public Enemy enemy;
    //private bool canDrawGizmos;


    void Start()
    {
        GameObject pastEnemy;
        pastEnemy = GameObject.FindGameObjectWithTag("Enemy");
        Destroy(pastEnemy);

        //GameObject[] respawns;
        //respawns = GameObject.FindGameObjectsWithTag("Node");

        //for (var i = 1; i < respawns.Length; i++)
        //{
        //    Destroy(respawns[i]);
        //}

        //nodeList.Clear();

        CreateRooms();
    }

    void Update()
    {
        if (StaticValues.roomFinished == 1)
        {
            tilemapVisualiser.Clear();
            CreateRooms();
            StaticValues.roomFinished = 0;
        }
    }
    protected override void RunProceduralGeneration()
    {
        GameObject pastEnemy;
        pastEnemy = GameObject.FindGameObjectWithTag("Enemy");
        //Destroy(pastEnemy);

        //GameObject[] respawns;
        //respawns = GameObject.FindGameObjectsWithTag("Node");
        //for (var i = 1; i < respawns.Length; i++)
        //{
        //    Destroy(respawns[i]);
        //}

        //nodeList.Clear();
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tilemapVisualiser.PaintFloorTiles(floor);


        WallGenerator.CreateWalls(floor, tilemapVisualiser);
        if(tilemapVisualiser.doorGenerated == false)
        {
            tilemapVisualiser.Clear();
            tilemapVisualiser.PaintFloorTiles(floor);
            WallGenerator.CreateWalls(floor, tilemapVisualiser);
        }
        else
        {
            tilemapVisualiser.doorGenerated = false;
            tilemapVisualiser.playerPlaced = false;
        }
            
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for(int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];

            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            foreach(var position in roomFloor)
            {
                if(position.x >= (roomBounds.xMin +offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);

                    //Nodes node = Instantiate(nodePrefab, new Vector2(position.x + 0.5f, position.y +0.5f), Quaternion.identity);
                    
                    //nodeList.Add(node);
                }
            }    
        }

        //for(int i = 0; i<nodeList.Count; i++)
        //{
        //    for(int j= i+1; j<nodeList.Count; j++)
        //    {
        //        if (Vector2.Distance(nodeList[i].transform.position, nodeList[j].transform.position) <= 1.0f) 
        //        {
        //            ConnectNodes(nodeList[i], nodeList[j]);
        //            ConnectNodes(nodeList[j], nodeList[i]);

        //        }
        //    }
        //}
        //canDrawGizmos = true;
        //SpawnAI();

        return floor;
    }
    
    //void ConnectNodes(Nodes from, Nodes to)
    //{
    //    if(from ==to)
    //    { return; }

    //    from.connections.Add(to);
    //}

    //void SpawnAI()
    //{
    //    Nodes randomNode = nodeList[UnityEngine.Random.Range(0, nodeList.Count)];
    //    Enemy newEnemy = Instantiate(enemy, randomNode.transform.position, Quaternion.identity);
    //    newEnemy.currentNode = randomNode;
    //}

    //private void OnDrawGizmos()
    //{
    //    if(canDrawGizmos)
    //    {
    //        Gizmos.color = Color.red;
    //        for(int i=0; i<nodeList.Count; i++)
    //        {
    //            for(int j=0; j < nodeList[i].connections.Count; j++)
    //            {
    //                Gizmos.DrawLine(nodeList[i].transform.position, nodeList[i].connections[j].transform.position);
    //            }
    //        }
    //    }
    //}

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);
        while( roomCenters.Count > 0 )
        {
            Vector2Int closestCenter = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closestCenter);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closestCenter);
            currentRoomCenter = closestCenter;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while(position.y != destination.y)
        {
            if(destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if(destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while(position.x != destination.x)
        {
            if(destination.x>position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closestCenter = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach(var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if(currentDistance < distance )
            {
                distance = currentDistance;
                closestCenter = position;
            }
        }
        return closestCenter;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for(int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
