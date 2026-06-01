using UnityEngine;
using UnityEngine.UI;

public class SliderLoader : MonoBehaviour
{
    [SerializeField]
    private string prefsKey;

    [SerializeField]
    private float defaultValue = 0.8f;

    void Start()
    {
        if (!TryGetComponent<Slider>(out var slider))
            return;

        slider.value = PlayerPrefs.GetFloat(prefsKey, defaultValue);
    }

}
