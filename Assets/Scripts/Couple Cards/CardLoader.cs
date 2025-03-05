using UnityEngine;
using UnityEngine.UI;

public class CardLoader : MonoBehaviour
{
    public Image cardImage;

    private const string SelectedCardSkinObjectNameKey = "SelectedCardObjectName";
    private const string DefaultCardSkinObjectName = "SilverCard";

    private void Start()
    {
        LoadSelectedCardSkin();
    }

    private void LoadSelectedCardSkin()
    {
        string selectedCardSkinObjectName = PlayerPrefs.GetString(SelectedCardSkinObjectNameKey, "");

        if (string.IsNullOrEmpty(selectedCardSkinObjectName))
        {
            selectedCardSkinObjectName = DefaultCardSkinObjectName;
        }

        string resourcePath = $"Visual/CoupleCards/Cards/BackSideSkins/{selectedCardSkinObjectName}";

        Sprite loadedSprite = Resources.Load<Sprite>(resourcePath);

        if (loadedSprite != null && cardImage != null)
        {
            cardImage.sprite = loadedSprite;
        }
        else
        {
            Debug.LogWarning($"Не вдалося завантажити спрайт з ресурсів за шляхом {resourcePath}");
        }
    }
}
