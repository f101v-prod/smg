using System.Collections.Generic;

public class DictionaryHelpers
{
    public static void AdjustValue<Key>(ref Dictionary<Key, int> dict, Key k, int v)
    {
        if (dict.ContainsKey(k))
            dict[k] += v;
        else
            dict[k] = v;
    }
}