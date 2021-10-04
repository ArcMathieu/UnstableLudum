using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score = 0;
    public TMP_Text textmeshPro;
    public void AddScore(int scoreToAdd) {
        score += scoreToAdd;
        textmeshPro.SetText("{0}", score);
    }
}
