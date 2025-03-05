using UnityEngine;
using UnityEngine.UI;

public class TVSkins : MonoBehaviour
{
    public GameObject tvSkin;

    private Image imageComponent;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (imageComponent != null && tvSkin != null)
        {
            Image tvSkinImageComponent = tvSkin.GetComponent<Image>();

            if (tvSkinImageComponent != null && tvSkinImageComponent.sprite != null)
            {
                imageComponent.sprite = tvSkinImageComponent.sprite;
            }

        }
    }
}
