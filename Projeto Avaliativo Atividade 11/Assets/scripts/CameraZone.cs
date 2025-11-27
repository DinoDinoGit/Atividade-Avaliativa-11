using TMPro.Examples;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [Tooltip("A posição para onde a câmera vai quando o player entra na zona.")]
    public Vector3 cameraPosition;

    private void OnDrawGizmos()
    {
        // Mostra no editor onde a câmera vai parar
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(cameraPosition, new Vector3(5, 3, 0)); // só pra visualizar
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraController cam = FindObjectOfType<CameraController>();
            if (cam != null)
            {
                cam.SetCameraPosition(cameraPosition);
            }
        }
    }
}