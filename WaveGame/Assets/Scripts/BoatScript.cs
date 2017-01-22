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

    Queue<GameObject> passengers;
    [Range(0, 1)]
    float percSkinnyPerson = .33f;
    [Range(0, 1)]
    float percChildPerson = .33f;
    [Range(0, 1)]
    float percFatPerson = .33f;

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
        passengers = new Queue<GameObject>();

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    passengers.Enqueue(transform.GetChild(i).gameObject);
        //}

        for (int i = 0; i < lives; i++)
        {
            float guyPerc = Random.Range(0, 100);
            GameObject newGuy;

            if (guyPerc < percSkinnyPerson * 100)
            {
                newGuy = Resources.Load("SkinnyPerson") as GameObject;
            }
            else if (guyPerc < (percSkinnyPerson + percChildPerson) * 100)
            {
                newGuy = Resources.Load("ChildPerson") as GameObject;
            }
            else
            {
                newGuy = Resources.Load("FatPerson") as GameObject;
            }

            newGuy = Instantiate(newGuy, transform);
            newGuy.transform.parent = transform;
            newGuy.transform.localPosition = new Vector3(newGuy.transform.localPosition.x + Random.Range(-.3f, .3f), newGuy.transform.localPosition.y, newGuy.transform.localPosition.z);
            passengers.Enqueue(newGuy);
        }
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
            GameObject bailingPassenger = passengers.Dequeue();
            bailingPassenger.GetComponent<BailingPassenger>().enabled = true;
            bailingPassenger.transform.parent = null;
            //bailingPassenger.GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        immunityTimeLeft -= Time.deltaTime;
        lastPos = transform.position;
    }
}
