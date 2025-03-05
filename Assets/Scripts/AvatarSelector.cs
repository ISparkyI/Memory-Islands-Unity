using UnityEngine;
using UnityEngine.UI;

public class AvatarSelector : MonoBehaviour
{
    [SerializeField] private Image avatarka;

    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        if (avatarka != null && buttonImage != null)
        {
            avatarka.sprite = buttonImage.sprite;
        }
    }
}
