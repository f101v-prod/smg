using System.Collections.Generic;
using UnityEngine;

public class PlanetResourceScript : MonoBehaviour
{
    [SerializeField]
    private List<ResourceCount> resourcesList;

    public Dictionary<ResourceKind, int> GiveResources()
    {
        var res = GetResourcesDict();
        resourcesList = new List<ResourceCount>();
        return res;
    }

    public Dictionary<ResourceKind, int> GetResourcesDict()
    {
        Dictionary<ResourceKind, int> holdingResources = new()
        {
            [ResourceKind.Red] = 0,
            [ResourceKind.Green] = 0,
            [ResourceKind.Blue] = 0
        };

        foreach (var elem in resourcesList)
            holdingResources[elem.kind] += elem.count;

        return holdingResources;
    }
}
