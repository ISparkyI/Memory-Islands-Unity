using System.Collections.Generic;
using UnityEngine;

public class TVSpriteAssigner : MonoBehaviour
{
    public GameObject lvlPanel;
    public string[] spriteSheetFolderPaths;
    public CardSizeController cardSizeController;
    public CardShuffler cardShuffler;

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

        foreach (Transform child in lvlPanel.transform)
        {
            TVCardsController cardsController = child.GetComponent<TVCardsController>();
            if (cardsController != null && !child.name.Equals("FreeSpace"))
            {
                cardsController.backSideCard = GetRandomSprite(sprites);
            }
        }

        if (cardShuffler != null)
        {
            cardShuffler.ShuffleCards();
        }
    }

    public Sprite GetRandomSprite(Sprite[] sprites)
    {
        if (sprites.Length == 0) return null;
        return sprites[Random.Range(0, sprites.Length)];
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
}
