using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsSelector : MonoBehaviour
{
    public void SelectLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
