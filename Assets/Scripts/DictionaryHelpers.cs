using System.Collections.Generic;

public class DictionaryHelpers
{
    public static void AdjustValue<TKey>(ref Dictionary<TKey, int> dict, TKey k, int v)
    {
        if (dict.ContainsKey(k))
            dict[k] += v;
        else
            dict[k] = v;
    }
}