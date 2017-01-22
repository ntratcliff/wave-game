using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BailingPassenger : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    // Use this for initialization
    void Start ()
    {
        Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
        body.gravityScale = 1;
        body.AddForce(new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax)));
        Debug.Log("Time to bail!");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
