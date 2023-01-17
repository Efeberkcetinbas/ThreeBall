using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : Obstacleable
{

    public Finish()
    {
        interactionTag="Ball";
    }
    internal override void DoAction(Player player)
    {
        GameManager.Instance.isGameEnd=true;
        //GameManager.Instance.successPanel.SetActive(true);
        StartCoroutine(NextLevel());
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        LevelManager.Instance.LoadNextLevel();
    }
}
