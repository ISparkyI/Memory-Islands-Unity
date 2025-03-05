using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar; 
    public TMP_Text progressText;
    public TMP_Text loadInfoText;

    private Coroutine loadingCoroutine;

    public void LoadScene(string sceneName)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            loadingCoroutine = StartCoroutine(AnimateProgressBar(sceneName));
        }
        else
        {
            Debug.LogWarning("No internet connection. Loading cannot proceed.");
            ResetLoadingUI();
        }
    }

    public void StopLoading()
    {
        if (loadingCoroutine != null)
        {
            StopCoroutine(loadingCoroutine);
            loadingCoroutine = null;
            ResetLoadingUI();
        }
    }

    private void ResetLoadingUI()
    {
        progressBar.value = 0;
        progressText.text = "0%";
        loadInfoText.text = "Waiting for internet connection...";
    }

    public void LoadSceneAgain()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && loadingCoroutine == null)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            LoadScene(currentScene);
        }
        else
        {
            Debug.LogWarning("Cannot reload scene without internet connection.");
        }
    }

    private IEnumerator AnimateProgressBar(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float fakeProgress = 0f;
        float loadingTime = 3f;
        float timer = 0f;

        while (timer < loadingTime)
        {
            timer += Time.deltaTime;
            fakeProgress = Mathf.Clamp01(timer / loadingTime);
            progressBar.value = fakeProgress;
            progressText.text = Mathf.RoundToInt(fakeProgress * 100f) + "%";

            if (fakeProgress >= 0f && fakeProgress < 0.33f)
            {
                loadInfoText.text = "Loading textures...";
            }
            else if (fakeProgress >= 0.33f && fakeProgress < 0.66f)
            {
                loadInfoText.text = "Loading scripts...";
            }
            else if (fakeProgress >= 0.66f)
            {
                loadInfoText.text = "Finishing up...";
            }

            if (fakeProgress >= 0.4f && fakeProgress <= 0.9f)
            {
                float alphaProgress = Mathf.InverseLerp(0.6f, 0.9f, fakeProgress);
            }

            yield return null;
        }

        progressText.text = "100%";
        loadInfoText.text = "Game is ready!";
        operation.allowSceneActivation = true;
    }
}
