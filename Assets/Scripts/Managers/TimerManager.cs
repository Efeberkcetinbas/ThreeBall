using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public GameManager gameManager;

    private bool oneTime=false;


    void ScaleUpText()
    {
        timeText.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.25f).OnComplete(()=>timeText.transform.DOScale(new Vector3(1,1,1),0.25f));
    }

    void Update()
    {
        if (gameManager.timerIsRunning)
        {
            if (gameManager.RemainingTime > 0)
            {
                gameManager.RemainingTime -= Time.deltaTime;
                DisplayTime(gameManager.RemainingTime);
                //O dan büyükse azaltıyor.
            }
            else
            {
                gameManager.RemainingTime = 0;
                gameManager.timerIsRunning = false;
                DisplayTime(gameManager.RemainingTime);
                Time.timeScale = 0f;
                oneTime=true;
                if(oneTime)
                    Debug.Log("FAILLL");
                //bu durumda ise ana menuye dönücek.
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
}
