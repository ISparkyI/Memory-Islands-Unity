using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class IslandUpgradeManager : MonoBehaviour
{
    [System.Serializable]
    public class LayerUpgrade
    {
        public Button upgradeButton;
        public GameObject[] levelObjects;
        public GameObject[] layerObjects;
        public int[] costsForButton;
        public GameObject upgradedObject;
        public TextMeshProUGUI costText;
    }

    public LayerUpgrade[] layers;

    public TextMeshPro coinsText;
    public TextMeshPro rubinsText;
    public TextMeshProUGUI coinsPerHourText;
    public TextMeshProUGUI rubinsPerHourText;
    public CoinsConverter coinsConverter;

    private string islandName;

    public PerHourLoader perHourLoader;
    public ParticlesGenerator particlesGenerator;
    public AudioSource hummersound;

    private void Start()
    {
        islandName = gameObject.name;

        LoadUpgradeLevels();
        UpdateUI();
        SetupUpgradeButtons();
    }

    private void SetupUpgradeButtons()
    {
        foreach (var layer in layers)
        {
            layer.upgradeButton.onClick.AddListener(() => UpgradeLayer(layer));
        }
    }

    private void UpdateUI()
    {
        UpdateCoinsText();
        UpdateRubinsText();
        UpdateCostTexts();
        UpdateCoinsPerHourText();
        UpdateRubinsPerHourText();
        coinsConverter.UpdateCoinsText();
    }

    private void UpgradeLayer(LayerUpgrade layer)
    {
        int currentLevel = GetCurrentLevel(layer);
        if (currentLevel >= layer.levelObjects.Length - 1) return;

        int cost = GetCostForLayer(layer);
        bool upgradeSuccessful = (IsThirdLayer(layer)) ? UpgradeLayerWithRubins(layer, cost) : UpgradeLayerWithCoins(layer, cost);

        if (upgradeSuccessful)
        {
            particlesGenerator.StartParticlesGeneration();
            hummersound.Play();
            StartCoroutine(UpgradeWithDelay(layer, currentLevel));
        }
    }

    private IEnumerator UpgradeWithDelay(LayerUpgrade layer, int currentLevel)
    {
        yield return new WaitForSeconds(1f);
        HandleUpgradeSuccess(layer, currentLevel);
        SaveUpgradeLevel(layer, currentLevel + 1);
    }

    private bool IsThirdLayer(LayerUpgrade layer)
    {
        return layer == layers[2];
    }

    private void HandleUpgradeSuccess(LayerUpgrade layer, int currentLevel)
    {
        if (currentLevel == 3)
        {
            layer.upgradeButton.gameObject.SetActive(false);
            layer.upgradedObject?.SetActive(true);
        }
        if (layer == layers[2])
        {
            int rubinsPerHour = PlayerPrefs.GetInt("RubinsPerHour", 0) + 1;
            PlayerPrefs.SetInt("RubinsPerHour", rubinsPerHour);
            UpdateRubinsPerHourText();
        }
        else if (layer == layers[0] || layer == layers[1])
        {
            int coinsPerHour = PlayerPrefs.GetInt("CoinsPerHour", 0) + 10;
            PlayerPrefs.SetInt("CoinsPerHour", coinsPerHour);
            UpdateCoinsPerHourText();
        }

        ActivateNextLevel(layer, currentLevel);
    }

    private bool UpgradeLayerWithRubins(LayerUpgrade layer, int cost)
    {
        int rubins = PlayerPrefs.GetInt("Rubins");

        if (rubins >= cost)
        {
            int startRubins = rubins;
            rubins -= cost;
            PlayerPrefs.SetInt("Rubins", rubins);
            UpdateRubinsText();
            StartCoroutine(AnimateRubinsText(startRubins, rubins));
            return true;
        }
        else
        {
            GoToMenuAndShowRubinsError();
            return false;
        }
    }

    private bool UpgradeLayerWithCoins(LayerUpgrade layer, int cost)
    {
        int coins = PlayerPrefs.GetInt("Coins");

        if (coins >= cost)
        {
            int startCoins = coins;
            coins -= cost;
            PlayerPrefs.SetInt("Coins", coins);
            UpdateCoinsText();
            StartCoroutine(AnimateCoinsText(startCoins, coins));
            return true;
        }
        else
        {
            GoToMenuAndShowCoinsError();
            return false;
        }
    }

    private void GoToMenuAndShowCoinsError()
    {
        SceneManager.LoadScene("Menu");
        PlayerPrefs.SetInt("ShowCoinsError", 1);
    }

    private void GoToMenuAndShowRubinsError()
    {
        SceneManager.LoadScene("Menu");
        PlayerPrefs.SetInt("ShowRubinsError", 1);
    }

    private IEnumerator AnimateCoinsText(int startValue, int endValue)
    {
        float animationDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            int currentValue = (int)Mathf.Lerp(startValue, endValue, elapsedTime / animationDuration);
            coinsText.text = currentValue.ToString();
            yield return null;
        }

        coinsText.text = endValue.ToString();
        if (coinsConverter != null)
        {
            coinsConverter.UpdateCoinsText();
        }
    }

    private IEnumerator AnimateRubinsText(int startValue, int endValue)
    {
        float animationDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            int currentValue = (int)Mathf.Lerp(startValue, endValue, elapsedTime / animationDuration);
            rubinsText.text = currentValue.ToString();
            yield return null;
        }

        rubinsText.text = endValue.ToString();
    }

    private int GetCostForLayer(LayerUpgrade layer)
    {
        int currentLevel = GetCurrentLevel(layer);
        if (currentLevel >= 0 && currentLevel < layer.costsForButton.Length)
        {
            return layer.costsForButton[currentLevel + 1];
        }
        return 0;
    }

    private int GetCurrentLevel(LayerUpgrade layer)
    {
        for (int i = layer.levelObjects.Length - 1; i >= 0; i--)
        {
            Image img = layer.levelObjects[i].GetComponent<Image>();
            if (img != null && img.color == Color.white)
            {
                return i;
            }
        }
        return -1;
    }

    private void ActivateNextLevel(LayerUpgrade layer, int currentLevel)
    {
        int nextLevel = currentLevel + 1;
        if (nextLevel < layer.levelObjects.Length)
        {
            Image nextImage = layer.levelObjects[nextLevel].GetComponent<Image>();
            if (nextImage != null)
            {
                nextImage.color = Color.white;
            }

            Transform upgraded = layer.levelObjects[nextLevel].transform.Find("Upgraded");
            if (upgraded != null)
            {
                upgraded.gameObject.SetActive(true);
            }

            if (nextLevel < layer.layerObjects.Length)
            {
                layer.layerObjects[nextLevel].SetActive(true);

                if ((nextLevel > 0 && layer == layers[0]) || (nextLevel > 0 && layer == layers[1]))
                {
                    layer.layerObjects[nextLevel - 1].SetActive(false);
                }
            }

            UpdateCostText(layer);
        }
    }

    private void UpdateCostTexts()
    {
        foreach (var layer in layers)
        {
            UpdateCostText(layer);
        }
    }

    private void UpdateCostText(LayerUpgrade layer)
    {
        int currentLevel = GetCurrentLevel(layer);
        if (currentLevel + 1 < layer.costsForButton.Length)
        {
            int nextCost = layer.costsForButton[currentLevel + 1];
            layer.costText.text = nextCost.ToString();
        }
        else
        {
            layer.costText.text = "0";
        }
    }

    private void UpdateCoinsText()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coins.ToString();
    }

    private void UpdateRubinsText()
    {
        int rubins = PlayerPrefs.GetInt("Rubins", 0);
        rubinsText.text = rubins.ToString();
    }

    private void UpdateCoinsPerHourText()
    {
        perHourLoader.UpdateCoinsPerHourText();
    }

    private void UpdateRubinsPerHourText()
    {
        perHourLoader.UpdateRubinsPerHourText();
    }

    private void SaveUpgradeLevel(LayerUpgrade layer, int level)
    {
        string key = $"{islandName}_{layer.upgradeButton.gameObject.name}_Level";
        PlayerPrefs.SetInt(key, level);
    }

    public void LoadUpgradeLevels()
    {
        foreach (var layer in layers)
        {
            string key = $"{islandName}_{layer.upgradeButton.gameObject.name}_Level";
            int savedLevel = PlayerPrefs.GetInt(key, 0);

            for (int i = 0; i <= savedLevel; i++)
            {
                if (i < layer.levelObjects.Length)
                {
                    Image img = layer.levelObjects[i].GetComponent<Image>();
                    if (img != null)
                    {
                        img.color = Color.white;
                    }

                    Transform upgraded = layer.levelObjects[i].transform.Find("Upgraded");
                    if (upgraded != null)
                    {
                        upgraded.gameObject.SetActive(true);
                    }

                    if (i < layer.layerObjects.Length)
                    {
                        layer.layerObjects[i].SetActive(true);
                        if (i >= 4)
                        {
                            layer.upgradeButton.gameObject.SetActive(false);
                            layer.upgradedObject?.SetActive(true);
                        }
                        if (layer == layers[2])
                        {
                            
                        }
                        else if (i > 0)
                        {
                            layer.layerObjects[i - 1].SetActive(false);
                        }
                    }
                }
            }

            UpdateCostText(layer);
        }
    }
}
