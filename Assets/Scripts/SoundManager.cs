using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Button muteButton;

    private float previousVolume = 1f;
    private AudioSource[] soundSources;
    private const string VolumePrefKey = "SoundVolume";

    private void Start()
    {
        soundSources = GameObject.FindGameObjectsWithTag("Sound")
            .Select(obj => obj.GetComponent<AudioSource>())
            .Where(source => source != null)
            .ToArray();

        previousVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);

        if (volumeSlider != null)
        {
            volumeSlider.value = previousVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
        }

        if (muteButton != null)
        {
            muteButton.onClick.AddListener(ToggleVolume);
        }

        SetVolume(previousVolume);
    }

    private void OnVolumeSliderChanged(float value)
    {
        SetVolume(value);
        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();
    }

    private void ToggleVolume()
    {
        float currentVolume = volumeSlider.value;
        float newVolume = (currentVolume > 0f) ? 0f : 0.5f;
        SetVolume(newVolume);
        volumeSlider.value = newVolume;
        PlayerPrefs.SetFloat(VolumePrefKey, newVolume);
        PlayerPrefs.Save();
    }

    private void SetVolume(float volume)
    {
        foreach (AudioSource source in soundSources)
        {
            if (source != null)
            {
                source.volume = volume;
            }
        }
        previousVolume = volume;
    }
}
