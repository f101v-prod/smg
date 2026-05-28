using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceScript : MonoBehaviour
{
    public Dictionary<ResourceKind, int> collectedResources = new();

    [SerializeField]
    private LayerMask planetsLayersMask;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!LayerChecker.IsInLayerMask(collision.gameObject.layer, planetsLayersMask))
            return;

        if (!collision.gameObject.TryGetComponent<PlanetResourceScript>(out var planetResourcesController))
            return;

        var planetResources = planetResourcesController.GiveResources();

        foreach (var elem in planetResources)
        {
            DictionaryHelpers.AdjustValue(ref collectedResources, elem.kind, elem.count);
        }

        LevelManager.Instance.ResourcesCollected(collectedResources);
    }
}
