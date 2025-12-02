using UnityEngine;

public class SmoothMover : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public Vector3 direction = Vector3.right; // Direção que você escolher
    public float speed = 5f;                  // Velocidade configurável
    public float smoothness = 10f;            // Quanto maior, mais suave

    private Vector3 targetPosition;

    void Start()
    {
        // Define a primeira posição alvo
        targetPosition = transform.position;
    }

    void Update()
    {
        // Atualiza a posição alvo continuamente
        targetPosition += direction.normalized * speed * Time.deltaTime;

        // Movimento suave usando Lerp
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothness);
    }
}
