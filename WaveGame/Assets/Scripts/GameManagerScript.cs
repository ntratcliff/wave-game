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

    [HideInInspector]
    public Fading fader;
    public GameObject startText;
    public GameObject endText;
    public GameObject scoreText;
    public float fadeInTime = 2;
    public float fadeOutTime = 2;
    [HideInInspector]
    public bool gameEnded = false;
    [HideInInspector]
    public bool preGame = true;
    public Scoreboard score;
    public BoatScript boat;
    [HideInInspector]
    public AudioSource audio;
    private bool firstLoop = true;

    // Use this for initialization
    void Start ()
    {
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

        cScript = GetComponent<CollectibleScript>();

        fader = GameObject.FindObjectOfType<Fading>();
        audio = gameObject.GetComponent<AudioSource>();

        if (!fader)
        {
            Debug.LogError("Can't find the fader for the game manager");
        }

        if (!audio)
        {
            Debug.LogError("Can't find the audio source component on the game manager");
        }

        if (!startText)
        {
            GameObject.Find("Start Screen");
            Debug.LogError("Forgot to add the start text to the game manager");
        }

        if (!endText)
        {
            GameObject.Find("Game Over Screen");
            Debug.LogError("Forgot to add the game over text to the game manager");
        }

        if (!scoreText)
        {
            GameObject.Find("Score");
            Debug.LogError("Forgot to add the score text to the game manager");
        }

        if (!score)
        {
            GameObject.FindObjectOfType<Scoreboard>();
            Debug.LogError("Forgot to add the scoreboard to the game manager");
        }

        if (!boat)
        {
            GameObject.FindObjectOfType<BoatScript>();
            Debug.LogError("Forgot to add the boat to the game manager");
        }

        RestartGame();
        fader.FadeOut(scoreText, fadeOutTime);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetAxis("Wave1") > 0 && Input.GetAxis("Wave2") > 0)
        {
            if (preGame)
            {
                StartGame();
            }
            else if (gameEnded)
            {
                RestartGame();
            }
        }

        if (firstLoop)
        {
            Vector3 startCloudsPos;
            for (int i = 15; i < 20; i++)
            {
                clouds[i].GetComponent<CloudScript>().rerollScale();
                clouds[i].GetComponent<CloudScript>().rerollHeight();
                clouds[i].GetComponent<CloudScript>().isMoving = true;
                startCloudsPos = clouds[i].GetComponent<CloudScript>().transform.position;
                startCloudsPos = new Vector3(Random.Range(-4.0f, 4.0f), startCloudsPos.y, startCloudsPos.z);
                clouds[i].GetComponent<CloudScript>().transform.position = startCloudsPos;
            }
        }
        firstLoop = false;

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

        if (preGame || gameEnded)
        {
            return;
        }

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

        timeCounter += Time.deltaTime;
	}

    public void EndGame()
    {
        fader.FadeIn(endText, fadeInTime);
        gameEnded = true;
    }

    public void RestartGame()
    {
        fader.FadeOut(endText, fadeOutTime);
        gameEnded = false;
        boat.ResetBoat();

        timeCounter = 0;
        collCounter = 0;
        cloudTimeCounter = 0;
        cloudCounter = 0;
        cloudSendNextTime = Random.Range(lowerCloudRangeSeconds, higherCloudRangeSeconds);
        sendNextTime = Random.Range(lowerRangeSeconds, higherRangeSeconds);
        score.Reset();
        audio.PlayOneShot(audio.clip);
        Debug.Log("Fading out end text");
    }

    public void StartGame()
    {
        fader.FadeOut(startText, fadeOutTime);
        fader.FadeIn(scoreText, fadeInTime);
        preGame = false;
        audio.PlayOneShot(audio.clip);
    }
}
