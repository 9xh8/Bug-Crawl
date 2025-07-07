using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitToMenuButton;

    [SerializeField] private string selectSign;

    [SerializeField] private Transform pauseMenu;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private SceneTransition sceneTransition;

    private bool isPaused;

    private void Awake() {
        gameInput.OnPlayerPause += GameInput_OnPlayerPause;

        continueButton.Select();

        continueButton.onClick.AddListener(() =>
            isPaused = false
        );

        restartButton.onClick.AddListener(() => {
            sceneTransition.ReloadCurrentScene();
            isPaused = false;
        }); 

        quitToMenuButton.onClick.AddListener(() => {
            sceneTransition.LoadScene(0);
            isPaused = false;
        });
    }

    private void GameInput_OnPlayerPause(object sender, System.EventArgs e) {
        isPaused = !isPaused;
    }

    private void Update() {
        pauseMenu.gameObject.SetActive(isPaused);

        if (isPaused) {
            Time.timeScale = 0f;
            gameInput.PlayerInputActions().Player.Disable();
            gameInput.PlayerInputActions().UI.Enable();
        }
        else {
            Time.timeScale = 1f;
            gameInput.PlayerInputActions().Player.Enable();
            gameInput.PlayerInputActions().UI.Disable();
        }
    }

}
