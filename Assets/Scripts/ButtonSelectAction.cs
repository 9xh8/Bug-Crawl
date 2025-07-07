using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectAction : MonoBehaviour, ISelectHandler, IDeselectHandler{

    [SerializeField] private TMP_Text text;
    [SerializeField] private string selected;
    [SerializeField] private string original;

    public void OnSelect(BaseEventData eventData) {
        text.text = selected;        
    }

    public void OnDeselect(BaseEventData eventData) {
        text.text = original;
    }
}
