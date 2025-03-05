using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private Image avatarka;
    [SerializeField] private Image headAvatarka;

    [SerializeField] private AvatarkaLoader avatarkaLoader;

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        if (avatarka != null && headAvatarka != null)
        {
            headAvatarka.sprite = avatarka.sprite;

            if (avatarkaLoader != null)
            {
                avatarkaLoader.SaveHeadAvatar(headAvatarka.sprite);
            }
        }
    }
}
