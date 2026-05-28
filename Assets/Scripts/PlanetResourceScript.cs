using System.Collections.Generic;
using UnityEngine;

public class PlanetResourceScript : MonoBehaviour
{
    [SerializeField]
    private List<ResourceCount> resourcesList;

    public List<ResourceCount> GiveResources()
    {
        var res = resourcesList;
        resourcesList = new List<ResourceCount>();
        return res;
    }
}
