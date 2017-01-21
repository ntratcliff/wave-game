using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour {

    public float y;
    public float speed;
    public float lowerRangeX; //-3 to 3 seems to be the best current one
    public float higherRangeX;
    //    public float lowerRangeSpeed;
    //    public float higherRangeSpeed; //recommmend to cap this at 4.
    //public float speed;

    public float perlinFreq;

    public bool isMoving;
    public bool didCollide;

	// Use this for initialization
	void Start () {
        //speed = Random.Range(lowerRangeSpeed, higherRangeSpeed);
        y = Mathf.PerlinNoise(Time.time + Random.Range(2.0f, 5.0f), 0) * 5;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        didCollide = false;
        isMoving = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            Vector3 speedAdjust = new Vector3(speed * Time.deltaTime, 0, 0);
            //    y = Mathf.PerlinNoise(Time.time, 0) * 3;
            //   transform.position = new Vector3(transform.position.x, y, transform.position.z);
            transform.position -= speedAdjust;
        }
        
        //need way to mess with 
	}

    //score implementation
   void OnTriggerEnter(Collider other)
    {
        didCollide = true;
        isMoving = false;
        transform.position = new Vector3(10, transform.position.y, transform.position.z);
    }
}
