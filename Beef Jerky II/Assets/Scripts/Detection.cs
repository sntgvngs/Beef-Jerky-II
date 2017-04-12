using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {
    private RaycastHit hit;
    public LayerMask detectionLayer;
    private float range = 5;
    private Transform myTransform;

    public MapGenerator generator;
	// Use this for initialization
	void Start () {
        myTransform = transform;
        generator = GameObject.Find("Map").GetComponent<MapGenerator>();
        detectionLayer = 3 << 10;
	}
	
	// Update is called once per frame
	void Update () {
        DetectItems();
	}

    void DetectItems()
    {
        if (Input.GetButtonUp("Submit")) {
            if (Physics.Raycast(myTransform.position, myTransform.forward, out hit, range, detectionLayer))
            {
                Debug.Log("Current room: " + CurrentRoom());
                Vector3 next = NextRoom(hit.transform.position);
                Debug.Log("Next room: " + next);
                if (!generator.HasCreated(next))
                    generator.CreateRoom(next);
                hit.transform.gameObject.GetComponent<Door>().Toggle();
            }
        }
    }

    Vector3 CurrentRoom()
    {
        Vector3 estimate = myTransform.position / generator.scalefactor;
        return new Vector3((int) estimate.x, (int) estimate.y, (int) estimate.z);
    }

    Vector3 NextRoom(Vector3 door)
    {
        Vector3 current = CurrentRoom();
        Vector3 delta = new Vector3((int) (door.x / generator.scalefactor * 2), 0, (int) (door.y / generator.scalefactor * 2));
        Debug.Log(delta);
        //for (int i = 1; i < generator.neighbors.Length; i++)
        //{
        //    if (Vector3.Distance(current, current + generator.neighbors[i]) < Vector3.Distance(current, closest))
        //        closest = current + generator.neighbors[i];
        //}
        return current + delta;
    }
}
