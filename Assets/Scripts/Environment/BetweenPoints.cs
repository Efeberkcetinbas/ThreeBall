using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BetweenPoints : Obstacleable
{
    [SerializeField] private int customId;

    [SerializeField] private GameObject increaseScorePrefab;
    [SerializeField] private Transform pointPos;

    private GameManager gameManager;
    private CameraManager cameraManager;
    private SoundManager soundManager;
    private LevelManager levelManager;
    private LineRenderer lineRenderer;
    private ScoreManager scoreManager;

    void Start()
    {
        gameManager=GameManager.Instance;
        cameraManager=CameraManager.Instance;
        soundManager=SoundManager.Instance;
        levelManager=LevelManager.Instance;
        scoreManager=ScoreManager.Instance;

        lineRenderer=GetComponent<LineRenderer>();
    }
    public BetweenPoints()
    {
        interactionTag="Ball";
    }
    internal override void DoAction(Player player)
    {
        if(gameManager.canCollide && customId==player.id && gameManager.RequirementNumber>0 && !gameManager.isGameEnd)
        {
            StartPointMove();
            scoreManager.UpdateScore(+1);
            gameManager.ChangeRequirement(-1);
            gameManager.UpdateProgress();
            cameraManager.ShakeIt();
            cameraManager.ChangeFieldOfViewHit(4f,5f,1f);
            EventManager.Broadcast(GameEvent.OnHitBoss);
            if(gameManager.isWall)
                gameManager.PlayFireWorks();
            
            gameManager.canCombo=true;
            gameManager.comboTime=3;

            if(gameManager.canCombo)
            {
                if(gameManager.comboAmount>1)
                {
                    gameManager.comboText.gameObject.SetActive(true);
                    gameManager.comboText.transform.localScale=Vector3.zero;
                    gameManager.comboText.transform.DOScale(Vector3.one*2f,0.2f).OnComplete(()=>{
                        gameManager.comboText.transform.DOScale(Vector3.one*1.25f,0.2f).OnComplete(()=>gameManager.comboText.gameObject.SetActive(false));
                    });
                }
                
                gameManager.comboAmount++;
                gameManager.comboText.SetText("x " + gameManager.comboAmount.ToString());
            }
            
            soundManager.Play("Tick");
            gameManager.canCollide=false;
            //lineRenderer.enabled=true;
            player.particle.Play();
            //StartCoroutine(EnabledFalse());
        }
        //gameManager.Door.SetActive(true);
        if(gameManager.RequirementNumber==0 && !gameManager.isGameEnd)
        {
            gameManager.success=true;
            gameManager.isGameEnd=true;
            //Burada success menu ac
            StartCoroutine(OpenSuccess());
        }
    }

    private IEnumerator OpenSuccess()
    {
        yield return new WaitForSeconds(2);
        gameManager.OpenSuccessMenu(true);
    }

    private IEnumerator EnabledFalse()
    {
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled=false;
    }

    private void StartPointMove()
    {
        GameObject coin=Instantiate(increaseScorePrefab,pointPos.transform.position,increaseScorePrefab.transform.rotation);
        coin.transform.DOLocalJump(coin.transform.localPosition,1,1,1,false);
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().text=" + " + (GameManager.Instance.AmounOfIncrease+GameManager.Instance.comboAmount).ToString();
        coin.transform.GetChild(1).GetComponent<SpriteRenderer>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(coin,2);
    } 

   
    
}
