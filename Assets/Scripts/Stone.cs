using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private Character character;

    private AudioSource audioSource;

    public AudioClip audioClip;

    private void Awake()
    {
        character = FindObjectOfType<Character>();
        audioSource = character.GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (character.CheckPick() && (Mathf.Abs(character.transform.position.x - transform.position.x) > 0.85F) && (Mathf.Abs(character.transform.position.x - transform.position.x) < 3.2F))
        {
            Destroy(gameObject);
            audioSource.PlayOneShot(audioClip);
        }
    }
}
