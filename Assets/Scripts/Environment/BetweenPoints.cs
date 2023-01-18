using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenPoints : Obstacleable
{
    [SerializeField] private int customId;

    private GameManager gameManager;
    private CameraManager cameraManager;
    private SoundManager soundManager;

    void Start()
    {
        gameManager=GameManager.Instance;
        cameraManager=CameraManager.Instance;
        soundManager=SoundManager.Instance;
    }
    public BetweenPoints()
    {
        interactionTag="Ball";
    }
    internal override void DoAction(Player player)
    {
        if(gameManager.canCollide && customId==player.id && gameManager.RequirementNumber>0)
        {
            gameManager.ChangeRequirement(-1);
            gameManager.UpdateProgress();
            cameraManager.ShakeIt();
            soundManager.Play("Tick");
        }

        if(gameManager.RequirementNumber==0) gameManager.Door.SetActive(true);
    }

    
}
