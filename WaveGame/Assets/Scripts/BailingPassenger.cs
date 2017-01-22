using System.Collections;
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
    Renderer renderer;
    public AudioClip[] screams;

    // Use this for initialization
    void Awake ()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        renderer = GetComponent<Renderer>();
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
        int randIndex = (int)Random.Range(0.0f, 2.0f);
        audio.PlayOneShot(screams[randIndex]);
    }

    public void Board()
    {
        //renderer.sharedMaterial.SetColor("_Color", new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0));
        //renderer.material.SetColor("_Color", new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0));
        body.gravityScale = 0;
        body.velocity = Vector3.zero;
        body.angularVelocity = 0;
        //body.rotation = transform.parent.rotation.eulerAngles.z;
        transform.rotation = transform.parent.rotation;
        //Debug.Log("Tring to board " + transform.parent.rotation.eulerAngles.z + " " + body.rotation);
    }
}
