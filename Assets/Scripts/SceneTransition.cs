using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    [SerializeField] private float transitionTime;

    private Animator animator;
    private const string START = "Start";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void ReloadCurrentScene() {
        StartCoroutine(ReloadCurrentSceneCoroutine());
    }

    public void LoadScene(int sceneIndex) {
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    private IEnumerator ReloadCurrentSceneCoroutine() {
        animator.SetTrigger(START);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LoadSceneCoroutine(int sceneIndex) {
        animator.SetTrigger(START);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }

}
