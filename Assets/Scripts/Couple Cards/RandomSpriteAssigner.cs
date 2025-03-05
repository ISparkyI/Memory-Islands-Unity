using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteAssigner : MonoBehaviour
{
    public GameObject lvlPanel;
    public string[] spriteSheetFolderPaths;
    public CardSizeController cardSizeController;
    public CardShuffler cardShuffler;
    public HintManager hintManager;

    private List<string> usedSpritesNames = new List<string>();
    private string currentSpriteSheetFolderPath;

    private void OnEnable()
    {
        if (cardSizeController != null)
        {
            cardSizeController.OnCardSizeUpdated += AssignRandomSprites;
        }
    }

    private void OnDisable()
    {
        if (cardSizeController != null)
        {
            cardSizeController.OnCardSizeUpdated -= AssignRandomSprites;
        }
    }

    private void AssignRandomSprites()
    {
        currentSpriteSheetFolderPath = spriteSheetFolderPaths[Random.Range(0, spriteSheetFolderPaths.Length)];

        Sprite[] sprites = LoadAllSpritesFromFolder(currentSpriteSheetFolderPath);
        if (sprites.Length == 0) return;

        Dictionary<int, Sprite> pairSprites = new Dictionary<int, Sprite>();

        foreach (Transform child in lvlPanel.transform)
        {
            CardsController cardsController = child.GetComponent<CardsController>();
            if (cardsController != null && !child.name.Equals("FreeSpace"))
            {
                string[] nameParts = child.name.Split('_');
                if (nameParts.Length < 3) continue;

                int pairIndex;
                if (!int.TryParse(nameParts[1], out pairIndex)) continue;

                int cardIndex;
                if (!int.TryParse(nameParts[2], out cardIndex)) continue;

                if (!pairSprites.ContainsKey(pairIndex))
                {
                    pairSprites[pairIndex] = GetRandomUnusedSprite(sprites);
                }

                cardsController.backSideCard = pairSprites[pairIndex];
            }
        }

        if (cardShuffler != null)
        {
            cardShuffler.ShuffleCards();
        }

        if (hintManager != null)
        {
            hintManager.ActivateHints(PlayerPrefs.GetInt("CurrentLevel", 1));
        }
    }

    public Sprite GetRandomUnusedSprite(Sprite[] allSprites)
    {
        if (allSprites.Length == 0) return null;

        List<Sprite> availableSprites = new List<Sprite>();
        foreach (var sprite in allSprites)
        {
            if (!usedSpritesNames.Contains(sprite.name))
            {
                availableSprites.Add(sprite);
            }
        }

        if (availableSprites.Count == 0)
        {
            return allSprites[Random.Range(0, allSprites.Length)];
        }

        Sprite selectedSprite = availableSprites[Random.Range(0, availableSprites.Count)];
        usedSpritesNames.Add(selectedSprite.name);
        return selectedSprite;
    }

    Sprite[] LoadAllSpritesFromFolder(string folderPath)
    {
        Object[] loadedObjects = Resources.LoadAll(folderPath, typeof(Sprite));
        List<Sprite> allSprites = new List<Sprite>();

        foreach (Object obj in loadedObjects)
        {
            if (obj is Sprite)
            {
                allSprites.Add(obj as Sprite);
            }
        }

        return allSprites.ToArray();
    }

    public void ClearUsedSprites()
    {
        usedSpritesNames.Clear();
    }
}
