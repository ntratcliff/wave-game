using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject audioManager;
    public GameObject collectibleGod;
    public GameObject cloudGod;

    public float lowerRangeSeconds;
    public float higherRangeSeconds;
    public float lowerCloudRangeSeconds;
    public float higherCloudRangeSeconds;

    private float timeCounter;
    public float sendNextTime;
    public int collCounter;

    public int cloudCounter;
    private float cloudTimeCounter;
    public float cloudSendNextTime;

    public List<GameObject> collectibles;
    public List<GameObject> clouds;
    private CollectibleScript cScript;
    private CloudScript cloudScript;
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

        GameObject cl1 = Instantiate(cloudGod);
        GameObject cl2 = Instantiate(cloudGod);
        GameObject cl3 = Instantiate(cloudGod);
        GameObject cl4 = Instantiate(cloudGod);
        GameObject cl5 = Instantiate(cloudGod);
        GameObject cl6 = Instantiate(cloudGod);
        GameObject cl7 = Instantiate(cloudGod);
        GameObject cl8 = Instantiate(cloudGod);
        GameObject cl9 = Instantiate(cloudGod);
        GameObject cl10 = Instantiate(cloudGod);
        GameObject cl11 = Instantiate(cloudGod);
        GameObject cl12 = Instantiate(cloudGod);
        GameObject cl13 = Instantiate(cloudGod);
        GameObject cl14 = Instantiate(cloudGod);
        GameObject cl15 = Instantiate(cloudGod);
        GameObject cl16 = Instantiate(cloudGod);
        GameObject cl17 = Instantiate(cloudGod);
        GameObject cl18 = Instantiate(cloudGod);
        GameObject cl19 = Instantiate(cloudGod);
        GameObject cl20 = Instantiate(cloudGod);

        clouds.Add(cl1);
        clouds.Add(cl2);
        clouds.Add(cl3);
        clouds.Add(cl4);
        clouds.Add(cl5);
        clouds.Add(cl6);
        clouds.Add(cl7);
        clouds.Add(cl8);
        clouds.Add(cl9);
        clouds.Add(cl10);
        clouds.Add(cl11);
        clouds.Add(cl12);
        clouds.Add(cl13);
        clouds.Add(cl14);
        clouds.Add(cl15);
        clouds.Add(cl16);
        clouds.Add(cl17);
        clouds.Add(cl18);
        clouds.Add(cl19);
        clouds.Add(cl20);

        timeCounter = 0;
        collCounter = 0;
        cloudTimeCounter = 0;
        cloudCounter = 0;
        cloudSendNextTime = Random.Range(lowerCloudRangeSeconds, higherCloudRangeSeconds);
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
            collectibles[collCounter].GetComponent<CollectibleScript>().rerollHeight();
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

        //Cloud management software
        if (cloudTimeCounter > cloudSendNextTime)
        {
            cloudTimeCounter = 0;
            newStart = clouds[cloudCounter].transform.position;
            clouds[cloudCounter].GetComponent<CloudScript>().rerollHeight();
            clouds[cloudCounter].GetComponent<CloudScript>().rerollScale();

            clouds[cloudCounter].transform.position = new Vector3(10, newStart.y, newStart.z);
            cloudSendNextTime = Random.Range(lowerCloudRangeSeconds, higherCloudRangeSeconds);
            //         print(collectibles[2].GetComponent<CollectibleScript>().speed);
            //           print(collectibles[2].GetComponent<CollectibleScript>().higherRangeSpeed);

            //        print(collectibles[2].GetComponent<CollectibleScript>().lowerRangeSpeed);
            //       collectibles[1].GetComponent<CollectibleScript>().speed = Random.Range(GetComponent<CollectibleScript>().lowerRangeSpeed, GetComponent<CollectibleScript>().higherRangeSpeed);
            clouds[cloudCounter].GetComponent<CloudScript>().isMoving = true;
            cloudCounter++;
            if (cloudCounter >= clouds.Count)
            {
                cloudCounter = 0;

            }
        }

        cloudTimeCounter += Time.deltaTime;
        timeCounter += Time.deltaTime;
	}
}
