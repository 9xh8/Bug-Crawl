using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Borders : MonoBehaviour {

    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float dieShakeIntensity;
    [SerializeField] private float dieShakeTime;

    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Rigidbody2D>()) {
            cameraShake.Shake(dieShakeIntensity, dieShakeTime);
            sceneTransition.ReloadCurrentScene();
        }
    }
}
