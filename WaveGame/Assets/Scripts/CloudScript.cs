using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{

    public float y;
    public float xMod;
    public float vanillaSpeed;
    public float speed;
    //    public float lowerRangeX; //-3 to 3 seems to be the best current one
    //   public float higherRangeX;
    //    public float lowerRangeSpeed;
    //    public float higherRangeSpeed; //recommmend to cap this at 4.
    //public float speed;

    public float perlinFreq;
    //private Scoreboard scoreboard;
    public bool isMoving;
    //public bool didCollide;

    public float noiseModifierRangeLow; //-2 default
    public float noiseModifierRangeHigh; //2 default
    public float heightMultiplier; //4 default

    public float scaleModMin; //.25f
    public float scaleModMax; //1.33f
    public float speedModMin; //.1f
    public float speedMonMax; //.3f
    public float zAnchor;
    public float zMod;
    public float minHeight;
    public float maxHeight;
    public float minXmod;
    public float maxXmod;

    // Use this for initialization
    void Start()
    {

        //        scoreboard = FindObjectOfType<Scoreboard>();
        //speed = Random.Range(lowerRangeSpeed, higherRangeSpeed);
        y = Random.Range(minHeight, maxHeight);//minHeight + (Mathf.PerlinNoise(Time.time + Random.Range(noiseModifierRangeLow, noiseModifierRangeHigh), 0)) * (heightMultiplier - maxHeight);
        xMod = Random.Range(minXmod, maxXmod);
        transform.position = new Vector3(transform.position.x + xMod, y, transform.position.z);
        speed = vanillaSpeed;
  //      didCollide = false;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void rerollScale()
    {
        float scaleBy = Random.Range(scaleModMin, scaleModMax);
        transform.localScale = new Vector3(scaleBy, scaleBy, scaleBy);

        float zPos = zAnchor * (zMod * scaleBy);
        transform.position = new Vector3(transform.position.x, zPos, transform.position.z);

        speed = vanillaSpeed * scaleBy + Random.Range(speedModMin,speedMonMax);
    }




}
