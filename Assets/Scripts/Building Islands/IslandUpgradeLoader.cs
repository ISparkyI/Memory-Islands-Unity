using UnityEngine;

public class IslandUpgradeLoader : MonoBehaviour
{
    public IslandUpgradeManager islandUpgradeManager;

    private void Start()
    {
        islandUpgradeManager.LoadUpgradeLevels();
    }
    private void OnEnable()
    {
        islandUpgradeManager.LoadUpgradeLevels();
    }
}
