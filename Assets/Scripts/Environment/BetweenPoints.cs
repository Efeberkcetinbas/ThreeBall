using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenPoints : Obstacleable
{
    [SerializeField] private int customId;
    [SerializeField] private ParticleSystem pastEffect;

    private GameManager gameManager;
    private CameraManager cameraManager;
    private SoundManager soundManager;
    private LevelManager levelManager;
    private LineRenderer lineRenderer;
    void Start()
    {
        gameManager=GameManager.Instance;
        cameraManager=CameraManager.Instance;
        soundManager=SoundManager.Instance;
        levelManager=LevelManager.Instance;

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
            gameManager.ChangeRequirement(-1);
            gameManager.UpdateProgress();
            cameraManager.ShakeIt();
            soundManager.Play("Tick");
            gameManager.canCollide=false;
            lineRenderer.enabled=true;
            StartCoroutine(EnabledFalse());
            DoPastEffect();
        }
        //gameManager.Door.SetActive(true);
        if(gameManager.RequirementNumber==0 && !gameManager.isGameEnd)
        {
            gameManager.success=true;
            gameManager.isGameEnd=true;
            StartCoroutine(NextLevel());

        }
    }

    private IEnumerator EnabledFalse()
    {
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled=false;
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        LevelManager.Instance.LoadNextLevel();
    }

    private void DoPastEffect()
    {
        pastEffect.Play();
    }
    
}
