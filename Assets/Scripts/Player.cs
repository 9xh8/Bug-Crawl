using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private PlayerInputActions playerInputActions;
    private PlayerGlitch playerGlitch;

    [SerializeField] private float jumpForce;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float bufferTime;
    [SerializeField] private float checkRadius;
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask groundLayer;
    
    private bool isGrounded;
    private float coyoteTimeCounter;
    private float bufferTimeCounter;

    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform playerVisual;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        rb = GetComponent<Rigidbody2D>();
        playerGlitch = GetComponent<PlayerGlitch>();
    }

    private void Update() {
        if (!playerGlitch.IsGlitched())
            HandleJumping();
        
        HandleMovement();
        HandleFlipping();
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

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }

    public bool IsGrounded() {
        return isGrounded;
    }

}