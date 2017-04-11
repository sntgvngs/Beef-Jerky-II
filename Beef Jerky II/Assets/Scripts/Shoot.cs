using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    private float nextFire = 0f;
    private float rate = 0.25f;
    private RaycastHit hit;
    private float range = 300;
    private Transform myTransform;

    public Door doorScript;
    private bool open;

	// Use this for initialization
	void Start () {
        myTransform = transform;
        doorScript = GameObject.Find("EastDoor").GetComponent<Door>();
        open = false;
	}
	
	// Update is called once per frame
	void Update () {
        checkForInput();
	}

    void checkForInput()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + rate;

            if(Physics.Raycast(myTransform.position, myTransform.forward, out hit, range))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    if (open)
                    {
                        doorScript.Close();
                    } else
                    {
                        doorScript.Open();
                    }
                    open = !open;
                }
            }
        }
    }
}
