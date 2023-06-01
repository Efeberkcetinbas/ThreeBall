using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public CinemachineVirtualCamera cm;
    public Camera mainCamera;

    public Transform cmCamera;
    public Transform player,boss;


    Vector3 cameraInitialPosition;

    [Header("Shake Control")]
    public float shakeMagnitude = 0.05f;
    public float shakeTime = 0.5f;
    public float amplitudeGain=1;
    public float frequencyGain=1;
    private CinemachineBasicMultiChannelPerlin noise;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnHitBoss,OnHitBoss);
        EventManager.AddHandler(GameEvent.OnSpawnWeapon,OnSpawnWeapon);
        EventManager.AddHandler(GameEvent.OnBossDie,OnBossDie);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnBossDieParticle,OnBossDieParticle);
        EventManager.AddHandler(GameEvent.OnSaw,OnSaw);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnHitBoss,OnHitBoss);
        EventManager.RemoveHandler(GameEvent.OnSpawnWeapon,OnSpawnWeapon);
        EventManager.RemoveHandler(GameEvent.OnBossDie,OnBossDie);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnBossDieParticle,OnBossDieParticle);
        EventManager.RemoveHandler(GameEvent.OnSaw,OnSaw);
    }

    private void OnHitBoss()
    {
        Noise(amplitudeGain,frequencyGain,shakeTime);
    }

    private void OnSaw()
    {
        Noise(2,2,0.2f);
        ChangeFieldOfViewHit(4,5,0.2f);
    }

    private void OnSpawnWeapon()
    {
        Noise(1,1,0.1f);
    }

    private void OnBossDie()
    {
        cm.Follow=boss;
        ChangeFieldOfView(3,1);
        StartCoroutine(CallParticleBroadcast());
    }

    private void OnBossDieParticle()
    {
        Noise(5,5,1);
    }

    private void OnNextLevel()
    {
        cm.Follow=player;
        ChangeFieldOfView(5,0.1f);
    }

    private IEnumerator CallParticleBroadcast()
    {
        yield return new WaitForSeconds(1);
        EventManager.Broadcast(GameEvent.OnBossDieParticle);
    }

    private void Start() 
    {
        noise=cm.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        if(noise == null)
            Debug.LogError("No MultiChannelPerlin on the virtual camera.", this);
        else
            Debug.Log($"Noise Component: {noise}");
    }

    private void Noise(float amplitudeGain,float frequencyGain,float shakeTime) 
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
        StartCoroutine(ResetNoise(shakeTime));    
    }

    private IEnumerator ResetNoise(float duration)
    {
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;    
    }
    //Boss oldugunde
    public void ChangeFieldOfView(float fieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.OrthographicSize, x => cm.m_Lens.OrthographicSize = x, fieldOfView, duration);
    }

    

    //Hit Effect
    public void ChangeFieldOfViewHit(float newFieldOfView, float oldFieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.OrthographicSize, x => cm.m_Lens.OrthographicSize = x, newFieldOfView, duration).OnComplete(()=>{
            DOTween.To(() => cm.m_Lens.OrthographicSize, x => cm.m_Lens.OrthographicSize = x, oldFieldOfView, duration);
        });
    }
   
    


    
}
