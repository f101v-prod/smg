using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLoader : MonoBehaviour
{
    [SerializeField]
    private string prefsKey;

    [SerializeField]
    private bool defaultValue = true;

    private void Start()
    {
        if (!TryGetComponent<Toggle>(out var toggle))
            return;

        toggle.SetIsOnWithoutNotify(Convert.ToBoolean(
            PlayerPrefs.GetInt(prefsKey, Convert.ToInt32(defaultValue))));
    }
}
