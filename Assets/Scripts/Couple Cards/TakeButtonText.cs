using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TakeButtonText : MonoBehaviour
{
    [SerializeField] private TMP_Text takeButtonText;
    [SerializeField] private TMP_Text coinsResult;

    public void UpdateTakeButtonText()
    {
        if (takeButtonText != null && coinsResult != null)
        {
            int coins = int.Parse(coinsResult.text);
            takeButtonText.text = "" + coins.ToString();
        }
    }
}
