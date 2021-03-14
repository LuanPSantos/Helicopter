using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineMachineShake : MonoBehaviour
{
    public float intensity = 3f;
    public float shakeDuration = 0.2f;

    private float currentTimeCount = 0;

    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    void Awake()
    {
        CinemachineVirtualCamera cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
         HelicopterBehaviour.onCrash += ShakeCamera;
    }

    private void OnDestroy()
    {
        HelicopterBehaviour.onCrash -= ShakeCamera;
    }

    void Update()
    {
        if(currentTimeCount > 0f)
        {
            currentTimeCount -= Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(intensity, 0f, 1 - currentTimeCount / shakeDuration);
        }
    }

    private void ShakeCamera()
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        currentTimeCount = shakeDuration;
    }
}
