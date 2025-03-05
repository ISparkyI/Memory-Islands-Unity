using UnityEngine;

public class GameStart : MonoBehaviour
{
    public LoadingManager loadingManager;

    void Start()
    {
        loadingManager.LoadScene("Menu");
    }
}
