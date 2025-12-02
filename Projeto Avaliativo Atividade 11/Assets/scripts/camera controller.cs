using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum MoveMode { Instant, Smooth }

    [Header("Movement")]
    public MoveMode mode = MoveMode.Smooth;
    [Tooltip("Velocidade usada no modo Smooth (quanto maior, mais rápido).")]
    public float moveSpeed = 6f;
    [Tooltip("Distância abaixo da qual a câmera já considera que chegou (aplica ao Smooth).")]
    public float snapThreshold = 0.05f;

    private Vector3 targetPosition;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (mode == MoveMode.Smooth)
        {
            // Move suavemente mantendo o z
            Vector3 current = transform.position;
            Vector3 desired = new Vector3(targetPosition.x, targetPosition.y, current.z);
            transform.position = Vector3.Lerp(current, desired, 1f - Mathf.Exp(-moveSpeed * Time.deltaTime));

            // Se estiver bem perto, fixa exatamente para evitar micro jitter
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                                 new Vector2(desired.x, desired.y)) < snapThreshold)
            {
                transform.position = desired;
            }
        }
        else // Instant
        {
            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        }
    }

    // Chamado por CameraZone — move para a posição passada (mantendo Z)
    public void SetCameraPosition(Vector3 worldPosition)
    {
        worldPosition.z = transform.position.z;
        targetPosition = worldPosition;

        // Se estiver no modo Instant, aplicar imediatamente
        if (mode == MoveMode.Instant)
            transform.position = targetPosition;
    }

    // Atalho se quiser forçar teleporte por script externo
    public void TeleportTo(Vector3 worldPosition)
    {
        worldPosition.z = transform.position.z;
        targetPosition = worldPosition;
        transform.position = targetPosition;
    }
}
