using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int scoreValue = 0;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        scoreText.text = "Score: " + scoreValue;
    }
}
