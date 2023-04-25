using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    public TextMeshProUGUI timeText;
    public GameManager gameManager;


    private float tempRemainingTime;

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

    void ScaleUpText()
    {
        timeText.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.25f).OnComplete(()=>timeText.transform.DOScale(new Vector3(1,1,1),0.25f));
    }

    public void UpdateTempRemaining()
    {
        tempRemainingTime=gameManager.RemainingTime;
    }

    void Update()
    {
        if (gameManager.timerIsRunning)
        {
            if (gameManager.RemainingTime > 0)
            {
                gameManager.RemainingTime -= Time.deltaTime;
                //DisplayTime(gameManager.RemainingTime);
                DisplayCounterClock();
                //O dan büyükse azaltıyor.
            }
            else
            {
                gameManager.RemainingTime = 0;
                gameManager.OpenFailMenu();

                gameManager.timerIsRunning = false;
                DisplayCounterClock();
                //DisplayTime(gameManager.RemainingTime);
                //Time.timeScale = 0f;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); //60tan sonra dakikaya 1 ekliyor.

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //dakika saniye cinsinden gösteriyor.

    }

    public void DisplayCounterClock()
    {
        UIManager.Instance.UpdateTimerCounter(tempRemainingTime);
    }
}
