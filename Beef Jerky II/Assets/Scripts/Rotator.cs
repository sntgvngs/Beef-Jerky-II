using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    static Health playerHP;
    private AudioSource aSource;

    private void Start()
    {
        if (playerHP == null)
            playerHP = GameObject.Find("HUD").GetComponent<Health>();
        aSource = GetComponent<AudioSource>();
    }

    public void Heal()
    {
        if (playerHP.health < 3)
        {
            playerHP.Heal(1);
            aSource.Play();
            Destroy(gameObject);
        }
    }

    void Update () {
        transform.Rotate(Vector3.up * 90 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FPSController")
        {
            Heal();
        }
            
    }
}
