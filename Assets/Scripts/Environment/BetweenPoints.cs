using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenPoints : Obstacleable
{
    [SerializeField] private int customId;

    private GameManager gameManager;

    void Start()
    {
        gameManager=GameManager.Instance;
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
            Debug.Log("WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
        }

        if(gameManager.RequirementNumber==0) gameManager.Door.SetActive(true);
    }

    
}
