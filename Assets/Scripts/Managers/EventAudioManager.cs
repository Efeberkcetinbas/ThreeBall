using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip ExplosionSound,OpeningSound,BossHitSound,PassSound;

    AudioSource musicSource,effectSource;

    private bool hit;

    private void Start() {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = GameLoop;
        //musicSource.Play();
        effectSource = gameObject.AddComponent<AudioSource>();
        effectSource.volume=0.4f;
        //effectSource.PlayOneShot(OpeningSound);
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBossDieParticle,OnBossDieParticle);
        EventManager.AddHandler(GameEvent.OnHitBoss,OnHitBoss);
        EventManager.AddHandler(GameEvent.OnPassBetweenPoints,OnPassBetweenPoints);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossDieParticle,OnBossDieParticle);
        EventManager.RemoveHandler(GameEvent.OnHitBoss,OnHitBoss);
        EventManager.RemoveHandler(GameEvent.OnPassBetweenPoints,OnPassBetweenPoints);
    }

    private void OnBossDieParticle()
    {
        effectSource.PlayOneShot(ExplosionSound);
    }

    private void OnHitBoss()
    {
        effectSource.PlayOneShot(BossHitSound);
    }

    private void OnPassBetweenPoints()
    {
        effectSource.PlayOneShot(PassSound);
    }
}
