using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Indexes")]
    public int levelIndex;
    
    public List<GameObject> levels;



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
        LoadLevel();
    }
    private void LoadLevel()
    {

        UIManager.Instance.StartFader();

        levelIndex = PlayerPrefs.GetInt("LevelNumber");
        if (levelIndex == levels.Count) levelIndex = 0;
        PlayerPrefs.SetInt("LevelNumber", levelIndex);
       

        UIManager.Instance.UpgradeLevelText();
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(false);
        }
        levels[levelIndex].SetActive(true);

        
        //ScoreManager.Instance.score=PlayerPrefs.GetInt("Score");
        //UIManager.Instance.UpgradeScoreText();
        GameManager.Instance.UpdateBallsPositions();
        GameManager.Instance.UpdateRequirement();
        GameManager.Instance.UpdateDoorPosition();
        GameManager.Instance.ResetTheLevel();
        GameManager.Instance.isGameEnd=false;
        
        
        
    }

    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("LevelNumber", levelIndex + 1);
        PlayerPrefs.SetInt("RealLevel", PlayerPrefs.GetInt("RealLevel", 0) + 1);
        LoadLevel();
    }

    public void RestartLevel()
    {
        UIManager.Instance.StartFader();
        GameManager.Instance.UpdateBallsPositions();
        GameManager.Instance.UpdateRequirement();
        GameManager.Instance.UpdateDoorPosition();
        GameManager.Instance.ResetTheLevel();
        StartCoroutine(GameEndingTrue());
        
    }

    private IEnumerator GameEndingTrue()
    {
        yield return new WaitForSeconds(3f);
        GameManager.Instance.isGameEnd=false;
    }
    
}
