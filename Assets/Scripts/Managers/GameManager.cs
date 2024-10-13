using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
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
    public int MoveNumber;
    public string BossName;
    

    [Header("Open/Close")]
    [SerializeField] private GameObject[] open_close;


    private int tempRequirementNumber;
    
    private float progressNumber=0;

    [Header("Line Collisions")]
    public List<GameObject> LinesCol=new List<GameObject>(); 
    public bool canCollide=false;
    public bool success=false;
    public bool isWall=false;


    public ParticleSystem[] fireworks;

    

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

    public void PlayFireWorks()
    {
        for (int i = 0; i < fireworks.Length; i++)
        {
            fireworks[i].Play();
        }
    }

    public void ResetTheLevel()
    {
        ResetSuccessPanel();
        OpenClose(open_close,true);
        failPanel.SetActive(false);
        UIManager.Instance.UpdateProgressBar(0,0.1f);
        progressNumber=0;
        canCollide=false;
        success=false;
        UIManager.Instance.SuccessButton.transform.DOScale(Vector3.zero,0.1f).OnComplete(()=>UIManager.Instance.SuccessButton.SetActive(false));
        
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
        MoveNumber=FindObjectOfType<MoveRequirement>().moveNumber;
        UIManager.Instance.UpgradeMoveNumber(MoveNumber);
        tempRequirementNumber=RequirementNumber;
        BossName=FindObjectOfType<RequirementControl>().bossName;
        UIManager.Instance.UpgradeBossNameText();
    }

    public void UpdateMoveNumber(int useMove)
    {
        MoveNumber+=useMove;
        UIManager.Instance.UpgradeMoveNumber(MoveNumber);
        CheckMoveNumber();
    }

    private void CheckMoveNumber()
    {
        if(MoveNumber<=0)
        {
            EventManager.Broadcast(GameEvent.OnBallShoot);
            StartCoroutine(CheckIfSuccessOrNot());
        }
        else
            return;
    }

    private IEnumerator CheckIfSuccessOrNot()
    {
        yield return new WaitForSeconds(2);
        isGameEnd=true;
        if(!success)
            EventManager.Broadcast(GameEvent.OnSetMoveZero);


    }


    public void UpdateProgress()
    {
        float value=1/(float)tempRequirementNumber;
        //Merge seklinde artmasi gerekiyor
        progressNumber+=value;
        
        UIManager.Instance.UpdateProgressBar(progressNumber,0.5f);
    }

    public int ChangeRequirement(int amount)
    {
        RequirementNumber+=amount;
        return RequirementNumber;
    }

    public void OpenSuccessMenu(bool station)
    {

        //SoundManager.Instance.Play("success");
        //Event firlat
        OpenClose(open_close,false);

        successPanel.SetActive(station);
        successPanel.transform.DOScale(Vector2.one*1.15f,0.5f).OnComplete(()=> {
            successPanel.transform.DOScale(Vector2.one,0.5f);
            StartCoroutine(OnIncreaseScore());
        });
    }

    public void OpenFailMenu()
    {
        failPanel.SetActive(true);
        failPanel.transform.DOScale(Vector2.one*1.15f,0.5f).OnComplete(()=> {
            failPanel.transform.DOScale(Vector2.one,0.5f);
        });
    }


    IEnumerator OnIncreaseScore()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.SuccessButton.SetActive(true);
        UIManager.Instance.SuccessButton.transform.DOScale(Vector3.one,0.5f);
    }

    private int GetScore()
    {
        return ScoreManager.Instance.score;
    }

    private void ChangeScore(int value)
    {
        ScoreManager.Instance.score=value;
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpgradeEndScoreText();
        
    }

    

    public void ResetSuccessPanel()
    {
        successPanel.transform.DOScale(Vector2.zero,0.2f);
        successPanel.SetActive(false);
    }

    private void MoveBallsEnd(Vector2 increase,Transform ball1,Transform ball2, Transform ball3)
    {
        ball1.DOMove(Vector2.zero,1f);
        ball2.DOMove(Vector2.zero+increase,1f);
        ball3.DOMove(Vector2.zero-increase,1f);
    }

    

}
