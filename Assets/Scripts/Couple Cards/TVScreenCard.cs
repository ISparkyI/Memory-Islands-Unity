using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TVScreenCard : MonoBehaviour
{
    public GameObject panel;
    public GameObject winPanel;
    public float changeInterval = 2f;

    public TextMeshProUGUI timerText;
    public GameObject timer;

    private Image image;
    private Coroutine changeCardCoroutine;

    public void OnEnable()
    {
        timer.SetActive(true);
        image = GetComponent<Image>();
        StartCoroutine(SetupScreenCardDelayed());
        StartChangingCards();
    }

    private void OnDisable()
    {
        StopChangingCards();
    }

    public IEnumerator SetupScreenCardDelayed()
    {
        float delay = 0.11f;
        yield return new WaitForSeconds(delay);

        SetRandomCardImage();
    }

    public void StartChangingCards()
    {
        if (changeCardCoroutine == null)
        {
            changeCardCoroutine = StartCoroutine(ChangeCardPeriodically());
        }
    }

    public void StopChangingCards()
    {
        if (changeCardCoroutine != null)
        {
            StopCoroutine(changeCardCoroutine);
            changeCardCoroutine = null;
        }
    }

    private IEnumerator ChangeCardPeriodically()
    {
        while (true)
        {
            SetRandomCardImage();
            StartTimer();
            yield return new WaitForSeconds(changeInterval);
        }
    }

    private void StartTimer()
    {
        if (timerText != null)
        {
            StartCoroutine(UpdateTimer());
        }
    }

    private IEnumerator UpdateTimer()
    {
        float remainingTime = changeInterval;
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            float seconds = Mathf.Max(remainingTime, 0);
            timerText.text = string.Format("{0:00}.{1:00}", Mathf.Floor(seconds), Mathf.Floor((seconds - Mathf.Floor(seconds)) * 100));
            yield return null;
        }
        timerText.text = "00.00";
    }

    public void SetRandomCardImage()
    {
        TVCardsController[] cardsControllers = panel.GetComponentsInChildren<TVCardsController>();
        bool allFounded = true;

        foreach (TVCardsController controller in cardsControllers)
        {
            if (controller.gameObject.name != "CardFounded")
            {
                allFounded = false;
                break;
            }
        }

        if (allFounded)
        {
            winPanel.SetActive(true);
            StopChangingCards();
            SetFrontSideCardTexture(null);
        }
        else
        {
            TVCardsController randomCard = null;

            do
            {
                randomCard = cardsControllers[Random.Range(0, cardsControllers.Length)];
            } while (randomCard.gameObject.name == "CardFounded");

            image.sprite = randomCard.backSideCard;
        }
    }

    public void SetFrontSideCardTexture(Sprite frontSide)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = 0f;
            image.color = color;
        }
    }

    public void ResetCardTransparency()
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = 1f;
            image.color = color;
        }
    }

    public void SetChangeInterval(float interval)
    {
        changeInterval = interval;

        if (gameObject.activeInHierarchy)
        {
            StopChangingCards();
            StartChangingCards();
        }
    }
}
