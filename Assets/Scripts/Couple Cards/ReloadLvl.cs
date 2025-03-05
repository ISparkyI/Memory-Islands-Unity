using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ReloadLvl : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Transform contentTransform;

    public void ReloadLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        levelText.text = "LVL : " + currentLevel;
        if (int.TryParse(levelText.text.Replace("LVL : ", ""), out currentLevel))
        {
            GameObject levelObject = FindLevelObject(currentLevel);
            if (levelObject != null)
            {
                Button levelButton = levelObject.GetComponent<Button>();
                if (levelButton != null)
                {
                    var newOnClick = new Button.ButtonClickedEvent();

                    for (int i = 0; i < levelButton.onClick.GetPersistentEventCount(); i++)
                    {
                        var target = levelButton.onClick.GetPersistentTarget(i);
                        var method = levelButton.onClick.GetPersistentMethodName(i);

                        if (!(target is ChangeSkybox && method == "ChangeToPreviousSkybox"))
                        {
                            var tempTarget = target;
                            var tempMethod = method;
                            newOnClick.AddListener(() => tempTarget.GetType().GetMethod(tempMethod).Invoke(tempTarget, null));
                        }
                    }

                    newOnClick.Invoke();

                    TVCardFlipper cardFlipper = FindAnyObjectByType<TVCardFlipper>();
                    if (cardFlipper != null)
                    {
                        cardFlipper.StartCoroutine(cardFlipper.StartFlipAllCardsWithDelay());
                    }

                    TVScreenCard screenCard = FindTVScreenCard();
                    if (screenCard != null)
                    {
                        screenCard.StartCoroutine(screenCard.SetupScreenCardDelayed());
                    }
                }
            }
        }
    }


    private GameObject FindLevelObject(int level)
    {
        string levelName = "LVL " + level;
        foreach (Transform child in contentTransform)
        {
            if (child.gameObject.CompareTag("LVL") && child.gameObject.name == levelName)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    private TVScreenCard FindTVScreenCard()
    {
        TVScreenCard screenCard = FindAnyObjectByType<TVScreenCard>();
        return screenCard;
    }

}
