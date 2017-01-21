using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloMo : MonoBehaviour
{
    bool isSlow = false;
    [Range(0, 1)]
    [SerializeField]
    float slowFactor = .2f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P) && Debug.isDebugBuild)
        {
            if (isSlow)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = slowFactor;
            }

            isSlow = !isSlow;
        }
	}
}
