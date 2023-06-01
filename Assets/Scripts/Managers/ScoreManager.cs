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

    private void Start() 
    {
        score=PlayerPrefs.GetInt("Score");
    }


    public void UpdateScore(int increaseScore)
    {
        score+=increaseScore;
        PlayerPrefs.SetInt("Score",score);
        UIManager.Instance.UpgradeScoreText();
    }
}
