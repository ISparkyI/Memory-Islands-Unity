using System.Linq;
using UnityEngine;

public class SoundVolumeLoader : MonoBehaviour
{
    private const string VolumePrefKey = "SoundVolume";

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);

        AudioSource[] soundSources = GameObject.FindGameObjectsWithTag("Sound")
            .Select(obj => obj.GetComponent<AudioSource>())
            .Where(source => source != null)
            .ToArray();

        foreach (AudioSource source in soundSources)
        {
            if (source != null)
            {
                source.volume = savedVolume;
            }
        }
    }
}
