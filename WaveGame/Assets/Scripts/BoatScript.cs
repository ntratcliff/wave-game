using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    

    public WaterManager wave;
    public int nodeDifference = 1;
    public float distMultiplier = 7.0f;
    public float angleMultiplier = 7.0f;
    private int index;
    private Vector2 lastPos;
    
    [SerializeField]
    float deathForce = .25f;
    [SerializeField]
    float deathTilt = 30;
    [SerializeField]
    int lives = 3;
    [SerializeField]
    float immunityFrames = 7;
    float immunityTimeLeft = 0;

    public int Lives
    {
        get
        {
            return lives;
        }
    }

    // Use this for initialization
    void Start ()
    {
        index = wave.Positions.Length / 2;
        transform.position = wave.Positions[index];
        lastPos = transform.position;
    }

    #region Old Physics Code
    //forceMag /= mass;
    //force = forceDir * forceMag;
    //accel = force;

    //transform.position += vel * Time.deltaTime;
    //vel += accel * Time.deltaTime;
    #endregion

    // Update is called once per frame
    void Update ()
    {
        Vector2 middleNode = wave.Positions[index];
        Vector2 nextNode = wave.Positions[index + nodeDifference];
        Vector2 prevNode = wave.Positions[index - nodeDifference];
        float yVal = Mathf.Lerp(transform.position.y, middleNode.y, Time.deltaTime* distMultiplier);
        transform.position = new Vector2(middleNode.x, yVal);

        Vector2 diff = (nextNode - prevNode).normalized;
        transform.right = Vector2.Lerp(transform.right, diff, Time.deltaTime*angleMultiplier);

        float angle = Mathf.Acos(Vector3.Dot(transform.right, Vector3.right)) * Mathf.Rad2Deg;
        float velocity = ((Vector2)transform.position - lastPos).magnitude;

        if ((velocity >= deathForce || angle >= deathTilt) && immunityTimeLeft <= 0)
        {
            Debug.Log("You lost a life");
            --lives;
            immunityTimeLeft = immunityFrames;
        }

        immunityTimeLeft -= Time.deltaTime;
        lastPos = transform.position;
    }
}
