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
    public Transform ball1;
    public Transform ball2;
    public Transform ball3;

    public Vector2 Pos1,Pos2,Pos3;

    [Header("Requirement")]
    public int RequirementNumber;

    //Kaç kez swipelama hakkimiz var
    public int TurnNumber;
    //Her levelde ne kadar fazlasinin Turn Number olacagini secebiliyoruz
    public int IncreaseRightNumber;

    private int tempRequirementNumber;
    private float progressNumber=0;

    [Header("Line Collisions")]
    public List<GameObject> LinesCol=new List<GameObject>(); 
    public bool canCollide=false;
    public bool success=false;

    [Header("Door Control")]
    public GameObject Door;
    internal Transform DoorTransform;

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

    private void Start()
    {
        Application.targetFrameRate=60;
    }


    public void ResetTheLevel()
    {
        successPanel.SetActive(false);
        failPanel.SetActive(false);
        UIManager.Instance.UpdateProgressBar(0,0.1f);
        progressNumber=0;
        canCollide=false;
        success=false;
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

    public void LineOpenControl(int selected)
    {
        for (int i = 0; i < LinesCol.Count; i++)
        {
            LinesCol[i].SetActive(false);
        }

        LinesCol[selected].SetActive(true);
    }

    public void UpdateDoorPosition()
    {
        DoorTransform=FindObjectOfType<DoorPosition>().doorPos;
        Door.transform.position=DoorTransform.position;
        Door.SetActive(false);
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
        IncreaseRightNumber=FindObjectOfType<IncreaseTurnN>().increaseTurnNumber;
        UIManager.Instance.UpdateRequirementText();
        tempRequirementNumber=RequirementNumber;
        TurnNumber=RequirementNumber+IncreaseRightNumber;
        UIManager.Instance.UpdateTurnNumberText();        
    }

    public void UpdateProgress()
    {
        float value=1/(float)tempRequirementNumber;
        progressNumber+=value;
        
        UIManager.Instance.UpdateProgressBar(progressNumber,0.5f);
    }

    public int ChangeRequirement(int amount)
    {
        RequirementNumber+=amount;
        UIManager.Instance.UpdateRequirementText();
        return RequirementNumber;
    }

    public int ChangeTurnNumber(int amount)
    {
        TurnNumber+=amount;
        UIManager.Instance.UpdateTurnNumberText();
        return TurnNumber;
    }

}
