using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welcome : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(WelcomeMessage());
	}

    IEnumerator WelcomeMessage ()
    {
        yield return new WaitForSeconds(4.5f);
        Debug.Log("Welcome!");
    }

     
}
