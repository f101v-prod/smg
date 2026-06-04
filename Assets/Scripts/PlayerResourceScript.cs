using System;
using UnityEngine;

public class PlayerResourceScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask planetsLayersMask;

    public static Action<Transform> OnResourcesCollected;

    public void Collect(GameObject planet)
    {
        if (planet == null)
            return;

        if (!LayerChecker.IsInLayerMask(planet.layer, planetsLayersMask))
            return;

        if (!planet.TryGetComponent<PlanetResourceScript>(out var planetResourcesController))
            return;

        if (planetResourcesController.IsPlanetEmpty)
            return;

        var planetResources = planetResourcesController.GiveResources();
        LevelManager.Instance.ResourcesCollected(planetResources);
        OnResourcesCollected?.Invoke(planet.transform);
    }
}
