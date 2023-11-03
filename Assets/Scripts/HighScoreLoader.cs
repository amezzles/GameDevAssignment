using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreLoader : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI highScoreTimeText;

    private int highScore;
    private int highScoreTime;
    private System.TimeSpan timeSpan;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreTime = PlayerPrefs.GetInt("HighScoreTime", 0);

        timeSpan = System.TimeSpan.FromSeconds(highScoreTime);
        string highScoreTimeString = string.Format("{0:D2}:{1:D2}:{2:D2}",
            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        highScoreText.text = highScore.ToString();
        highScoreTimeText.text = highScoreTimeString;

    }

}
