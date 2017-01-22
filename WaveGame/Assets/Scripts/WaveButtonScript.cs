using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveButtonScript : MonoBehaviour {


    //Control values for player input buttons
    //Player 1 is left, Player 2 is right
    //Numbering handled in the scene

    public enum PlayerNum
    {
        Player_1,
        Player_2
    };

    public  float maxCap;
    public float minCap;
    public float scaleForce;

    [HideInInspector]
    public bool increaseForce;
    [HideInInspector]
    public bool decreaseForce;
    public PlayerNum playerNum;
    public float inputForceP1;
    public float inputForceP2;
    private bool hoveredOver;
    WaterManager water;
    Camera cam;
    float currAspect;

    int minLeftNode = 0;
    int minRightNode = 0;

    public float InputForceP1
    {
        get
        {
            return inputForceP1;
        }
    }
    public float InputForceP2
    {
        get
        {
            return inputForceP2;
        }
    }

    // Use this for initialization
    void Start () {
        //setting the initial values for player input buttons
        hoveredOver = false;
        inputForceP1 = 0;
        inputForceP2 = 0;
        water = GameObject.FindGameObjectWithTag("Water").GetComponent<WaterManager>();

        if (!water)
        {
            Debug.LogError("Can't find the water");
        }

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        currAspect = cam.aspect;

        FindEdgeWaves();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (cam.aspect != currAspect)
        {
            FindEdgeWaves();
        }

        //Scales based off of holding down currently. Can adjust for time once we start tinkering with it

        if (playerNum == PlayerNum.Player_1)
        {

            if (Input.GetAxis("Wave1") > 0)
            {

                increaseForce = true;
                decreaseForce = false;
                inputForceP1 += scaleForce * Time.deltaTime;
                if (inputForceP1 > maxCap)
                {
                    inputForceP1 = maxCap;
                }
                if (inputForceP1 != maxCap)
                {
                    this.transform.localScale += new Vector3(0.01f, 0, 0.01f);
                }

                water.AddForce(inputForceP1 / 2, minLeftNode - 1);//water.NumNodes / 3);
            }
            else
            {
                increaseForce = false;
                decreaseForce = true;
                inputForceP1 = 0; //-= scaleForce* Time.deltaTime;
                if (inputForceP1 < minCap)
                {
                    inputForceP1 = minCap;
                }
                if (inputForceP1 != minCap)
                {
                    this.transform.localScale -= new Vector3(0.01f, 0, 0.01f);
                }
            }

        }
        if (playerNum == PlayerNum.Player_2)
        {

            if (Input.GetAxis("Wave2") > 0)
            {

                inputForceP2 += scaleForce * Time.deltaTime;
                if (inputForceP2 > maxCap)
                {
                    inputForceP2 = maxCap;
                }
                if (inputForceP2 != maxCap)
                {
                    this.transform.localScale += new Vector3(0.01f, 0, 0.01f);

                }

                water.AddForce(inputForceP2 / 2, minRightNode + 1);// water.NumNodes / 3 * 2);
            }
            else 
            {
                Debug.Log("Adding the force");
                increaseForce = false;
                decreaseForce = true;
                inputForceP2 = 0;//-= scaleForce * Time.deltaTime;
                if (inputForceP2 < minCap)
                {
                    inputForceP2 = minCap;
                }
                if (inputForceP2 != minCap)
                {
                    this.transform.localScale -= new Vector3(0.01f, 0, 0.01f);
                }
            }

        }



        //Subtracted when not held down for smoothing right now
        //Make it reset to 0 instantly?

        //Extreme ends capping - double check
        if (inputForceP1 < minCap)
        {
            inputForceP1 = minCap;
        }
        if(inputForceP1 > maxCap)
        {
            inputForceP1 = maxCap;
        }
        if (inputForceP2 < minCap)
        {
            inputForceP2 = minCap;
        }
        if (inputForceP2 > maxCap)
        {
            inputForceP2 = maxCap;
        }
        //Debugging line
        //print(gameObject.name + " force is " + inputForce);
    }
    /*
    void FixedUpdate()
    {
        
        if (increaseForce)
        {
            inputForce++;
            if (inputForce > 100)
            {
                inputForce = 100;
            }
            if (inputForce != 100)
            {
                this.transform.localScale += new Vector3(0.01f, 0, 0.01f);

            }
        }
        if(decreaseForce)
        {
            inputForce--;
            if (inputForce < 0)
            {
                inputForce = 0;
            }
            if (inputForce != 0)
            {
                this.transform.localScale -= new Vector3(0.01f, 0, 0.01f);
            }
        }
    }
    */
    //Raycast hitting is boring
    void OnMouseOver()
    {
        print(gameObject.name);
        hoveredOver = true;
      
    }
    
    //Raycast hitting is boring
    void OnMouseExit()
    {
        print(gameObject.name + "exit");
        hoveredOver = false;
    }

    void FindEdgeWaves()
    {
        //Debug.Log("The window size changed");
        float minLeftDist = int.MaxValue;
        float minRightDist = int.MaxValue;

        for (int i = 0; i < water.Positions.Length; i++)
        {
            float dist = Mathf.Abs(cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - water.Positions[i].x);
            if (dist < minLeftDist)
            {
                minLeftDist = dist;
                minLeftNode = i;
            }

            dist = Mathf.Abs(cam.ViewportToWorldPoint(new Vector3(1, 1, 0)).x - water.Positions[i].x);
            if (dist < minRightDist)
            {
                minRightDist = dist;
                minRightNode = i;
            }
        }

        currAspect = cam.aspect;
    }
}
