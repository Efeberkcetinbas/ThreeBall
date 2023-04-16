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
            soundManager.Play("Tick");
            gameManager.canCollide=false;
            lineRenderer.enabled=true;
            player.particle.Play();
            StartCoroutine(EnabledFalse());
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
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().text=" + " + 1.ToString();
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(coin,2);
    } 

   
    
}
