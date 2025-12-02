using UnityEngine;

public class GateController : MonoBehaviour
{
    public Collider2D hitboxPortao; // a hitbox que impede o player
    public GameObject portaVisual;  // parte visual do portão

    public void AbrirPortao()
    {
        if (hitboxPortao != null)
            hitboxPortao.enabled = false; // A barreira some

        if (portaVisual != null)
            portaVisual.SetActive(false); // Visual do portão desaparece
    }
}
