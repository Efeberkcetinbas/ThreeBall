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
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnHitBoss,OnHitBoss);
        EventManager.RemoveHandler(GameEvent.OnSpawnWeapon,OnSpawnWeapon);
        EventManager.RemoveHandler(GameEvent.OnBossDie,OnBossDie);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnHitBoss()
    {
        Noise(amplitudeGain,frequencyGain,shakeTime);
    }

    private void OnSpawnWeapon()
    {
        Noise(5,5,0.1f);
    }

    private void OnBossDie()
    {
        Noise(1,1,3);
        cm.Follow=boss;
        ChangeFieldOfView(3,1);
        StartCoroutine(CallParticleBroadcast());
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
        DOTween.To(() => mainCamera.fieldOfView, x => mainCamera.fieldOfView = x, newFieldOfView, duration).OnComplete(()=>{
            DOTween.To(() => mainCamera.fieldOfView, x => mainCamera.fieldOfView = x, oldFieldOfView, duration);
        });
    }
   
    public void ResetCamera(int resetAmount)
    {
        cm.m_Priority = resetAmount;
    }

    public void ChangeCameras(float fieldOfView, float duration,GameObject gameObject)
    {
        //gameObject.transform.rotation=Quaternion.Euler(0,180,0);
        /*cm2.m_Priority=15;
        DOTween.To(() => cm2.m_Lens.FieldOfView, x => cm2.m_Lens.FieldOfView = x, fieldOfView, duration);*/
        //cmCamera.DOLocalMove(new Vector3(xPos, yPos, zPos), duration);
        //cmCamera.DOLocalRotate(new Vector3(xRot, yRot, 0), duration);
    }

    public void ChangeCameraAngle(float fieldOfView, float duration, float xPos, float yPos, float zPos, float xRot, float yRot, int priority)
    {
        /*cm2Camera.DOMove(new Vector3(xPos, yPos, zPos), duration).OnComplete(()=> {
           // DOTween.To(() => cm2.m_Priority, x => cm2.m_Priority = x, priority, duration);
        });
        cm2Camera.DORotate(new Vector3(xRot, yRot, 0), duration);*/
        /*DOTween.To(() => cm2.m_Lens.FieldOfView, x => cm2.m_Lens.FieldOfView = x, fieldOfView, duration);
        DOTween.To(() => cm2.m_Priority, x => cm2.m_Priority = x, priority, duration);*/
    }


    #region CameraShaker

    public void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermediatePosition = mainCamera.transform.position;
        cameraIntermediatePosition.x += cameraShakingOffsetX;
        cameraIntermediatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermediatePosition;
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
    #endregion    
}
