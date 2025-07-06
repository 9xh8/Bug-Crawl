using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour {

    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private float cutsceneFinishTime;

    [SerializeField] private GameInput gameInput;

    private void Start() {
        gameInput.OnPlayerContinue += GameInput_OnPlayerContinue;
    }

    private void GameInput_OnPlayerContinue(object sender, System.EventArgs e) {
        cutsceneFinishTime = 0f;
    }

    private void Update() {
        cutsceneFinishTime -= Time.deltaTime;
        if (cutsceneFinishTime <= 0) {
            sceneTransition.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
