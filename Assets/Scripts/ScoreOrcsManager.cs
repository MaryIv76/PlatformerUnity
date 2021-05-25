using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreOrcsManager : MonoBehaviour
{
    public static ScoreOrcsManager instance;
    public TextMeshProUGUI text;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int orcsValue)
    {
        score += orcsValue;
        text.text = score.ToString() + " / 10";
    }
}
