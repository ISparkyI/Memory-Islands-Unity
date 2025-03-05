using UnityEngine;
using UnityEngine.UI;

public class PinScaler : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    private Vector2 initialCardSize = new Vector2(256, 374);

    void Start()
    {
        if (gridLayoutGroup == null)
        {
            gridLayoutGroup = GetComponent<GridLayoutGroup>();
            if (gridLayoutGroup == null)
            {
                enabled = false;
                return;
            }
        }

        initialCardSize = gridLayoutGroup.cellSize;
    }

    void Update()
    {
        Vector2 currentCardSize = gridLayoutGroup.cellSize;

        float widthRatio = currentCardSize.x / initialCardSize.x;
        float heightRatio = currentCardSize.y / initialCardSize.y;

        ScalePins(widthRatio, heightRatio);
    }

    void ScalePins(float widthRatio, float heightRatio)
    {
        foreach (Transform card in transform)
        {
            foreach (Transform child in card)
            {
                if (child.name == "Pin") 
                {
                    child.localScale = new Vector3(widthRatio, heightRatio, child.localScale.z);
                }
            }
        }
    }
}
