using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ResTextUi : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;

    [SerializeField]
    private ResourceKind resourceKind;
    
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        LevelManager.Instance.OnResourcesFound[resourceKind] += UpdateCollectedResourceText;
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnResourcesFound[resourceKind] -= UpdateCollectedResourceText;
    }

    private void UpdateCollectedResourceText(int current, int incomming, int required)
    {
        if (incomming == 0)
            _textMeshPro.text = $"{current} / {required}";
        else
            _textMeshPro.text = $"{current} <color=green>+ {incomming}</color> / {required}";
    }
}
