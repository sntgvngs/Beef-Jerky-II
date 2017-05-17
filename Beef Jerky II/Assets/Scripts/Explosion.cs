using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    static Health playerHP;
    private AudioSource aSource;
    float destroyTime;
    // Use this for initialization
    void Start () {
        if (playerHP == null)
            playerHP = GameObject.Find("HUD").GetComponent<Health>();
        aSource = GetComponent<AudioSource>();
        aSource.Play();
        destroyTime = Time.time + 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > destroyTime)
            Destroy(gameObject);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "FPSController")
            playerHP.Damage(1);
    }
}
