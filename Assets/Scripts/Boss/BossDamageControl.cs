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
    [SerializeField] private ParticleSystem deadparticle;
    [SerializeField] private GameObject shadow;

    [SerializeField] private List<ParticleSystem> textParticles=new List<ParticleSystem>();

    private int particleIndex;
    

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnHitBoss,OnHitBoss);
        EventManager.AddHandler(GameEvent.OnBossDieParticle,OnBossDieParticle);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnHitBoss,OnHitBoss);
        EventManager.RemoveHandler(GameEvent.OnBossDieParticle,OnBossDieParticle);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnBossDieParticle()
    {
        deadparticle.Play();
        spriteRenderer.enabled=false;
        shadow.SetActive(false);
    }

    private void OnNextLevel()
    {
        spriteRenderer.enabled=true;
        shadow.SetActive(true);
    }

    private void OnHitBoss()
    {
        StartCoroutine(ChangeColor());
        MakeRandomParticle();
        textParticles[particleIndex].Play();
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
        Destroy(coin,1);
    }

    private int MakeRandomParticle()
    {
        particleIndex=Random.Range(0,textParticles.Count);
        return particleIndex;
    }

    
}
