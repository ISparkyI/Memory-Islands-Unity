using UnityEngine;
using UnityEngine.UI;

public class PinLoader : MonoBehaviour
{
    public Image pinImage;

    private const string SelectedSkinObjectNameKey = "SelectedSkinObjectName";
    private const string DefaultPinSkinObjectName = "DefoultPin";

    private void Start()
    {
        LoadSelectedSkin();
    }

    private void LoadSelectedSkin()
    {
        string selectedSkinObjectName = PlayerPrefs.GetString(SelectedSkinObjectNameKey, "");

        if (string.IsNullOrEmpty(selectedSkinObjectName))
        {
            selectedSkinObjectName = DefaultPinSkinObjectName;
        }

        string resourcePath = $"Visual/CoupleCards/Cards/Pins/{selectedSkinObjectName}";

        Sprite loadedSprite = Resources.Load<Sprite>(resourcePath);

        if (loadedSprite != null && pinImage != null)
        {
            pinImage.sprite = loadedSprite;
        }
        else
        {
            Debug.LogWarning($"Не вдалося завантажити спрайт з ресурсів за шляхом {resourcePath}");
        }
    }
}
