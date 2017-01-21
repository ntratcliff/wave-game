using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public Text ScoreText;
    public Text ScoreShadow;

    private int score;
    public int Score
    {
        get { return Score; }
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = score.ToString();
        ScoreShadow.text = ScoreText.text;
    }

    /// <summary>
    /// Adds a single point to the scoreboard
    /// </summary>
    public void AddPoint()
    {
        score++;
    }
}
