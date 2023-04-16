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
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ScoreText;
    //public TextMeshProUGUI RequirementText;

    [Header("Images")]
    public Image progressImage;

    public RectTransform fader;

    [Header("Game End")]
    public TextMeshProUGUI endTimer;
    public TextMeshProUGUI endScore;


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
        LevelText.text = "Level " + (PlayerPrefs.GetInt("RealLevel") + 1).ToString() + " Completed";
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

    public void UpdateRequirementText()
    {
        //RequirementText.text=GameManager.Instance.RequirementNumber.ToString();
    }

    public void UpdateEndTimer()
    {
        endTimer.text="+ " + Mathf.FloorToInt(GameManager.Instance.RemainingTime).ToString();
    }

    public void StartFader()
    {
        fader.gameObject.SetActive(true);

        fader.DOScale(new Vector3(3,3,3),1).OnComplete(()=>{
            fader.DOScale(Vector3.zero,1f).OnComplete(()=>fader.gameObject.SetActive(false));
        });
    }
}
