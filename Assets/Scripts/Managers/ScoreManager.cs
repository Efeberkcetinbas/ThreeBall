using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    public int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void UpdateScore(int increaseScore)
    {
        score+=increaseScore;
        //DOTween.To(() => score, x => score = x, score+=increaseScore, 0.25f);
        PlayerPrefs.SetInt("Score",score);
        UIManager.Instance.UpgradeScoreText();
    }
}
