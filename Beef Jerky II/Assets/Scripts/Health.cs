﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int health;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;

    private AudioSource aSource;

    private UnityEngine.UI.Image[] hearts;
    Animator anim;

    public Sprite full;
    public Sprite empty;

	// Use this for initialization
	void Start () {
        health = 3;

        aSource = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();
        hearts = new UnityEngine.UI.Image[3];
        hearts[0] = Heart1.GetComponent<UnityEngine.UI.Image>();
        hearts[1] = Heart2.GetComponent<UnityEngine.UI.Image>();
        hearts[2] = Heart3.GetComponent<UnityEngine.UI.Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Damage(int dmg)
    {
        aSource.Play();
        health -= dmg;
        if (health == 0)
        {
            anim.SetTrigger("GameOver");
            hearts[health].sprite = empty;
        }
        else if (health > 0)
            hearts[health].sprite = empty;
    }

    public void Heal(int hea)
    {
        hearts[health].sprite = full;
        health += hea;
    }
}
