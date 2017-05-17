using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
    float destroyTime;
    // Use this for initialization
    void Start () {
        destroyTime = Time.time + 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > destroyTime)
            Destroy(gameObject);
    }
}
