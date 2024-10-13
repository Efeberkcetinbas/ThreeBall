using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Texts")]
    public TextMeshProUGUI[] LevelText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BossNameText;
    public TextMeshProUGUI MoveText;

    [Header("Images")]
    public Image progressImage;

    public RectTransform fader;

    [Header("Game End")]
    public TextMeshProUGUI endTimer;
    public TextMeshProUGUI endScore;

    public GameObject SuccessButton;


    void Awake()
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


    public void UpgradeLevelText()
    {
        LevelText[0].text = "Level " + (PlayerPrefs.GetInt("RealLevel") + 1).ToString() + " Completed";
        LevelText[1].text = "Level " + (PlayerPrefs.GetInt("RealLevel") + 1).ToString();
        LevelText[2].text = "Level " + (PlayerPrefs.GetInt("RealLevel") + 1).ToString() + " FAIL !";
    }

    public void UpgradeBossNameText()
    {
        BossNameText.text=GameManager.Instance.BossName;
    }

    public void UpgradeScoreText()
    {
        ScoreText.text= ScoreManager.Instance.score.ToString();
    }

    public void UpgradeEndScoreText()
    {
        endScore.text= "Score " + ScoreManager.Instance.score.ToString();
        UpgradeScoreText();
    }
    public void UpdateProgressBar(float value,float duration)
    {
        progressImage.DOFillAmount(value,duration);
    }

    public void UpgradeMoveNumber(int moveNumber)
    {
        MoveText.SetText("Move: " + moveNumber);
    }

    public void StartFader()
    {
        fader.gameObject.SetActive(true);

        fader.DOScale(new Vector3(3,3,3),0.75f).OnComplete(()=>{
            fader.DOScale(Vector3.zero,0.75f).OnComplete(()=>fader.gameObject.SetActive(false));
        });
    }
}
