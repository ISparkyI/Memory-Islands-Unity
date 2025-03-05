using UnityEngine;
using UnityEngine.UI;

public class AvatarkaSync : MonoBehaviour
{
    [SerializeField] private Image headAvatarka;
    [SerializeField] private Image avatarka;

    private void OnEnable()
    {
        if (headAvatarka != null && avatarka != null)
        {
            avatarka.sprite = headAvatarka.sprite;
        }
    }
}
