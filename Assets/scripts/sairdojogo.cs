using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Método chamado ao clicar no botão
    public void ExitGame()
    {
        Debug.Log("Saindo do jogo...");

#if UNITY_EDITOR
        // Se estiver rodando no Editor, apenas para o Play Mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Se estiver rodando em Build (jogo compilado), fecha o aplicativo
        Application.Quit();
#endif
    }
}
