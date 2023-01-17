using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Ending")]
    public GameObject successPanel;
    public GameObject failPanel;
    public bool isGameEnd=false;

    [Header("Balls")]
    public Transform ball1,ball2,ball3;

    public Vector2 Pos1,Pos2,Pos3;

    [Header("Requirement")]
    public int RequirementNumber;

    private int tempRequirementNumber;
    private float progressNumber=0;

    [Header("Scripts")]
    public BallManager ballManager;
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
   

    public void ResetTheLevel()
    {
        successPanel.SetActive(false);
        failPanel.SetActive(false);
        UIManager.Instance.UpdateProgressBar(0,0.1f);
        progressNumber=0;
    }


    public void OpenClose(GameObject[] gameObjects,bool canOpen)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if(canOpen)
                gameObjects[i].SetActive(true);
            else
                gameObjects[i].SetActive(false);
        }
    }

    public void UpdateBallsPositions()
    {
        ballManager.StopBall();
        ballManager.StartCoroutine(ballManager.ResetBallSpeed(2.5f));

        Pos1=FindObjectOfType<BallInitialPoses>().pos1;
        Pos2=FindObjectOfType<BallInitialPoses>().pos2;
        Pos3=FindObjectOfType<BallInitialPoses>().pos3;

        ball1.localPosition=Pos1;
        ball2.localPosition=Pos2;
        ball3.localPosition=Pos3;
    }

    public void UpdateRequirement()
    {
        RequirementNumber=FindObjectOfType<RequirementControl>().requirementNumber;
        UIManager.Instance.UpdateRequirementText();
        tempRequirementNumber=RequirementNumber;
        Debug.Log("TEMP " + tempRequirementNumber);
    }

    public void UpdateProgress()
    {
        float value=1/(float)tempRequirementNumber;
        Debug.Log("VALUE : " + value);
        progressNumber+=value;
        
        UIManager.Instance.UpdateProgressBar(progressNumber,0.5f);
    }

    public int ChangeRequirement(int amount)
    {
        RequirementNumber+=amount;
        UIManager.Instance.UpdateRequirementText();
        return RequirementNumber;
    }

}
