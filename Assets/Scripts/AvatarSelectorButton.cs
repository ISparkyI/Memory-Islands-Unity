using UnityEngine;
using UnityEngine.UI;

public class AvatarSelectorButton : MonoBehaviour
{
    [SerializeField] private Canvas targetCanvas;

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
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(true);
        }
    }
}
