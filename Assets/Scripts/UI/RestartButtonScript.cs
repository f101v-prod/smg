using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
    }
}
