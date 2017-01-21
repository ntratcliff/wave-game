using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleScript : MonoBehaviour {

    public float y;
    public float speed;
    public float lowerRangeX; //-3 to 3 seems to be the best current one
    public float higherRangeX;
    public float lowerRangeSpeed;
    public float higherRangeSpeed; //recommmend to cap this at 0.2

    public bool didCollide;

	// Use this for initialization
	void Start () {
        speed = Random.Range(lowerRangeSpeed, higherRangeSpeed);
        y = Random.Range( lowerRangeX, higherRangeX);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        didCollide = false;

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 speedAdjust = new Vector3(speed * Time.deltaTime, 0, 0);
        transform.position -= speedAdjust;
        //need way to mess with 
	}

    //score implementation
   void OnTriggerEnter(Collider other)
    {
        didCollide = true;
    }
}
