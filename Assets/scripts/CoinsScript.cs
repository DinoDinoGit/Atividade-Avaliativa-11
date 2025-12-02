using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Encontra o GameManager na cena
            GameManager gm = FindObjectOfType<GameManager>();
            gm.ColetarMoeda();

            Destroy(gameObject); // remove a moeda
        }
    }
}
