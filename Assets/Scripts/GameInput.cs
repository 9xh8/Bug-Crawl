using System;
using UnityEngine;

public class GameInput : MonoBehaviour {

    public event EventHandler OnPlayerPressJump;
    public event EventHandler OnPlayerReleaseJump;

    public event EventHandler OnPlayerPressGlitch;
    public event EventHandler OnPlayerReleaseGlitch;

    public event EventHandler OnPlayerContinue;

    public event EventHandler OnPlayerPause;

    private PlayerInputActions playerInputActions;

    [SerializeField] private Player player; 

    private void Awake() {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable() {
        playerInputActions.Player.Enable();
        playerInputActions.UI.Enable();
    }

    private void OnDisable() {
        playerInputActions.UI.Disable();
    }

    private void Start() {
        playerInputActions.Player.Jump.performed += Jump_performed;
        playerInputActions.Player.Jump.canceled += Jump_canceled;

        playerInputActions.Player.Glitch.performed += Glitch_performed;
        playerInputActions.Player.Glitch.canceled += Glitch_canceled;

        playerInputActions.Player.Continue.performed += Continue_performed;

        playerInputActions.Player.Pause.performed += Pause_performed1;
        playerInputActions.UI.Pause.performed += Pause_performed;
    }

    private void Pause_performed1(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPlayerPause?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPlayerPause?.Invoke(this, EventArgs.Empty);
    }

    private void Glitch_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPlayerPressGlitch?.Invoke(this, EventArgs.Empty);
    }

    private void Glitch_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPlayerReleaseGlitch?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPlayerPressJump?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPlayerReleaseJump?.Invoke(this, EventArgs.Empty);
    }

    private void Continue_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPlayerContinue?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMoveInput() {
        Vector2 moveInput;

        moveInput = playerInputActions.Player.Movement.ReadValue<Vector2>();

        return moveInput;
    }

    private void Update() {
        if (player != null) {
            if (player.state != Player.PlayerStates.gameplay) {
                playerInputActions.Player.Movement.Disable();
                playerInputActions.Player.Jump.Disable();
                playerInputActions.Player.Glitch.Disable();
            }
            else {
                playerInputActions.Player.Movement.Enable();
                playerInputActions.Player.Jump.Enable();
                playerInputActions.Player.Glitch.Enable();
            }
        }
    }

    public PlayerInputActions PlayerInputActions() {
        return playerInputActions;
    }

}
