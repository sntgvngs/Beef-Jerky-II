using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public GameObject wall;
    public GameObject door;
    public GameObject floor;
    public GameObject exit;
    public GameObject enemyFloor;
    public GameObject healingFloor;

    public float scalefactor = 30f;
    public float vscalefactor = 10f;

    public Vector3[] neighbors;
    List<Vector3> createdRooms;

    private float exitChance = 0.2f;

    public int currentLevel;
    public bool exitCreated;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.Find("FPSController").transform;
        neighbors = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        currentLevel = -1;
        exitCreated = false;
        createdRooms = new List<Vector3>();
        CreateRoom(Vector3.zero, 0);
    }

    public void CreateRoom(Vector3 loc, int level)
    {
        float selector;
        if (level == currentLevel)
        {
            selector = Random.value;
            Instantiate(floor, new Vector3(loc.x * scalefactor, (loc.y + 1) * vscalefactor, loc.z * scalefactor), Quaternion.identity, transform);
        }
        else
        {
            selector = 2f;
        }
        if(selector < exitChance && !exitCreated)
        {
            // Create exit floor
            Instantiate(exit, new Vector3(loc.x * scalefactor, -level * vscalefactor, loc.z * scalefactor), Quaternion.identity, transform);
            exitCreated = true;
            CheckNextLevel();
            CreateRoom(loc + Vector3.down, level + 1);
        } else if(selector < 0.5f)
        {
            // Create healing floor
            Instantiate(healingFloor, new Vector3(loc.x * scalefactor, -level * vscalefactor, loc.z * scalefactor), Quaternion.identity, transform);
        } else if(selector < 0.8f)
        {
            // Create enemy floor
            Instantiate(enemyFloor, new Vector3(loc.x * scalefactor, -level * vscalefactor, loc.z * scalefactor), Quaternion.identity, transform);
        } else
        {
            // Create vanilla floor
            Instantiate(floor, new Vector3(loc.x * scalefactor, -level * vscalefactor, loc.z * scalefactor), Quaternion.identity, transform);
        }
        
        int j = Random.Range(0, 4);
        for (int i = 0; i < neighbors.Length; i++)
        {
            if(!HasCreated(loc + neighbors[i]))
            {
                if((i == j && !exitCreated)  || Random.value > 0.5f)
                {
                    if (i > 1)
                        Instantiate(door, new Vector3(loc.x * scalefactor + neighbors[i].x * scalefactor / 2, -level * vscalefactor, loc.z * scalefactor + neighbors[i].z * scalefactor / 2), Quaternion.FromToRotation(Vector3.forward, neighbors[i]), transform);
                    else
                        Instantiate(door, new Vector3(loc.x * scalefactor + neighbors[i].x * scalefactor / 2, -level * vscalefactor, loc.z * scalefactor + neighbors[i].z * scalefactor / 2), Quaternion.identity, transform);
                } else
                {
                    if (i > 1)
                        Instantiate(wall, new Vector3(loc.x * scalefactor + neighbors[i].x * scalefactor / 2, -level * vscalefactor, loc.z * scalefactor + neighbors[i].z * scalefactor / 2), Quaternion.FromToRotation(Vector3.forward, neighbors[i]), transform);
                    else
                        Instantiate(wall, new Vector3(loc.x * scalefactor + neighbors[i].x * scalefactor / 2, -level * vscalefactor, loc.z * scalefactor + neighbors[i].z * scalefactor / 2), Quaternion.identity, transform);
                }
            }
        }
        createdRooms.Add(loc);
        
    }

    public bool HasCreated(Vector3 loc)
    {
        bool contains = false;
        foreach (Vector3 room in createdRooms)
            if (Vector3.Distance(room, loc) < 0.5)
                contains = true;
        return contains;
    }

    private void CheckNextLevel()
    {
        //if(levels[(currentLevel + 1) % 3] != null)
        //{
        //    GameObject[] destroyParts = GameObject.FindGameObjectsWithTag("M"+((currentLevel + 1) % 3));
        //    for (int i = 0; i < destroyParts.Length; i++)
        //    {
        //        Destroy(destroyParts[i]);
        //    }
        //}
        //levels[(currentLevel + 1) % 3] = new ArrayList();
        //exitCreated = false;
    }

    private void Update()
    {
        int estimate = Mathf.FloorToInt(-playerTransform.position.y / vscalefactor) + 1;
        if(estimate > currentLevel)
        {
            exitCreated = false;
            currentLevel = estimate;
        }
    }
}
