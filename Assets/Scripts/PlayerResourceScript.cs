using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceScript : MonoBehaviour
{
    public Dictionary<ResourceKind, int> collectedResources = new();

    [SerializeField]
    private LayerMask planetsLayersMask;

    public void Collect(GameObject planet)
    {
        if (planet == null)
            return;

        if (!LayerChecker.IsInLayerMask(planet.layer, planetsLayersMask))
            return;

        if (!planet.TryGetComponent<PlanetResourceScript>(out var planetResourcesController))
            return;

        var planetResources = planetResourcesController.GiveResources();

        foreach (var elem in planetResources)
        {
            DictionaryHelpers.AdjustValue(ref collectedResources, elem.kind, elem.count);
        }

        LevelManager.Instance.ResourcesCollected(collectedResources);
    }
}
