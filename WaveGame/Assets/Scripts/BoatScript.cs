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
    int startingLives = 6;
    int lives;
    [SerializeField]
    float immunityFrames = 7;
    float immunityTimeLeft = 0;

    Queue<GameObject> passengers;
    GameObject[] winningPassengers;
    BailingPassenger[] winningPassengersScripts;
    public int numWinningPassengers = 100;
    public float winningPassengerCooldown = .01f;
    float winningPassengerTimeLeft = 0;
    int winningPassengerIndex = 0;
    //[Range(0, 1)]
    //float percSkinnyPerson = .33f;
    //[Range(0, 1)]
    //float percChildPerson = .33f;
    //[Range(0, 1)]
    //float percFatPerson = .33f;

    public GameObject PersonPrefab;
    public float PersonStartX, PersonSpacing, MiddleSpacing;
    public GameManagerScript gameManager;

    public int Lives
    {
        get
        {
            return lives;
        }
    }

    void Awake()
    {
        passengers = new Queue<GameObject>();

        for (int i = 0; i < startingLives; i++)
        {
            GameObject person = Instantiate(PersonPrefab);
            passengers.Enqueue(person);
        }
    }

    // Use this for initialization
    void Start ()
    {
        index = wave.Positions.Length / 2;
        transform.position = wave.Positions[index];
        lastPos = transform.position;

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    passengers.Enqueue(transform.GetChild(i).gameObject);
        //}

        //for (int i = 0; i < lives; i++)
        //{
        //    float guyPerc = Random.Range(0, 100);
        //    GameObject newGuy;

        //    if (guyPerc < percSkinnyPerson * 100)
        //    {
        //        newGuy = Resources.Load("SkinnyPerson") as GameObject;
        //    }
        //    else if (guyPerc < (percSkinnyPerson + percChildPerson) * 100)
        //    {
        //        newGuy = Resources.Load("ChildPerson") as GameObject;
        //    }
        //    else
        //    {
        //        newGuy = Resources.Load("FatPerson") as GameObject;
        //    }

        //    newGuy = Instantiate(newGuy, transform);
        //    newGuy.transform.parent = transform;
        //    newGuy.transform.localPosition = new Vector3(newGuy.transform.localPosition.x + Random.Range(-.3f, .3f), newGuy.transform.localPosition.y, newGuy.transform.localPosition.z);
        //    passengers.Enqueue(newGuy);
        //}


        //ResetBoat();

        winningPassengers = new GameObject[numWinningPassengers];
        winningPassengersScripts = new BailingPassenger[numWinningPassengers];

        for (int i = 0; i < numWinningPassengers; i++)
        {
            winningPassengers[i] = Instantiate(PersonPrefab);
            winningPassengers[i].transform.position = new Vector3(-1000, -1000, 0);
            winningPassengersScripts[i] = winningPassengers[i].GetComponent<BailingPassenger>();
            winningPassengersScripts[i].enabled = true;
        }

        if (!gameManager)
        {
            Debug.LogError("Forgot to add the game manager to the boat");
            gameManager = GameObject.FindObjectOfType<GameManagerScript>();
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
        if (lives <= 0)
        {
            LaunchWinners();
        }

        Vector2 middleNode = wave.Positions[index];
        Vector2 nextNode = wave.Positions[index + nodeDifference];
        Vector2 prevNode = wave.Positions[index - nodeDifference];
        float yVal = Mathf.Lerp(transform.position.y, middleNode.y, Time.deltaTime* distMultiplier);
        transform.position = new Vector2(middleNode.x, yVal);

        Vector2 diff = (nextNode - prevNode).normalized;
        transform.right = Vector2.Lerp(transform.right, diff, Time.deltaTime*angleMultiplier);

        float angle = Mathf.Acos(Vector3.Dot(transform.right, Vector3.right)) * Mathf.Rad2Deg;
        float velocity = ((Vector2)transform.position - lastPos).magnitude;

        if (gameManager.preGame)
        {
            return;
        }

        if ((velocity >= deathForce || angle >= deathTilt) && immunityTimeLeft <= 0 && lives > 0)
        {
            Debug.Log("You lost a life");
            --lives;
            immunityTimeLeft = immunityFrames;
            GameObject bailingPassenger = passengers.Dequeue();
            bailingPassenger.transform.parent = null;
            bailingPassenger.GetComponent<BailingPassenger>().Bail();
            passengers.Enqueue(bailingPassenger);

            if (lives <= 0)
            {
                gameManager.EndGame();
            }
        }

        immunityTimeLeft -= Time.deltaTime;
        lastPos = transform.position;
    }

    void LaunchWinners()
    {
        winningPassengerTimeLeft -= Time.deltaTime;
        //Debug.Log("Winning!");

        if (winningPassengerTimeLeft <= 0)
        {
            winningPassengers[winningPassengerIndex].transform.position = new Vector3(transform.position.x + Random.Range(PersonStartX, -PersonStartX), transform.position.y + .2f, transform.position.z);
            winningPassengersScripts[winningPassengerIndex].Bail();
            winningPassengerIndex = (winningPassengerIndex + 1) % numWinningPassengers;
            winningPassengerTimeLeft = winningPassengerCooldown;
        }
    }

    public void ResetBoat()
    {
        lives = startingLives;

        for (int i = 0; i < lives; i++)
        {
            GameObject person = passengers.Dequeue();
            person.transform.parent = transform;
            //person.transform.position = Vector3.zero;
            person.GetComponent<BailingPassenger>().Board();

            person.transform.localPosition = PersonPrefab.transform.localPosition;

            Vector3 pos = person.transform.localPosition;
            pos.x = PersonStartX + i * PersonSpacing;

            if (i >= lives / 2)
            {
                pos.x += MiddleSpacing;
            }

            person.transform.localPosition = pos;

            passengers.Enqueue(person);
        }
    }
}
