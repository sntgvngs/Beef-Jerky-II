using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {
    private RaycastHit hit;
    public LayerMask detectionLayer;
    public LayerMask shootStuff;
    private float range = 5;
    private Transform myTransform;

    public GameObject shot;

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
                Vector3 next = NextRoom(hit.transform.position);
                if (!generator.HasCreated(next))
                    generator.CreateRoom(next, generator.currentLevel);
                hit.transform.gameObject.GetComponent<Door>().Toggle();
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Instantiate(shot, myTransform.position + myTransform.forward, Quaternion.FromToRotation(Vector3.forward, myTransform.forward));
            if (Physics.Raycast(myTransform.position, myTransform.forward, out hit, range * 10, shootStuff))
            {
                Floid enemy = hit.transform.gameObject.GetComponent<Floid>();
                Rotator heart = hit.transform.gameObject.GetComponent<Rotator>();
                if (enemy != null)
                {
                    enemy.Explode();
                } else if (heart != null)
                {
                    heart.Heal();
                }
            }
        }
    }

    public Vector3 CurrentRoom()
    {
        Vector3 estimate = myTransform.position;
        estimate.x /= generator.scalefactor;
        estimate.y /= generator.vscalefactor;
        estimate.z /= generator.scalefactor;
        return new Vector3(Mathf.Round(estimate.x), Mathf.Round(estimate.y), Mathf.Round(estimate.z));
    }

    Vector3 NextRoom(Vector3 door)
    {
        Vector3 current = CurrentRoom();
        Vector3 delta = (door - current * generator.scalefactor)/generator.scalefactor;
        delta = new Vector3(2 * delta.x, 0, 2 * delta.z);
        return current + delta;
    }
}
