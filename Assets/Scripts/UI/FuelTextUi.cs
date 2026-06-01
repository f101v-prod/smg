using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FuelTextUi : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PlayerMovementScript.OnNewFuelCalculated += UpdateRequiredFuelText;
    }

    private void OnDisable()
    {
        PlayerMovementScript.OnNewFuelCalculated -= UpdateRequiredFuelText;
    }

    private void UpdateRequiredFuelText(int current, int required)
    {
        if (required == 0)
            _textMeshPro.text = $"{current}";
        else
            _textMeshPro.text = $"{current}<color=red>-{required}</color>";
    }
}
