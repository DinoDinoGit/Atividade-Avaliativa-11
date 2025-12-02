using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCenaPorToque : MonoBehaviour
{
    public string nomeDaCena; // nome da cena que você quer carregar

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(nomeDaCena);
        }
    }
}
