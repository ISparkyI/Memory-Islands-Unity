using UnityEngine;
using UnityEngine.UI;

public class AvatarkaLoader : MonoBehaviour
{
    [SerializeField] private Image headAvatarka;

    private const string headAvatarKey = "HeadAvatar";

    private void Start()
    {
        LoadHeadAvatar();
    }

    public void SaveHeadAvatar(Sprite sprite)
    {
        if (sprite != null)
        {
            PlayerPrefs.SetString(headAvatarKey, sprite.name);
            PlayerPrefs.Save();
        }
    }

    private void LoadHeadAvatar()
    {
        if (PlayerPrefs.HasKey(headAvatarKey))
        {
            string spriteName = PlayerPrefs.GetString(headAvatarKey);
            Sprite savedSprite = FindSpriteByName(spriteName);

            if (savedSprite != null)
            {
                headAvatarka.sprite = savedSprite;
            }
        }
    }

    private Sprite FindSpriteByName(string name)
    {
        Sprite[] sprites = Resources.FindObjectsOfTypeAll<Sprite>();
        foreach (Sprite sprite in sprites)
        {
            if (sprite.name == name)
            {
                return sprite;
            }
        }
        return null;
    }
}
