using UnityEngine;

public class WindowSound : MonoBehaviour
{
    [SerializeField] private AudioSource soundToPlay;

    private void OnEnable()
    {
        if (soundToPlay != null)
        {
            soundToPlay.Play();
        }
    }
}
