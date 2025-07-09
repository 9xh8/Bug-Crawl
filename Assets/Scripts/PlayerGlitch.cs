using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlitch : MonoBehaviour {

    [SerializeField] private float glitchTimerMax;
    [SerializeField] private float glitchTimer;

    [SerializeField] private float floatingDistance;
    [SerializeField] private float floatingTweenTime;
    [SerializeField] private AnimationCurve curve;

    [SerializeField] private GameInput gameInput;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float collisionShakeIntensity;
    [SerializeField] private float collisionShakeTime;
    
    private bool isGlitched = false;
    [HideInInspector] public bool canGlitch = false;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private PlayerInputActions playerInputActions;
    private Player player;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        rb = GetComponent<Rigidbody2D>();
        bc = GetComponentInChildren<BoxCollider2D>();
        player = GetComponent<Player>();
    }

    private void OnEnable() {
        playerInputActions.Player.Enable();
    }

    private void OnDisable() {
        playerInputActions.Player.Disable();
    }

    private void Start() {
        glitchTimer = glitchTimerMax;

        gameInput.OnPlayerPressGlitch += GameInput_OnPlayerPressGlitch;
        gameInput.OnPlayerReleaseGlitch += GameInput_OnPlayerReleaseGlitch;
    }

    private void Update() {
        if (isGlitched) {
            glitchTimer -= Time.deltaTime;

            if (glitchTimer <= 0f) {
                glitchTimer = 0f;
                canGlitch = false;
                DeactivateGlitch();
            }
        }
    }

    private void GameInput_OnPlayerPressGlitch(object sender, System.EventArgs e) {
        if (canGlitch && glitchTimer > 0f && player.state == Player.PlayerStates.gameplay) {
            ActivateGlitch();
        }
        else {
            DeactivateGlitch();
        }
    }
    
    private void GameInput_OnPlayerReleaseGlitch(object sender, System.EventArgs e) {
        DeactivateGlitch();
    }

    private void ActivateGlitch() {
        if (!isGlitched && rb != null) {  
            isGlitched = true;
            canGlitch = false;

            rb.gravityScale = 0f;
            rb.velocity = Vector3.zero;
            bc.isTrigger = true;

            transform.DOMoveY(transform.position.y + floatingDistance, floatingTweenTime).SetEase(curve);
        }
    }

    private void DeactivateGlitch() {
        if (isGlitched && rb != null) {
            isGlitched = false;
            canGlitch = true;

            rb.gravityScale = 10f;
            bc.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isGlitched && !other.GetComponent<RoomBasedCamera>()) {
            cameraShake.Shake(collisionShakeIntensity, collisionShakeTime);
        }
    }

    public bool IsGlitched() {
        return isGlitched;
    }

    public bool CanGlitch() {
        return canGlitch;
    }

    public float GlitchTimer() {
        return glitchTimer;
    }

    public float GlitchTimerMax() {
        return glitchTimerMax;
    }
}
