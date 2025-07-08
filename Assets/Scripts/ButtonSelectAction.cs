using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectAction : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] private TMP_Text text;
    [SerializeField] private string original;
    [SerializeField] private string selected;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SoundRefsSO soundRefsSO;

    public void OnSelect(BaseEventData eventData) {
        text.text = selected;
        soundManager.PlayRandomMainMenuSound(
            soundRefsSO.mainMenu, 
            Vector3.zero, 
            1f);
    }

    public void OnDeselect(BaseEventData eventData) {
        text.text = original;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (text != null) {
            text.text = selected;
        }

        if (soundManager != null && soundRefsSO != null)
            soundManager.PlayRandomMainMenuSound(
                soundRefsSO.mainMenu,
                Vector3.zero,
                1f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (text != null) {
            text.text = original;
        }
    }
}
