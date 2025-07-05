using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBasedCamera : MonoBehaviour {

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Rigidbody2D>()) {
            virtualCamera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Rigidbody2D>()) {
            virtualCamera.gameObject.SetActive(false);
        }
    }


}
