using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float startShakeIntensity;
    private float shakeTimer;
    private float shakeTimerTotal;
    private bool usingCurve = false;
    private Transform originalFollowTarget;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        originalFollowTarget = cinemachineVirtualCamera.Follow;
    }

    public void ShakeCamera(float intensity, float time, bool isCurved)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startShakeIntensity = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
        usingCurve = isCurved;
    }



    public void ChangeFollowTarget(Transform target)
    {
        cinemachineVirtualCamera.Follow = target;
        cinemachineVirtualCamera.LookAt = target;
        cinemachineVirtualCamera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void ResetFollowTarget()
    {
        cinemachineVirtualCamera.Follow = originalFollowTarget;
        cinemachineVirtualCamera.LookAt = originalFollowTarget;
        cinemachineVirtualCamera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (!usingCurve)
            {
                if (shakeTimer <= 0f)
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }else{
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                    Mathf.Lerp(startShakeIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));
            }
        }



    }
}
