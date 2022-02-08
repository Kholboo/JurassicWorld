using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
    public CanvasGroup canvasGroup;
    public Image loadingSlider;

    void Start () {
        StartCoroutine (StartLoad ());
    }

    IEnumerator StartLoad () {
        AsyncOperation async = SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex + 1);
        async.allowSceneActivation = false;

        while (!async.isDone) {
            float progress = async.progress / 0.9f;
            loadingSlider.fillAmount = progress;

            if (async.progress >= 0.9f) {
                loadingSlider.fillAmount = 1f;
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}