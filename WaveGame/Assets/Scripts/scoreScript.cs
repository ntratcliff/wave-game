using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public static int score;
    Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void FixedUpdate()
    {
        text.text = "Score: " + score;
        score++;
    }
}
