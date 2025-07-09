using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningDoor : MonoBehaviour {

    public event EventHandler OnPlayerWin;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Player>()) {
            OnPlayerWin?.Invoke(this, EventArgs.Empty);
        }
    }

}
