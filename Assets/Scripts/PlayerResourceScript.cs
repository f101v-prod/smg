using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceScript : MonoBehaviour
{
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
        LevelManager.Instance.ResourcesCollected(planetResources);
    }
}
