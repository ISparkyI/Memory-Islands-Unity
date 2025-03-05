using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class AdNotLoadedMessage : MonoBehaviour
{
    public Canvas overlayCanvas;
    public TMP_Text messageTemplate;
    public Vector3 startPosition;
    public float moveSpeed = 50f;
    public float duration = 2f;

    private const string LanguagePrefKey = "SelectedLanguage";

    private string textUA = "Реклама не завантажена, спробуйте пізніше";
    private string textEN = "Ad is not loaded, please try again later";
    private string textRU = "Реклама не загружена, попробуйте позже";

    public void ShowAdNotLoadedMessage()
    {
        TMP_Text messageInstance = Instantiate(messageTemplate, overlayCanvas.transform);

        messageInstance.transform.localPosition = startPosition;

        messageInstance.gameObject.SetActive(true);

        string selectedLanguage = PlayerPrefs.GetString(LanguagePrefKey, "EN");

        switch (selectedLanguage)
        {
            case "UA":
                messageInstance.text = textUA;
                break;
            case "RU":
                messageInstance.text = textRU;
                break;
            default:
                messageInstance.text = textEN;
                break;
        }

        StartCoroutine(AnimateAndDestroy(messageInstance));
    }

    private IEnumerator AnimateAndDestroy(TMP_Text message)
    {
        Vector3 targetPosition = startPosition + new Vector3(0, 60, 0);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            message.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        Color originalColor = message.color;
        while (elapsedTime < duration / 2)
        {
            message.color = Color.Lerp(originalColor, Color.clear, elapsedTime / (duration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(message.gameObject);
    }
}
