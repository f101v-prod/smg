using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; } = null;

    [SerializeField]
    private List<ResourceCount> requiredResources;

    private Dictionary<ResourceKind, int> requiredResourcesDict;

    [SerializeField]
    private string nextSceneName; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            requiredResourcesDict = new Dictionary<ResourceKind, int>();
            foreach (var elem in requiredResources)
                DictionaryHelpers.AdjustValue(ref requiredResourcesDict, elem.kind, elem.count);

            requiredResources.Clear();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ResourcesCollected(in Dictionary<ResourceKind, int> collectedResources)
    {
        bool isLevelFinished = true;

        var keysToRemove = new List<ResourceKind>();

        foreach(var res in requiredResourcesDict)
        {
            if (!collectedResources.ContainsKey(res.Key))
            {
                isLevelFinished = false;
                break;
            }

            if(collectedResources[res.Key] < res.Value)
            {
                isLevelFinished = false;
                break;
            }

            keysToRemove.Add(res.Key);
        }

        foreach (var key in keysToRemove)
            requiredResourcesDict.Remove(key);

        if (isLevelFinished)          
            FinishLevel();
    }

    public void FinishLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
