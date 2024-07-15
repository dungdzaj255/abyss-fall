using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public float shakeIntensity = 1;
    public float shakeTime = 0.2f;

    private void Start() {
        StopShake();
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        cinemachineVirtualCamera = GameObject.FindWithTag("VTCamera").GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera() {
        CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = shakeIntensity;
        Invoke("StopShake", shakeTime);
    }
    public void StopShake() {
        CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
    }
}
