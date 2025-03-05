using UnityEngine;
using UnityEngine.UI;

public class ContentScrollbarSave : MonoBehaviour
{
    public Scrollbar scrollbar;
    public string playerPrefsKey = "LvlScrollbarValue";

    private void Start()
    {
        LoadScrollbarValue();

        scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
    }

    private void OnDestroy()
    {
        scrollbar.onValueChanged.RemoveListener(OnScrollbarValueChanged);
    }

    private void OnScrollbarValueChanged(float value)
    {
        PlayerPrefs.SetFloat(playerPrefsKey, value);
    }

    private void LoadScrollbarValue()
    {
        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            float savedValue = PlayerPrefs.GetFloat(playerPrefsKey);
            scrollbar.value = savedValue;
        }
    }
}
