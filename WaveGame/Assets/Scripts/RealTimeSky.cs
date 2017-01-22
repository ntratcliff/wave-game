using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeSky : MonoBehaviour {

    float rotationValue;
    private System.DateTime dt;
    public float mins;
    public float secs;
    public float hours;
    public float sumTime;
    public float convertedAngle;

    private float counter;
   // private DateTime dt;
	// Use this for initialization
	void Start () {
        // rotationValue = 0;
        dt = System.DateTime.Now;
        hours = dt.Hour;
        if (hours > 12)
        {
            hours -= 12;
        }
        mins = dt.Minute;
        secs = dt.Second;
        sumTime = (hours * 360) + (mins * 60) + secs;
        convertedAngle = sumTime / 12;
        transform.eulerAngles = new Vector3(0,0,convertedAngle);
    }
	
	// Update is called once per frame
	void Update () {
        float counter = Time.deltaTime/12;
        transform.eulerAngles += new Vector3(0, 0, counter);
       // transform.rotation

	}
}
