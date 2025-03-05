using UnityEngine;
using UnityEngine.UI;

public class ClearStarsPrefs : MonoBehaviour
{
    public void ClearAllLevelStars()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All level stars data has been cleared.");
    }
}
