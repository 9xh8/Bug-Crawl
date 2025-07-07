using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeActions : MonoBehaviour {

    [SerializeField] private Transform glitchStaminaUI;
    [SerializeField] private Transform inputTextContiner;
    [SerializeField] private Animator tutorialTextContiner;
    
    [SerializeField] private DialougeCameraManager dialougeCamera;
    [SerializeField] private TutorialArea tutorialArea;

    private Player player;

    private const string APPEAR = "Appear";

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Start() {
        tutorialArea.OnPlayerEnterTutorialArea += TutorialArea_OnPlayerEnterTutorialArea;
    }

    private void TutorialArea_OnPlayerEnterTutorialArea(object sender, System.EventArgs e) {
        player.state = Player.PlayerStates.tutorial;
        tutorialArea.gameObject.SetActive(player.state == Player.PlayerStates.tutorial);
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
