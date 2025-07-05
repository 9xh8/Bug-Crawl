using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlitchStaminaUI : MonoBehaviour {

    [SerializeField] private Image glitchImage;
    [SerializeField] private Color originalColor;
    [SerializeField] private Color halfColor;
    [SerializeField] private Color quarterColor;
    [SerializeField] private PlayerGlitch playerGlitch;
    

    private void Update() {
        float fillPercent = playerGlitch.GlitchTimer() / playerGlitch.GlitchTimerMax();
        glitchImage.fillAmount = fillPercent;

        if (fillPercent > .5f) {
            glitchImage.color = originalColor;
        }
        else if (fillPercent > 0.25f) {
            glitchImage.color = halfColor;
        }
        else {
            glitchImage.color = quarterColor;
        }
    }

}
