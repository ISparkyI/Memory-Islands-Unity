using UnityEngine;
using UnityEngine.UI;

public class LvlSoundLoader : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;

    private void Start()
    {
        GameObject[] levelObjects = GameObject.FindGameObjectsWithTag("LVL");

        foreach (GameObject levelObject in levelObjects)
        {
            Button button = levelObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(PlaySound);
            }
        }
    }

    private void PlaySound()
    {
        if (buttonSound != null)
        {
            buttonSound.Play();
        }
    }
}
