﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BailingPassenger : MonoBehaviour
{
    public float xMin = -200;
    public float xMax = 200;
    public float yMin = 100;
    public float yMax = 300;
    public float torqueMin = -200;
    public float torqueMax = 200;
    Rigidbody2D body;
    AudioSource audio;

    // Use this for initialization
    void Start ()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Bail()
    {
        body.gravityScale = 1;
        body.velocity = Vector3.zero;
        body.angularVelocity = 0;
        body.AddForce(new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax)));
        body.AddTorque(Random.Range(torqueMin, torqueMax));
        audio.PlayOneShot(audio.clip);
        //Debug.Log("Time to bail!");
    }
}
