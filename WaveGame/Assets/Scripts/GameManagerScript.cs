using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject audioManager;
    public GameObject collectibleGod;

    public float lowerRangeSeconds;
    public float higherRangeSeconds;

    private float timeCounter;
    public float sendNextTime;
    public int collCounter;
    public List<GameObject> collectibles;
    private CollectibleScript cScript;
    private Vector3 newStart;
	// Use this for initialization
	void Start () {
        GameObject c1 = Instantiate(collectibleGod);
        GameObject c2 = Instantiate(collectibleGod);
        GameObject c3 = Instantiate(collectibleGod);
        GameObject c4 = Instantiate(collectibleGod);
        GameObject c5 = Instantiate(collectibleGod);

        collectibles.Add(c1);
        collectibles.Add(c2);
        collectibles.Add(c3);
        collectibles.Add(c4);
        collectibles.Add(c5);

        timeCounter = 0;
        collCounter = 0;
        sendNextTime = Random.Range(lowerRangeSeconds, higherRangeSeconds);


        cScript = GetComponent<CollectibleScript>();


    }

    // Update is called once per frame
    void Update () {
       // GameObject c;
		if(timeCounter >= sendNextTime)
        {
            timeCounter = 0;
            newStart = collectibles[collCounter].transform.position;
            collectibles[collCounter].transform.position = new Vector3(10, newStart.y, newStart.z);
            sendNextTime = Random.Range(lowerRangeSeconds, higherRangeSeconds);
            //         print(collectibles[2].GetComponent<CollectibleScript>().speed);
            //           print(collectibles[2].GetComponent<CollectibleScript>().higherRangeSpeed);

            //        print(collectibles[2].GetComponent<CollectibleScript>().lowerRangeSpeed);
            //       collectibles[1].GetComponent<CollectibleScript>().speed = Random.Range(GetComponent<CollectibleScript>().lowerRangeSpeed, GetComponent<CollectibleScript>().higherRangeSpeed);
            collectibles[collCounter].GetComponent<CollectibleScript>().isMoving = true;
            collCounter++;
            if (collCounter >= collectibles.Count)
            {
                collCounter = 0;
            }
        }
        timeCounter += Time.deltaTime;
	}
}
