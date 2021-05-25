using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 0.5f;

    public GameObject completeLevelUI;

    public bool levelPlants = false;
    public bool levelOrcs = false;
    public bool levelLabyrint = false;
    public bool levelFinal = false;

    private ScoreManager scoreManager;
    private ScoreOrcsManager scoreOrcsManager;
    private Enemy enemy;

    private Character character;
    private AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip audioClipAttack;
    public AudioClip audioClipNextLevel;
    public AudioClip audioClipFinal;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreOrcsManager = FindObjectOfType<ScoreOrcsManager>();
        enemy = FindObjectOfType<Enemy>();
        character = FindObjectOfType<Character>();
        audioSource = character.GetComponent<AudioSource>();
    }

    public void CompleteLevel()
    {
        if (levelPlants)
        {
            if(scoreManager.currentScore == 36)
            {
                completeLevelUI.SetActive(true);
                audioSource.PlayOneShot(audioClipNextLevel);
            }
            else
            {
                audioSource.PlayOneShot(audioClip);
            }
        }

        if (levelLabyrint)
        {
            if(scoreManager.currentScore == 112)
            {
                completeLevelUI.SetActive(true);
                audioSource.PlayOneShot(audioClipNextLevel);
            }
            else
            {
                audioSource.PlayOneShot(audioClip);
            }
        }

        if (levelOrcs)
        {
            if (scoreOrcsManager.score == 10)
            {
                completeLevelUI.SetActive(true);
                audioSource.PlayOneShot(audioClipNextLevel);
            }
            else
            {
                audioSource.PlayOneShot(audioClipAttack);
            }
        }

        if (levelFinal && enemy.IsDead)
        {
            completeLevelUI.SetActive(true);
            audioSource.PlayOneShot(audioClipFinal);
        }
    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
