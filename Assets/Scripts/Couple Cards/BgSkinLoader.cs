using UnityEngine;

public class BgSkinLoader : MonoBehaviour
{
    public Material skyboxMaterial;

    private const string SelectedBgSkinObjectNameKey = "SelectedBgObjectName";
    private const string DefaultBgSkinObjectName = "CoupleCardsBG1";

    private void Start()
    {
        LoadSelectedBgSkin();
    }

    private void LoadSelectedBgSkin()
    {
        string selectedBgSkinObjectName = PlayerPrefs.GetString(SelectedBgSkinObjectNameKey, "");

        if (string.IsNullOrEmpty(selectedBgSkinObjectName))
        {
            selectedBgSkinObjectName = DefaultBgSkinObjectName;
            Texture2D DefaultTexture = Resources.Load<Texture2D>(selectedBgSkinObjectName);
            ApplyTextureToSkybox(DefaultTexture);
        }

        string resourcePath = $"Visual/CoupleCards/BG/{selectedBgSkinObjectName}";

        Texture2D loadedTexture = Resources.Load<Texture2D>(resourcePath);

        if (loadedTexture != null)
        {
            ApplyTextureToSkybox(loadedTexture);
        }
        else
        {
            Debug.LogWarning($"Не вдалося завантажити текстуру з ресурсів за шляхом {resourcePath}");
        }
    }

    private void ApplyTextureToSkybox(Texture2D texture)
    {
        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetTexture("_FrontTex", texture);
            skyboxMaterial.SetTexture("_BackTex", texture);
            skyboxMaterial.SetTexture("_LeftTex", texture);
            skyboxMaterial.SetTexture("_RightTex", texture);
            skyboxMaterial.SetTexture("_UpTex", texture);
            skyboxMaterial.SetTexture("_DownTex", texture);
        }
        else
        {
            Debug.LogWarning("Не вдалося знайти матеріал SkyBox для встановлення текстури");
        }
    }
}
