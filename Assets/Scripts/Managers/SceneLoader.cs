using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public CanvasGroup canvasGroup;
    public Text loadingProgressText;
    public Image loadingSlider;

    public void Awake()
    {
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        loadingScreen.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            float progress = async.progress / 0.9f;
            loadingSlider.fillAmount = progress;
            loadingProgressText.text = (Mathf.Round(progress * 100.0f) + "%").ToString();

            if (async.progress >= 0.9f)
            {
                loadingSlider.fillAmount = 1f;
                loadingProgressText.text = "100%";
                async.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return StartCoroutine(FadeLoadingScreen(0, 1));
        loadingScreen.SetActive(false);
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetValue;
    }
}