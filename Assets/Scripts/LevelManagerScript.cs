using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MovementState
{
    Wait,
    Move,
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; } = null;

    public static Action<Transform> OnLevelFinished;

    public MovementState State { get; set; } = MovementState.Wait;

    [SerializeField]
    private List<ResourceCount> requiredResources;

    [SerializeField]
    private GameObject UiObject;

    [SerializeField]
    private GameObject SfxPlayerObject;

    [SerializeField]
    private string nextSceneName; 

    public Dictionary<ResourceKind, int> RequiredResourcesDict { private set; get; } = new()
    {
        [ResourceKind.Red] = 0,
        [ResourceKind.Green] = 0,
        [ResourceKind.Blue] = 0
    };

    public Dictionary<ResourceKind, int> CollectedResources { private set; get; } = new()
    {
        [ResourceKind.Red] = 0,
        [ResourceKind.Green] = 0,
        [ResourceKind.Blue] = 0,
    };

    public Dictionary<ResourceKind, Action<int, int, int>> OnResourcesFound { get; private set; } = new()
    {
        [ResourceKind.Red] = (curr, incomming, required) => {},
        [ResourceKind.Green] = (curr, incomming, required) => {},
        [ResourceKind.Blue] = (curr, incomming, required) => {}
    };

    void Awake()
    {
        UiObject.SetActive(false);
        SfxPlayerObject.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
            foreach (var elem in requiredResources)
                RequiredResourcesDict[elem.kind] += elem.count;

            requiredResources.Clear();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UiObject.SetActive(true);
        SfxPlayerObject.SetActive(true);

        foreach (var ctx in OnResourcesFound)
            ctx.Value?.Invoke(CollectedResources[ctx.Key], 0, RequiredResourcesDict[ctx.Key]);
    }

    public void ResourcesCollected(in Dictionary<ResourceKind, int> newResources)
    {
        bool isLevelFinished = true;

        foreach (var res in newResources)
            CollectedResources[res.Key] += res.Value;

        foreach(var res in CollectedResources)
        {
            OnResourcesFound[res.Key]?.Invoke(
                res.Value,
                0,
                RequiredResourcesDict[res.Key]
            );
        }

        foreach(var res in RequiredResourcesDict)
        {
            if(CollectedResources[res.Key] < res.Value)
            {
                isLevelFinished = false;
                break;
            }
        }

        if (isLevelFinished)          
            FinishLevel();
    }

    public void FinishLevel()
    {
        OnLevelFinished?.Invoke(Camera.main.transform);
        StartCoroutine(ChangeLevelDelayed(1f));
    }

    private IEnumerator ChangeLevelDelayed(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        SceneManager.LoadScene(nextSceneName);
    }
}
