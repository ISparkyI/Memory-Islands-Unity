using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPlayerData : MonoBehaviour
{
    private string headAvatarKey = "HeadAvatar";
    private string nickNameKey = "PlayerNickName";

    public Image avatarImage;
    public TMP_Text nickNameText;

    void Start()
    {
        LoadData();
    }

    void LoadData()
    {
        string spriteName = PlayerPrefs.GetString(headAvatarKey);
        if (!string.IsNullOrEmpty(spriteName))
        {
            Sprite savedSprite = FindSpriteInSpriteSheet("Visual/BG/SelectAvatar/Avatars/Avatarki", spriteName);
            if (savedSprite != null)
            {
                avatarImage.sprite = savedSprite;
            }
        }

        string nickName = PlayerPrefs.GetString(nickNameKey);
        if (!string.IsNullOrEmpty(nickName))
        {
            nickNameText.text = nickName;
        }
    }

    Sprite FindSpriteInSpriteSheet(string spriteSheetPath, string spriteName)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(spriteSheetPath);
        foreach (Sprite sprite in sprites)
        {
            if (sprite.name == spriteName)
            {
                return sprite;
            }
        }
        return null;
    }
}
