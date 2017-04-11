using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {
    private RaycastHit hit;
    public LayerMask detectionLayer;
    private float checkRate = 0.5f;
    private float nextCheck = 0;
    private float range = 5;
    private Transform myTransform;
	// Use this for initialization
	void Start () {
        myTransform = transform;
        detectionLayer = 1 << 9;
	}
	
	// Update is called once per frame
	void Update () {
        DetectItems();
	}

    void DetectItems()
    {
        if(Time.time >= nextCheck)
        {
            nextCheck = Time.time + checkRate;
            if(Physics.Raycast(myTransform.position, myTransform.forward, out hit, range, detectionLayer))
            {
                Debug.Log(hit.transform.name + " is an item!");
            }
        }
    }
}
