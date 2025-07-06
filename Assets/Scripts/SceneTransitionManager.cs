using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour {

    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private float cutsceneFinishTime;

    private void Update() {
        cutsceneFinishTime -= Time.deltaTime;
        if (cutsceneFinishTime <= 0) {
            sceneTransition.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
