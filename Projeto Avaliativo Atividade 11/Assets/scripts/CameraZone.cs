using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CameraZone : MonoBehaviour
{
    [Tooltip("Se true, a câmera é forçada para cá mesmo quando o jogador sair; caso contrário é só ao entrar.")]
    public bool persistWhileInside = false;

    private void Reset()
    {
        // garantir que o collider seja trigger por padrão
        var col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        CameraController cam = FindObjectOfType<CameraController>();
        if (cam != null)
        {
            cam.SetCameraPosition(transform.position);
        }
        else
        {
            Debug.LogWarning("CameraController não encontrado na cena. Coloque o script CameraController no GameObject da Main Camera.");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!persistWhileInside) return;
        if (!collision.CompareTag("Player")) return;

        CameraController cam = FindObjectOfType<CameraController>();
        if (cam != null)
        {
            cam.SetCameraPosition(transform.position);
        }
    }
}
