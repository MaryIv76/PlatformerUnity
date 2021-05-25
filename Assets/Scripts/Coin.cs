using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private Character character;
    private AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        character = FindObjectOfType<Character>();
        audioSource = character.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.ChangeScore(coinValue);
            audioSource.PlayOneShot(audioClip);
        }
    }
}
