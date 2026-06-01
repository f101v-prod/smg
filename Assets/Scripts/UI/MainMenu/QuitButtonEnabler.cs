using UnityEngine;

public class QuitButtonEnabler : MonoBehaviour
{
    void Start()
    {
        #if UNITY_WEBGL
            gameObject.SetActive(false);
        #endif
    }
}
