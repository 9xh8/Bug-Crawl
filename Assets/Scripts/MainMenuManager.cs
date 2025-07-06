using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    
    [SerializeField] private SceneTransition sceneTransition;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            sceneTransition.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }

}
