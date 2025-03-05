using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RubinsLoader : MonoBehaviour
{
    public TMP_Text rubinsText;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Rubins"))
        {
            PlayerPrefs.SetInt("Rubins", 20);
        }

        LoadRubins();
    }

    private void LoadRubins()
    {
        int rubins = PlayerPrefs.GetInt("Rubins");
        UpdateRubinsText(rubins);
    }

    private void UpdateRubinsText(int rubins)
    {
        if (rubinsText != null)
        {
            rubinsText.text = rubins.ToString();
        }
    }
}
