using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class FirstNickNameChange : MonoBehaviour
{
    private const string NickNameKey = "PlayerNickName";
    public TMP_Text primaryNickNameText;
    public TMP_Text secondaryNickNameText;
    public TMP_InputField inputField;
    public Button saveButton;
    public TMP_Text errorText;

    private const int MaxNickNameLength = 16;

    public GameObject firstNickNameChange;

    void Start()
    {
        if (!PlayerPrefs.HasKey(NickNameKey))
        {

        }
        else
        {
            string savedNickName = PlayerPrefs.GetString(NickNameKey);
            UpdateNickNameTexts(savedNickName);
        }

        saveButton.onClick.AddListener(SaveNewNickName);

        if (errorText != null)
        {
            errorText.gameObject.SetActive(false);
        }
    }

    private void UpdateNickNameTexts(string nickName)
    {
        if (primaryNickNameText != null)
        {
            primaryNickNameText.text = nickName;
        }
        if (secondaryNickNameText != null)
        {
            secondaryNickNameText.text = nickName;
        }
    }

    public void SaveNewNickName()
    {
        string newNickName = inputField.text;

        if (newNickName.Length > MaxNickNameLength)
        {
            if (errorText != null)
            {
                errorText.text = "Nickname cannot be more than 16 characters.";
                errorText.gameObject.SetActive(true);
            }
        }
        else if (!string.IsNullOrEmpty(newNickName))
        {
            PlayerPrefs.SetString(NickNameKey, newNickName);
            PlayerPrefs.Save();
            UpdateNickNameTexts(newNickName);

            firstNickNameChange.SetActive(false);

            if (errorText != null)
            {
                errorText.gameObject.SetActive(false);
            }
        }
    }
}
