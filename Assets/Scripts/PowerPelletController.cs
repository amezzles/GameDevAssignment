using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPelletController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Animator[] ghostAnimators = new Animator[4];
    public AudioSource music;
    public AudioClip scaredMusic;

    private void Start()
    {
        GameObject scoreManagerObj = GameObject.Find("ScoreManager");

        scoreManager = scoreManagerObj.GetComponent<ScoreManager>();

        for (int i = 0; i < 4; i++)
        {
            GameObject cactus = GameObject.Find("Cactus-" + (i + 1));
            ghostAnimators[i] = cactus.GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.IncrementScore(10);

            for (int i = 0; i < 4; i++)
            {
                ghostAnimators[i].SetBool("Scared", true);
            }

            music.clip = scaredMusic;
            music.Play();

            scoreManager.GhostTimer();

            Destroy(gameObject);
        }
    }
}
