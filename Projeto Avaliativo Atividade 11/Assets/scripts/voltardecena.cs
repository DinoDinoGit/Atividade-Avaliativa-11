using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBack : MonoBehaviour
{
    [SerializeField] private string previousSceneName;

    public void GoBack()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogWarning("O nome da cena anterior não foi definido!");
        }
    }
}
