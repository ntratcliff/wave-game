using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour {

    public WaterManager wave;
    public float mass = 5;
    public Vector2 initPos;
    public Vector2 initVel;
    public Vector2 initAccel;
    public int nodeDifference = 1;
    private Vector3 accel = new Vector2(0, 0);
    private Vector3 vel = new Vector2(0, 0);
    private Vector2 forceDir = new Vector2(1, 0);
    private float forceMag = 500.0f;
    private Vector2 force;
    int index;

    // Use this for initialization
    void Start () {
        index = wave.Positions.Length / 2;
        transform.position = wave.Positions[index];

        
    }

    #region Old Physics Code
    //forceMag /= mass;
    //force = forceDir * forceMag;
    //accel = force;

    //transform.position += vel * Time.deltaTime;
    //vel += accel * Time.deltaTime;
    #endregion

    // Update is called once per frame
    void Update () {
        Vector2 middleNode = wave.Positions[index];
        Vector2 nextNode = wave.Positions[index + nodeDifference];
        Vector2 prevNode = wave.Positions[index - nodeDifference];
        transform.position = middleNode;
        Vector2 diff = (nextNode - prevNode).normalized;
        //float angle = Mathf.Acos(Vector2.Dot(diff, Vector2.right));
        transform.right = diff;
        //transform.Rotate(transform.rotation.eulerAngles - angle);
        //transform.rotation.SetEulerRotation(1,1,1);
        //Debug.Log("Boats forward " + transform.right);
        //Debug.Log("Angle of boat <color=blue>" + angle+"</color> vector between the boat <color=green>"+diff+"</color>");
        Vector2 offset = nextNode = middleNode;

        //string m = "Vector2(<color=red>" + offset.x + "</color>, <color=red>" + offset.y + "</color>)";

        //Debug.Log(m);
    }
}
