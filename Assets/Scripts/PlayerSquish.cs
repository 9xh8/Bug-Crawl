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
    private Tween currentTween;

    private bool doOnLand;

    private void Start() {
        player = GetComponent<Player>();
        playerGlitch = GetComponent<PlayerGlitch>();


        gameInput.OnPlayerPressJump += GameInput_OnPlayerPressJump;
    }


    private void GameInput_OnPlayerPressJump(object sender, System.EventArgs e) {
        if (player.IsGrounded() && player.state == Player.PlayerStates.gameplay) {
            KillCurrentTween();

            playerVisual.DOScale(new Vector3(stretchX, stretchY), stretchTime).OnComplete(() => {
                ResetScale();
            });
        }
    }

    private void Update() {
        if (playerVisual == null)
            return;

        if (player.IsGrounded()) {
            if (doOnLand && !playerGlitch.IsGlitched()) {
                KillCurrentTween();

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
        if (playerVisual == null) return;

        KillCurrentTween();

        playerVisual.DOScale(Vector3.one, stretchTime);
    }

    private void KillCurrentTween() {
        if (currentTween != null && currentTween.IsActive()) {
            currentTween.Kill();
            currentTween = null;
        }
    }
}
