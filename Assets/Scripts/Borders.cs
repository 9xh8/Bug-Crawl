using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Borders : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Rigidbody2D>()) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
