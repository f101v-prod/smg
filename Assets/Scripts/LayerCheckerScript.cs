using UnityEngine;

public class LayerChecker
{
    public static bool IsInLayerMask(int layer, LayerMask mask) => (mask.value & (1 << layer)) != 0;
    public static bool IsInLayerMask(GameObject obj, LayerMask mask) => IsInLayerMask(obj.layer, mask);
}
