using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BossDamageControl : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject increaseScorePrefab;
    [SerializeField] private Transform pointPos;
    [SerializeField] private ParticleSystem particle;
    

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
        StartCoroutine(ChangeColor());
        
        //Particle Effect olsun, slash
        //xp ciksin uzerinden
    }

    private IEnumerator ChangeColor()
    {
        spriteRenderer.color=Color.red;
        particle.Play();
        //StartPointMove();
        transform.DOScale(Vector3.one/1.3f,0.2f).OnComplete(()=>{
            transform.DOScale(Vector3.one,0.2f);
        });

        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color=Color.white;
    }

    private void StartPointMove()
    {
        GameObject coin=Instantiate(increaseScorePrefab,pointPos.transform.position,increaseScorePrefab.transform.rotation);
        //coin.transform.DOLocalJump(coin.transform.localPosition,1,1,1,false);
        /*coin.transform.GetChild(0).GetComponent<TextMeshPro>().color=Color.red;
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().text="XP " ;
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));*/
        Destroy(coin,1);
    }

    
}
