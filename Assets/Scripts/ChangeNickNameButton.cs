using UnityEngine;

public class ChangeNickNameButton : MonoBehaviour
{
    public GameObject selectAvatarPanel;
    public GameObject changeNickNamePanel;

    public void ShowChangeNickName()
    {
        if (selectAvatarPanel != null)
        {
            selectAvatarPanel.SetActive(false);
        }
        if (changeNickNamePanel != null)
        {
            changeNickNamePanel.SetActive(true);
        }
    }

    public void ShowSelectAvatar()
    {
        if (changeNickNamePanel != null)
        {
            changeNickNamePanel.SetActive(false);
        }
        if (selectAvatarPanel != null)
        {
            selectAvatarPanel.SetActive(true);
        }
    }
}
