using UnityEngine;

public class LoopMovement : MonoBehaviour
{
    [Header("Movimento Configurações")]
    public Vector2 direction = Vector2.up; // Direção do movimento (cima por padrão)
    public float distance = 2f;            // Distância total do movimento
    public float speed = 2f;               // Velocidade de movimento

    private Vector3 startPosition;         // Posição inicial do objeto
    private float timer;                   // Temporizador para o movimento senoidal

    void Start()
    {
        // Salva a posição inicial do objeto
        startPosition = transform.position;
    }

    void Update()
    {
        // Faz o objeto se mover suavemente de um lado para outro usando seno (Mathf.Sin)
        timer += Time.deltaTime * speed;

        // Movimento suave de -1 a 1
        float offset = Mathf.Sin(timer) * (distance / 2f);

        // Atualiza a posição do objeto
        transform.position = startPosition + (Vector3)(direction.normalized * offset);
    }
}
