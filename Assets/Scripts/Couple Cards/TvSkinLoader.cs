using UnityEngine;
using UnityEngine.UI;

public class TvSkinLoader : MonoBehaviour
{
    public Image tvSkinImage;

    private const string SelectedTvObjectNameKey = "SelectedTvObjectName";
    private const string DefaultTvSkinObjectName = "TV";

    private void Start()
    {
        LoadSelectedSkin();
    }

    private void LoadSelectedSkin()
    {
        string selectedSkinObjectName = PlayerPrefs.GetString(SelectedTvObjectNameKey, "");

        if (string.IsNullOrEmpty(selectedSkinObjectName))
        {
            selectedSkinObjectName = DefaultTvSkinObjectName;
        }

        string resourcePath = $"Visual/CoupleCards/Cards/TVs/{selectedSkinObjectName}";

        Sprite loadedSprite = Resources.Load<Sprite>(resourcePath);

        if (loadedSprite != null && tvSkinImage != null)
        {
            tvSkinImage.sprite = loadedSprite;
        }
        else
        {
            Debug.LogWarning($"Не вдалося завантажити спрайт з ресурсів за шляхом {resourcePath}");
        }
    }
}
