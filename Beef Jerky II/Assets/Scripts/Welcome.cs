using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welcome : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(WelcomeMessage());
	}

    ////Update is called once per frame
    //void Update()
    //{

    //}

    IEnumerator WelcomeMessage ()
    {
        yield return new WaitForSeconds(4.5f);
        Debug.Log("Welcome!");
    }

     
}
