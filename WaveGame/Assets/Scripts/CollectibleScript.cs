using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour {

    public float y;
    public float speed;
//    public float lowerRangeX; //-3 to 3 seems to be the best current one
 //   public float higherRangeX;
    //    public float lowerRangeSpeed;
    //    public float higherRangeSpeed; //recommmend to cap this at 4.
    //public float speed;

    public float perlinFreq;
    private Scoreboard scoreboard;
    public bool isMoving;
    public bool didCollide;

    public AudioSource sfx;

    public float noiseModifierRangeLow; //-2 default
    public float noiseModifierRangeHigh; //2 default
    public float heightMultiplier; //4 default
    public float minHeight;
    public float maxHeight;

    public float sfxPitchLow;  //.8
    public float sfxPitchHigh; //1.2

	// Use this for initialization
	void Start () {

        scoreboard = FindObjectOfType<Scoreboard>();
        //speed = Random.Range(lowerRangeSpeed, higherRangeSpeed);
        y = minHeight + (Mathf.PerlinNoise(Time.time + Random.Range(noiseModifierRangeLow, noiseModifierRangeHigh), 0) )* (heightMultiplier - maxHeight);
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

    public void rerollHeight()
    {
        y = Mathf.PerlinNoise(Time.time + Random.Range(noiseModifierRangeLow, noiseModifierRangeHigh), 0) * heightMultiplier;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public void moveOffscreen()
    {
        transform.position = new Vector3(10, 1000, transform.position.z);
    }
    //score implementation
    void OnTriggerEnter2D(Collider2D other)
    {
        didCollide = true;
        isMoving = false;
        transform.position = new Vector3(10, transform.position.y, transform.position.z);
        rerollHeight();
        scoreboard.AddPoint();
        sfx.pitch = Random.Range(sfxPitchLow, sfxPitchHigh);
        sfx.Play();
        
    }

    
}
