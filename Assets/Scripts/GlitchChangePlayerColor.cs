using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchChangePlayerColor : MonoBehaviour {

    [SerializeField] private new UnityEngine.Experimental.Rendering.Universal.Light2D light;
    [SerializeField] private SpriteRenderer playerVisual;
    [SerializeField] private Color originalLightColor;
    [SerializeField] private Color glitchLightColor;
    [SerializeField] private Color originalColor;
    [SerializeField] private Color glitchColor;
    private PlayerGlitch playerGlitch;

    private void Awake() {
        playerGlitch = GetComponent<PlayerGlitch>();
    }

    private void Update() {
        if (playerGlitch.IsGlitched()) {
            light.color = glitchLightColor;
            playerVisual.color = glitchColor;
        }
        else {
            light.color = originalLightColor;
            playerVisual.color = originalColor;
        }
    }

}
