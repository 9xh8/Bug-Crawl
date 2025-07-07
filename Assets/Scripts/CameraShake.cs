using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private CinemachineVirtualCamera activeVirtualCamera;

    public void Shake(float intensity, float duration) {
        ResetAllCameras();

        activeVirtualCamera = GetActiveVirtualCamera();
        if (activeVirtualCamera == null) return;

        var perlin = activeVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin == null) return;

        perlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimer = duration;
        shakeTimerTotal = duration;
    }

    private void ResetAllCameras() {
        CinemachineVirtualCamera[] vcams = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (var vcam in vcams) {
            var perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (perlin != null) {
                perlin.m_AmplitudeGain = 0f;
            }
        }
    }

    private void Update() {
        if (shakeTimer > 0f) {
            shakeTimer -= Time.unscaledDeltaTime; // use unscaled if you want to shake during pause

            if (shakeTimer <= 0f && activeVirtualCamera != null) {
                var perlin = activeVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                if (perlin != null)
                    perlin.m_AmplitudeGain = 0f;
            }
        }
    }

    private CinemachineVirtualCamera GetActiveVirtualCamera() {
        // Finds the active camera with the highest priority
        CinemachineVirtualCamera[] vcams = FindObjectsOfType<CinemachineVirtualCamera>();
        CinemachineVirtualCamera active = null;
        int highestPriority = int.MinValue;

        foreach (var vcam in vcams) {
            if (vcam.Priority > highestPriority) {
                highestPriority = vcam.Priority;
                active = vcam;
            }
        }

        return active;
    }
}
