using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquish : MonoBehaviour {

    [SerializeField] private Transform playerVisual;
    [SerializeField] private float stretchX;
    [SerializeField] private float stretchY;
    [SerializeField] private float stretchTime;
    [SerializeField] private float squishX;
    [SerializeField] private float squishY;
    [SerializeField] private float squishTime;
    [SerializeField] private GameInput gameInput;

    private Player player;
    private PlayerGlitch playerGlitch;
    private bool doOnLand;

    private void Start() {
        player = GetComponent<Player>();
        playerGlitch = GetComponent<PlayerGlitch>();


        gameInput.OnPlayerPressJump += GameInput_OnPlayerPressJump;
    }


    private void GameInput_OnPlayerPressJump(object sender, System.EventArgs e) {
        if (player.IsGrounded() && player.state == Player.PlayerStates.gameplay) {
            playerVisual.DOScale(new Vector3(stretchX, stretchY), stretchTime).OnComplete(() => {
                ResetScale();
            });
        }
    }

    private void Update() {
        if (player.IsGrounded()) {
            if (doOnLand && !playerGlitch.IsGlitched()) {
                playerVisual.DOScale(new Vector3(squishX, squishY), squishTime).OnComplete(() => {
                    ResetScale();
                });
                doOnLand = false;
            }
        }
        else {
            doOnLand = true;
        }
    }

    private void ResetScale() {
        playerVisual.DOScale(Vector3.one, stretchTime);
    }
}
