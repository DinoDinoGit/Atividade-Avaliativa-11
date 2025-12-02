using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 5f;

    [Header("Pulo")]
    public float jumpForce = 7f;
    public int maxJumps = 2; // 2 = pulo duplo

    [Header("Detecção de chão")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Wall Jump")]
    public float wallJumpForce = 7f;         // força vertical
    public float wallHorizontalNudge = 0.5f; // empurrão horizontal ao pular da parede
    public float wallSlideSpeed = 1f;
    public Transform wallCheck;
    public float wallCheckDistance = 0.45f;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isTouchingWall = false;
    private bool isWallSliding = false;
    private float moveInput;
    private bool isFacingRight = true;
    private int wallDirection = 0; // -1 esquerda, +1 direita

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- Movimento lateral ---
        moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
            moveInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            moveInput = 1f;

        // --- Flip visual ---
        if ((isFacingRight && moveInput < 0) || (!isFacingRight && moveInput > 0))
            Flip();

        // --- Input de pulo ---
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded || jumpCount < maxJumps)
            {
                NormalJump();
            }
            else if (isTouchingWall)
            {
                // Só permite o wall jump se o jogador estiver se movendo na direção oposta à parede
                bool movingAwayFromWall =
                    (wallDirection == 1 && moveInput < 0) ||
                    (wallDirection == -1 && moveInput > 0) ||
                    moveInput == 0;

                if (movingAwayFromWall)
                    WallJump();
            }
        }
    }

    void FixedUpdate()
    {
        // Movimento horizontal
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Segurança: evita erro se faltar referência no Inspector
        if (groundCheck == null || wallCheck == null)
            return;

        // --- Detecta chão ---
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && !wasGrounded)
            jumpCount = 0;

        // --- Detecta paredes dos dois lados ---
        RaycastHit2D hitR = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, wallLayer);
        RaycastHit2D hitL = Physics2D.Raycast(wallCheck.position, Vector2.left, wallCheckDistance, wallLayer);

        if (hitR.collider != null)
        {
            isTouchingWall = true;
            wallDirection = 1;
        }
        else if (hitL.collider != null)
        {
            isTouchingWall = true;
            wallDirection = -1;
        }
        else
        {
            isTouchingWall = false;
            wallDirection = 0;
        }

        // --- Wall slide ---
        if (isTouchingWall && !isGrounded && rb.linearVelocity.y < 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
        else
        {
            isWallSliding = false;
        }

        // --- Resetar pulo se encostar na parede no ar ---
        if (isTouchingWall && !isGrounded)
        {
            jumpCount = 0;
        }
    }

    private void NormalJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        jumpCount++;
    }

    private void WallJump()
    {
        // Aplica impulso para longe da parede e para cima
        float horiz = -wallDirection * wallHorizontalNudge;
        rb.linearVelocity = new Vector2(horiz, wallJumpForce);

        // Reseta contadores
        jumpCount = 0;
        isWallSliding = false;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * wallCheckDistance);
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.left * wallCheckDistance);
        }
    }
}