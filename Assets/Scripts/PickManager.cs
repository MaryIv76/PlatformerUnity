using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickManager : MonoBehaviour
{
    private Character character;
    private Canvas canvas;

    private void Awake()
    {
        character = FindObjectOfType<Character>();
        canvas = gameObject.GetComponent<Canvas>();
    }

    private void Update()
    {
        if(character.HasPick)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
    }
}
