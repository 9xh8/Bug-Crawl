using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeActions : MonoBehaviour {

    [SerializeField] private Transform glitchStaminaUI;
    [SerializeField] private Transform inputTextContiner;
    
    [SerializeField] private DialougeCameraManager dialougeCamera;

    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        glitchStaminaUI.gameObject.SetActive(player.state == Player.PlayerStates.gameplay);
        inputTextContiner.gameObject.SetActive(player.state == Player.PlayerStates.dialouge);
    }

}
