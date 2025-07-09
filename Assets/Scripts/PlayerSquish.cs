using System.Collections;
using UnityEngine;

public class PlayerSquish : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Transform playerVisual;
    [SerializeField] private GameInput gameInput;

    [Header("Stretch Settings")]
    [SerializeField] private float stretchX = 1.2f;
    [SerializeField] private float stretchY = 0.8f;
    [SerializeField] private float stretchTime = 0.1f;

    [Header("Squish Settings")]
    [SerializeField] private float squishX = 0.8f;
    [SerializeField] private float squishY = 1.2f;
    [SerializeField] private float squishTime = 0.1f;

    private Player player;
    private PlayerGlitch playerGlitch;
    private bool doOnLand;
    private Coroutine activeCoroutine;

    private void Start() {
        player = GetComponent<Player>();
        playerGlitch = GetComponent<PlayerGlitch>();
        gameInput.OnPlayerPressJump += GameInput_OnPlayerPressJump;
    }

    private void GameInput_OnPlayerPressJump(object sender, System.EventArgs e) {
        StartStretch();
    }

    private void Update() {
        if (playerVisual == null) return;

        if (player.IsGrounded()) {
            if (doOnLand && !playerGlitch.IsGlitched()) {
                StartSquish();
                doOnLand = false;
            }
        }
        else {
            doOnLand = true;
        }
    }

    private void StartStretch() {
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        activeCoroutine = StartCoroutine(AnimateScaleCoroutine(
            new Vector3(stretchX, stretchY, 1f),
            stretchTime
        ));
    }

    private void StartSquish() {
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        activeCoroutine = StartCoroutine(AnimateScaleCoroutine(
            new Vector3(squishX, squishY, 1f),
            squishTime
        ));
    }

    private IEnumerator AnimateScaleCoroutine(Vector3 targetScale, float duration) {
        Vector3 originalScale = playerVisual.localScale;
        float timer = 0f;

        // Animate to target
        while (timer < duration) {
            timer += Time.deltaTime;
            float t = timer / duration;
            playerVisual.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        timer = 0f;
        // Animate back to normal
        while (timer < duration) {
            timer += Time.deltaTime;
            float t = timer / duration;
            playerVisual.localScale = Vector3.Lerp(targetScale, Vector3.one, t);
            yield return null;
        }

        playerVisual.localScale = Vector3.one;
        activeCoroutine = null;
    }

    private void OnDestroy() {
        if (gameInput != null) {
            gameInput.OnPlayerPressJump -= GameInput_OnPlayerPressJump;
        }
    }

}
