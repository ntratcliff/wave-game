using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveButtonScript : MonoBehaviour {


    //Control values for player input buttons
    //Player 1 is left, Player 2 is right
    //Numbering handled in the scene

    public bool increaseForce;
    public bool decreaseForce;
    public float playerNum;
    private float inputForce;
    private bool hoveredOver;
    public float InputForce
    {
        get
        {
            return inputForce;
        }
    }
        
	// Use this for initialization
	void Start () {
        //setting the initial values for player input buttons
        hoveredOver = false;
        inputForce = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

        //Scales based off of holding down currently. Can adjust for time once we start tinkering with it

        if (playerNum == 1)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {

                increaseForce = true;
                decreaseForce = false;
            //    if (hoveredOver)
            //   {
            /*
                    inputForce++;
                    if (inputForce > 100)
                    {
                        inputForce = 100;
                    }
                    if (inputForce != 100)
                    {
                        this.transform.localScale += new Vector3(0.01f, 0, 0.01f);

                    }
                    */
            //    }

            }
            else
            {
                increaseForce = false;
                decreaseForce = true;
             /*   inputForce--;
                if (inputForce < 0)
                {
                    inputForce = 0;
                }
                if (inputForce != 0)
                {
                    this.transform.localScale -= new Vector3(0.01f, 0, 0.01f);
                }
                */
            }

        }
        if (playerNum == 2)
        {

            if (Input.GetKey(KeyCode.RightShift))
            {

                increaseForce = true;
                decreaseForce = false;
                /*
              //  if (hoveredOver)
              //  {
                    inputForce++;
                    if (inputForce > 100)
                    {
                        inputForce = 100;
                    }
                    if (inputForce != 100)
                    {
                        this.transform.localScale += new Vector3(0.01f, 0, 0.01f);

                    }
               // }*/

            }
            else
            {
                increaseForce = false;
                decreaseForce = true;
                /*
                inputForce--;
                if (inputForce < 0)
                {
                    inputForce = 0;
                }
                if (inputForce != 0)
                {
                    this.transform.localScale -= new Vector3(0.01f, 0, 0.01f);
                }
                */
            }

        }



        //Subtracted when not held down for smoothing right now
        //Make it reset to 0 instantly?

        //Extreme ends capping - double check
        if (inputForce < 0)
        {
            inputForce = 0;
        }
        if(inputForce > 100)
        {
            inputForce = 100;
        }

        //Debugging line
        print(gameObject.name + " force is " + inputForce);
	}

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

}
