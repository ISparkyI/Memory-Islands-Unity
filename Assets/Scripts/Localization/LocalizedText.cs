using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;

    private void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (LocalizationManager.Instance == null)
        {
            return;
        }

        TextMeshPro textMeshPro = GetComponent<TextMeshPro>();
        TextMeshProUGUI textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        if (textMeshPro != null)
        {
            textMeshPro.text = LocalizationManager.Instance.GetLocalizedValue(key);
        }
        else if (textMeshProUGUI != null)
        {
            textMeshProUGUI.text = LocalizationManager.Instance.GetLocalizedValue(key);
        }

    }
}
