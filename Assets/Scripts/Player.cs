using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform playerVisual;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private PlayerInputActions playerInputActions;
    private PlayerGlitch playerGlitch;

    [Header("Jumping Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float bufferTime;
    [SerializeField] private float checkRadius;
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask groundLayer;
    private float coyoteTimeCounter;
    private float bufferTimeCounter;
    private bool isGrounded;
    
    [Header("Input Settings")]
    [SerializeField] private GameInput gameInput;

    [Header("GameFeel Settings")]
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeTime;
    [SerializeField] private GameObject landParticle;
    private bool doOnLand;

    [Header("Refrences")]
    [SerializeField] private WinningDoor winningDoor;

    public enum PlayerStates {
        dialouge,
        gameplay,
        gameover,
        tutorial,
    }

    public PlayerStates state;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        rb = GetComponent<Rigidbody2D>();
        playerGlitch = GetComponent<PlayerGlitch>();
        state = PlayerStates.gameplay;
    }

    private void Start() {
        winningDoor.OnPlayerWin += WinningDoor_OnPlayerWin;
    }

    private void WinningDoor_OnPlayerWin(object sender, System.EventArgs e) {
        Destroy(gameObject);
    }

    private void OnEnable() {
        playerInputActions.Player.Enable();
    }

    private void OnDisable() {
        playerInputActions.Player.Disable();
    }

    private void Update() {
        if (!playerGlitch.IsGlitched() && state == PlayerStates.gameplay)
            HandleJumping();

        if (state != PlayerStates.gameplay) {
            rb.velocity = new Vector2(0f, rb.velocity.y * .5f);
            return;
        }

        HandleMovement();
        HandleFlipping();
        OnLand();
    }

    private void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
    }

    private void HandleMovement() {
        moveInput = gameInput.GetMoveInput();

        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    private void HandleJumping() {
        if (bufferTimeCounter > 0f && coyoteTimeCounter > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            bufferTimeCounter = 0f;
        }

        // Handles variable jump
        if (playerInputActions.Player.Jump.WasReleasedThisFrame()) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        
        if (isGrounded) {
            coyoteTimeCounter = coyoteTime;
        }
        else {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (playerInputActions.Player.Jump.WasPressedThisFrame()) {
            bufferTimeCounter = bufferTime;
        }
        else {
            bufferTimeCounter -= Time.deltaTime;
        }

    }

    private void HandleFlipping() {
        if (moveInput.x > 0f) {
            playerVisual.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
        }else if (moveInput.x < 0f) {
            playerVisual.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
        }
    }

    private void OnLand() {
        if (isGrounded) {
            if (doOnLand) {
                cameraShake.Shake(shakeIntensity, shakeTime);
                Instantiate(landParticle, feetPos.position, Quaternion.identity);
                doOnLand = false;
            }
        }
        else {
            doOnLand = true;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }

    public void Die() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsGrounded() {
        return isGrounded;
    }
}