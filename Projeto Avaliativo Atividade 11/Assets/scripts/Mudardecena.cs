using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Função pública que pode ser chamada pelo botão
    public void LoadScene(string Level1)
    {
        SceneManager.LoadScene(Level1);
    }

    // Ou uma versão simples sem parâmetro, se quiser fixar a cena:
    public void LoadNextScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
