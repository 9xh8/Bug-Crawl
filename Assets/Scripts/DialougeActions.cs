using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeActions : MonoBehaviour {

    [SerializeField] private Transform glitchStaminaUI;
    [SerializeField] private Transform inputTextContiner;
    [SerializeField] private Animator tutorialTextContiner;
    
    [SerializeField] private DialougeCameraManager dialougeCamera;
    [SerializeField] private TutorialArea tutorialArea;
    [SerializeField] private GameInput gameInput;

    private Player player;
    private PlayerGlitch playerGlitch;

    private const string APPEAR = "Appear";

    private void Awake() {
        player = GetComponent<Player>();
        playerGlitch = GetComponent<PlayerGlitch>();
    }

    private void Start() {
        tutorialArea.OnPlayerEnterTutorialArea += TutorialArea_OnPlayerEnterTutorialArea;
        gameInput.OnPlayerContinue += GameInput_OnPlayerContinue;
    }

    private void GameInput_OnPlayerContinue(object sender, System.EventArgs e) {
        if (player.state != Player.PlayerStates.gameplay) {
            Time.timeScale = 0.3f;
        }
    }

    private void TutorialArea_OnPlayerEnterTutorialArea(object sender, System.EventArgs e) {
        player.state = Player.PlayerStates.tutorial;
        tutorialArea.gameObject.SetActive(player.state == Player.PlayerStates.tutorial);
        Time.timeScale = 1f;
        playerGlitch.canGlitch = true;
    }

    private void Update() {
        glitchStaminaUI.gameObject.SetActive(player.state == Player.PlayerStates.gameplay);
        inputTextContiner.gameObject.SetActive(player.state == Player.PlayerStates.dialouge);
        if (player.state == Player.PlayerStates.tutorial) {
            tutorialTextContiner.gameObject.SetActive(true);
        }
        tutorialTextContiner.SetBool(APPEAR, player.state == Player.PlayerStates.tutorial);
    }

}
