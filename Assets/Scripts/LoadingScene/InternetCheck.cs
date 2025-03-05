using UnityEngine;

public class InternetCheck : MonoBehaviour
{
    public GameObject noInternetPanel;
    public LoadingManager loadingManager;

    void Start()
    {
        CheckInternetConnection();
    }

    void CheckInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if (noInternetPanel != null)
            {
                noInternetPanel.SetActive(true);
                loadingManager.StopLoading();
            }

            Time.timeScale = 0;
        }
        else
        {
            if (noInternetPanel != null)
            {
                noInternetPanel.SetActive(false);
            }

            Time.timeScale = 1;
        }
    }

    public void RetryCheckInternetConnection()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            Time.timeScale = 1;
            noInternetPanel.SetActive(false);
            loadingManager.LoadSceneAgain();
        }
        else
        {
            noInternetPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
