using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeCameraManager : MonoBehaviour {


    [SerializeField] private CinemachineVirtualCamera dialougeCamera;
    [SerializeField] private CinemachineBrain cameraBrain;

    [SerializeField] private GameInput gameInput;

    [SerializeField] private Player player;

    [SerializeField] private TutorialArea tutorialArea;

    [SerializeField] private float zoomDelayTime;

    private void Start() {
        player.state = Player.PlayerStates.dialouge;

        gameInput.OnPlayerContinue += GameInput_OnPlayerContinue;

        tutorialArea.OnPlayerEnterTutorialArea += TutorialArea_OnPlayerEnterTutorialArea;
    }

    private void TutorialArea_OnPlayerEnterTutorialArea(object sender, EventArgs e) {
        dialougeCamera.Priority = 13;
    }

    private void Update() {
        zoomDelayTime -= Time.deltaTime;
        if (zoomDelayTime <= 0f && zoomDelayTime >= -1f) {
            dialougeCamera.Priority = 13;
        }
    }

    private void GameInput_OnPlayerContinue(object sender, System.EventArgs e) {
        if (zoomDelayTime <= 0 && !cameraBrain.IsBlending) {
            player.state = Player.PlayerStates.gameplay;
            dialougeCamera.Priority = 3;
        }
    }
}
