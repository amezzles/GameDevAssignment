using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ghostTimeText;
    public TextMeshProUGUI countDown;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOver;
    private float startCount;
    private float ghostTime = 0;
    private float timer;
    public AudioSource music;
    public AudioClip gameOverClip;
    public AudioClip normalMusic;
    public Animator[] ghostAnimators = new Animator[4];

    public Image[] lives = new Image[3];
    private int lifeCount = 3;

    private bool start = false;
    
    private int score = 0;

    private void Start()
    {
        ghostTimeText.enabled = false;
        gameOver.enabled = false;

        for (int i = 0; i < 4; i++)
        {
            GameObject cactus = GameObject.Find("Cactus-" + (i + 1));
            ghostAnimators[i] = cactus.GetComponent<Animator>();
        }

        startCount = 3;
    }

    private void Update()
    {
        if (startCount <= 3)
        {
            countDown.text = "3,";
            startCount -= Time.deltaTime;

            if (startCount <= 2)
            {
                countDown.text = "3, 2,";

                if (startCount <= 1)
                {
                    countDown.text = "3, 2, 1,";

                    if (startCount <= 0)
                    {
                        countDown.text = "GO!";
                        if (startCount <= -1)
                        {
                            start = true;
                        }
                    }
                }
            }
        }
        if (start == true && countDown.enabled == true)
        {
            timer = 0;
            startCount = 4;
            countDown.enabled = false;
        }

        if (timer >= 0 && start == true)
        {

            timer += Time.deltaTime;

            int seconds = Mathf.FloorToInt(timer % 60);
            int minutes = Mathf.FloorToInt((timer / 60) % 60);
            int hours = Mathf.FloorToInt(timer / 3600);

            timerText.text = string.Format("Timer: {0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
    

        if (ghostTime > 0)
        {
            ghostTime -= Time.deltaTime;
            DisplayTime(ghostTime);
            bool deadGhost = false;

            if (ghostTime <= 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (!ghostAnimators[i].GetCurrentAnimatorStateInfo(0).IsName("Dead"))
                    {
                        ghostAnimators[i].SetBool("Recovering", true);
                        ghostAnimators[i].SetBool("Scared", false);
                    } else
                    {
                        deadGhost = true;
                    }
                }
            }

            if (ghostTime <= 0)
            {
                ghostTime = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (!ghostAnimators[i].GetCurrentAnimatorStateInfo(0).IsName("Dead"))
                    {
                        ghostAnimators[i].SetBool("Recovering", false);
                    }
                }
                if (deadGhost == false)
                {
                    ghostTimeText.enabled = false;
                    music.clip = normalMusic;
                    music.Play();
                }
            }
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void IncrementScore(int number)
    {
        score += number;
        UpdateScoreText();
    }

    public void GhostTimer()
    {
        ghostTimeText.enabled = true;
        ghostTime = 10;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (ghostTimeText != null)
        {
            ghostTimeText.text = string.Format("Scared Ghost Timer: {0:00}", seconds);
        }
    }

    public bool GhostTimerActive()
    {
        if (ghostTime > 0)
        {
            return true;
        }
        return false;
    }

    public void LoseLife()
    {
        lives[lifeCount - 1].enabled = false;
        lifeCount -= 1;
    }

    public int getLifeCount()
    {
        return lifeCount;
    }

    public void GameOver()
    {
        gameOver.enabled = true;
        start = false;
        music.clip = gameOverClip;
        music.Play();
        music.loop = false;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);

            int timeAsInt = Mathf.FloorToInt(timer);
            PlayerPrefs.SetInt("HighScoreTime", timeAsInt);

            PlayerPrefs.Save();
        }
    }
}
