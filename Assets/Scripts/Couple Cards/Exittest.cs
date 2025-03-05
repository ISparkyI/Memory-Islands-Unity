using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exittest : MonoBehaviour
{
    public GameObject clouds;
    public CloudsLoadAnim cloudsLoadAnim;
    public float animationDuration = 3.0f;

    private void Start()
    {
        clouds.SetActive(false);
    }
    public void LoadMenu()
    {
        clouds.SetActive(true);
        StartCoroutine(PlayCloudsAnimationAndLoadScene());
        
    }

    private IEnumerator PlayCloudsAnimationAndLoadScene()
    {
        cloudsLoadAnim.StartCloudsAnimation();

        yield return new WaitForSeconds(animationDuration);

        SceneManager.LoadScene("Menu");
    }


}
