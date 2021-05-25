using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    public int currentScore;

    public int maxScore;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int coinValue)
    {
        currentScore += coinValue;
        text.text = currentScore.ToString() + " / " + maxScore;
    }
}
