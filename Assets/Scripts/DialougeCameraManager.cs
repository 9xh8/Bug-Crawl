using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeCameraManager : MonoBehaviour {


    [SerializeField] private CinemachineVirtualCamera mainVirtualCamera;

    [SerializeField] private GameInput gameInput;

    [SerializeField] private Player player;

    [SerializeField] private float zoomDelayTime;

    private void Start() {
        player.state = Player.PlayerStates.dialouge;

        gameInput.OnPlayerContinue += GameInput_OnPlayerContinue;
    }

    private void Update() {
        zoomDelayTime -= Time.deltaTime;
        if (zoomDelayTime <= 0f && zoomDelayTime >= -1f) {
            mainVirtualCamera.Priority = 3;
        }
    }

    private void GameInput_OnPlayerContinue(object sender, System.EventArgs e) {
        if (zoomDelayTime <= 0) {
            player.state = Player.PlayerStates.gameplay;
            mainVirtualCamera.Priority = 10;
        }
    }
}
