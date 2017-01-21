using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {

    public GameObject wave;
    public float mass = 5;
    public Vector2 initPos;
    public Vector2 initVel;
    public Vector2 initAccel;
    private Vector3 accel = new Vector2(0, 0);
    private Vector3 vel = new Vector2(0, 0);
    private Vector2 forceDir = new Vector2(1, 0);
    private float forceMag = 500.0f;
    private Vector2 force;

    // Use this for initialization
    void Start () {
        transform.position = initPos;

        //vel = new Vector3(initVel.x, initVel.y, 0);
        //accel = new Vector3(initAccel.x, initAccel.y, 0);
    }

    // Update is called once per frame
    void Update () {
        //string message = "Force Mag: <color=red>" + forceMag.ToString() + "</color>";
        //Debug.Log(message);
        forceMag /= mass;
        force = forceDir * forceMag;
        accel = force;

        transform.position += vel * Time.deltaTime;
        vel += accel * Time.deltaTime;
    }
}
