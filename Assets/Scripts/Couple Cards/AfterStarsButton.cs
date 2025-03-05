using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterStarsButton : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public GameObject slider;
    public CoinsResult coinsResult;
    public GameObject exitButton;

    public List<GameObject> objectsToSearch;

    private void Start()
    {
        exitButton.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        slider.SetActive(false);
    }

    public void ActivateButtons()
    {
        exitButton.SetActive(true);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        slider.SetActive(true);
        coinsResult.ShowCoinsResult();
    }

    public void CheckUpdateCardSizeAndUpdate()
    {
        var activeCardSizeController = FindActiveCardSizeController(objectsToSearch);
        if (activeCardSizeController != null)
        {
            activeCardSizeController.UpdateCardSize();
        }

        var activeNumberCardSizeController = FindActiveNumberCardSizeController(objectsToSearch);
        if (activeNumberCardSizeController != null)
        {
            activeNumberCardSizeController.UpdateCardSize();
        }
    }

    private CardSizeController FindActiveCardSizeController(List<GameObject> objectsToSearch)
    {
        foreach (GameObject obj in objectsToSearch)
        {
            CardSizeController controller = obj.GetComponent<CardSizeController>();
            if (controller != null && obj.activeInHierarchy)
            {
                return controller;
            }
        }
        return null;
    }

    private NumberCardSizeController FindActiveNumberCardSizeController(List<GameObject> objectsToSearch)
    {
        foreach (GameObject obj in objectsToSearch)
        {
            NumberCardSizeController controller = obj.GetComponent<NumberCardSizeController>();
            if (controller != null && obj.activeInHierarchy)
            {
                return controller;
            }
        }
        return null;
    }
}
