using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArea : MonoBehaviour {

    public event EventHandler OnPlayerEnterTutorialArea;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Player>()) {
            OnPlayerEnterTutorialArea?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Player>()) {
            Destroy(gameObject);
        }
    }

}
