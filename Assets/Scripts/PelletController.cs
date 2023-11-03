using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public AudioSource audioSource;
    public AudioClip eatingClip;

    private void Start()
    {
        GameObject scoreManagerObj = GameObject.Find("ScoreManager");
        GameObject pacStudent = GameObject.Find("Pac-Student");

        scoreManager = scoreManagerObj.GetComponent<ScoreManager>();
        audioSource = pacStudent.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.IncrementScore(10);
            audioSource.clip = eatingClip;
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
