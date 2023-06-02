using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip ExplosionSound,OpeningSound,BossHitSound,PassSound,WallSound,ReleaseSound,CollectSound,SawSound,ButtonSound;

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
        EventManager.AddHandler(GameEvent.OnWall,OnWall);
        EventManager.AddHandler(GameEvent.OnFingerRelease,OnFingerRelease);
        EventManager.AddHandler(GameEvent.OnCollectGold,OnCollectGold);
        EventManager.AddHandler(GameEvent.OnSaw,OnSaw);
        EventManager.AddHandler(GameEvent.OnButtonClicked,OnButtonClicked);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossDieParticle,OnBossDieParticle);
        EventManager.RemoveHandler(GameEvent.OnHitBoss,OnHitBoss);
        EventManager.RemoveHandler(GameEvent.OnPassBetweenPoints,OnPassBetweenPoints);
        EventManager.RemoveHandler(GameEvent.OnWall,OnWall);
        EventManager.RemoveHandler(GameEvent.OnFingerRelease,OnFingerRelease);
        EventManager.RemoveHandler(GameEvent.OnCollectGold,OnCollectGold);
        EventManager.RemoveHandler(GameEvent.OnSaw,OnSaw);
        EventManager.RemoveHandler(GameEvent.OnButtonClicked,OnButtonClicked);
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

    private void OnWall()
    {
        effectSource.PlayOneShot(WallSound);
    }

    private void OnFingerRelease()
    {
        effectSource.PlayOneShot(ReleaseSound);
    }

    private void OnCollectGold()
    {
        effectSource.PlayOneShot(CollectSound);
    }

    private void OnSaw()
    {
        effectSource.PlayOneShot(SawSound);
    }

    private void OnButtonClicked()
    {
        effectSource.PlayOneShot(ButtonSound);
    }
}
