using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public Slider volumeSlider;
    public BackgroundMusicManager musicManager;
    public Button muteButton;

    private void Start()
    {
        if (musicManager == null)
        {
            musicManager = FindAnyObjectByType<BackgroundMusicManager>();
        }

        if (musicManager != null)
        {
            volumeSlider.value = musicManager.GetVolume();
            volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);

            if (muteButton != null)
            {
                muteButton.onClick.AddListener(ToggleVolume);
            }

        }

    }

    private void OnVolumeSliderChanged(float value)
    {
        if (musicManager != null)
        {
            musicManager.SetVolume(value);
        }
    }

    private void ToggleVolume()
    {
        if (musicManager != null)
        {
            float currentVolume = musicManager.GetVolume();
            float newVolume = (currentVolume > 0f) ? 0f : 0.5f;
            musicManager.SetVolume(newVolume);
            volumeSlider.value = newVolume; 
        }
    }
}
