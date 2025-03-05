using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButton : MonoBehaviour
{
    public IslandsScrollController scrollController;
    public Button playButton;
    public string[] islandScenes;

    public GameObject clouds;
    public CloudsLoadAnim cloudsLoadAnim;
    public float animationDuration = 3.0f;

    void Start()
    {
        clouds.SetActive(false);
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    void OnPlayButtonClicked()
    {
        int selectedItemIndex = scrollController.GetSelectedItemIndex();
        if (selectedItemIndex >= 0 && selectedItemIndex < islandScenes.Length)
        {
            clouds.SetActive(true);
            StartCoroutine(PlayCloudsAnimationAndLoadScene(islandScenes[selectedItemIndex]));
        }
    }

    private IEnumerator PlayCloudsAnimationAndLoadScene(string sceneName)
    {
        cloudsLoadAnim.StartCloudsAnimation();

        yield return new WaitForSeconds(animationDuration);

        SceneManager.LoadScene(sceneName);
    }
}
