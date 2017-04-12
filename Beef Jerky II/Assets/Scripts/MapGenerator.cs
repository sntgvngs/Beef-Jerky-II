using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public GameObject wall;
    public GameObject door;
    public GameObject floor;

    public float scalefactor = 30f;

    public Vector3[] neighbors;
    ArrayList createdRooms;

    void Start()
    {
        neighbors = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        createdRooms = new ArrayList();
        CreateRoom(Vector3.zero);
    }

    public void CreateRoom(Vector3 loc)
    {
        Instantiate(floor, new Vector3(loc.x * scalefactor, 0, loc.z * scalefactor), Quaternion.identity, transform);
        for(int i = 0; i < neighbors.Length; i++)
        {
            if(!HasCreated(loc + neighbors[i]))
            {
                if (Random.value > 0.5f)
                {
                    if (i > 1)
                        Instantiate(wall, new Vector3(loc.x * scalefactor + neighbors[i].x * scalefactor / 2, 0, loc.z * scalefactor + neighbors[i].z * scalefactor / 2), Quaternion.FromToRotation(Vector3.forward, neighbors[i]), transform);
                    else
                        Instantiate(wall, new Vector3(loc.x * scalefactor + neighbors[i].x * scalefactor / 2, 0, loc.z * scalefactor + neighbors[i].z * scalefactor / 2), Quaternion.identity, transform);
                }
                else
                {
                    if (i > 1)
                        Instantiate(door, new Vector3(loc.x * scalefactor + neighbors[i].x * scalefactor / 2, 0, loc.z * scalefactor + neighbors[i].z * scalefactor / 2), Quaternion.FromToRotation(Vector3.forward, neighbors[i]), transform);
                    else
                        Instantiate(door, new Vector3(loc.x * scalefactor + neighbors[i].x * scalefactor / 2, 0, loc.z * scalefactor + neighbors[i].z * scalefactor / 2), Quaternion.identity, transform);

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

    //    enum Objective {Spawn, Key, BigKey, Boss}
    //    enum Lock {Key, BigKey}

    //    ObjectiveNode objectiveList;

    //	// Use this for initialization
    //	void Start () {
    //        createObjectives(0);

    //	}

    //	// Update is called once per frame
    //	void Update () {

    //	}

    //    void createObjectives (int level)
    //    {
    //        ObjectiveNode list = new ObjectiveNode();
    //        for(int i = 0; i < level; i++)
    //        {
    //            list = new ObjectiveNode(Objective.Key, list);
    //        }
    //        objectiveList = new ObjectiveNode(Objective.Spawn, list);
    //    }

    //    class ObjectiveNode
    //    {
    //        Objective objective;
    //        public ObjectiveNode next;

    //        /* 
    //         * Creates a BigKey Node with a Boss Node as its next.
    //         */
    //        public ObjectiveNode()
    //        {
    //            next = new ObjectiveNode(Objective.Boss);
    //            objective = Objective.BigKey;
    //        }

    //        /* 
    //         * Creates a Node with the given obj.
    //         */
    //        public ObjectiveNode(Objective obj)
    //        {
    //            objective = obj;
    //        }
    //        /* 
    //         * Creates Node with the given obj. with nx as its next.
    //         */
    //        public ObjectiveNode(Objective obj, ObjectiveNode nx)
    //        {
    //            objective = obj;
    //            next = nx;
    //        }

    //    }
}
