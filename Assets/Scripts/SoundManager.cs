using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour {

    [SerializeField] private SoundRefsSO soundRefsSO;

    public void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayRandomMainMenuSound(AudioClip[] audioSources, Vector3 position, float volume = 1) {
        AudioSource.PlayClipAtPoint(
            audioSources[Random.Range(0, soundRefsSO.mainMenu.Length)], 
            position, 
            volume);
    }

}
