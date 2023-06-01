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
    private LevelManager levelManager;
    private LineRenderer lineRenderer;
    private ScoreManager scoreManager;

    void Start()
    {
        gameManager=GameManager.Instance;
        cameraManager=CameraManager.Instance;
        levelManager=LevelManager.Instance;
        scoreManager=ScoreManager.Instance;

        lineRenderer=GetComponent<LineRenderer>();
    }
    public BetweenPoints()
    {
        interactionTag="Ball";
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnHitBoss,OnHitBoss);    
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnHitBoss,OnHitBoss);
    }

    private void OnHitBoss()
    {

        gameManager.ChangeRequirement(-1);
        gameManager.UpdateProgress();

        if(gameManager.RequirementNumber<=0 && !gameManager.isGameEnd)
        {
            gameManager.success=true;
            gameManager.isGameEnd=true;
            EventManager.Broadcast(GameEvent.OnBossDie);
            //Burada success menu ac
            StartCoroutine(OpenSuccess());
        }
    }
    internal override void DoAction(Player player)
    {
        if(gameManager.canCollide && customId==player.id && gameManager.RequirementNumber>0 && !gameManager.isGameEnd)
        {
            StartPointMove();
            scoreManager.UpdateScore(+1);
            EventManager.Broadcast(GameEvent.OnSpawnWeapon);
            if(gameManager.isWall)
                gameManager.PlayFireWorks();
            
           
            
            EventManager.Broadcast(GameEvent.OnPassBetweenPoints);
            gameManager.canCollide=false;
            player.particle.Play();
        }
        
    }

    private IEnumerator OpenSuccess()
    {
        yield return new WaitForSeconds(4);
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
        coin.transform.GetChild(1).GetComponent<SpriteRenderer>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(coin,2);
    } 

   
    
}
