using UnityEngine;

public class CatMov : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float verticalSpeedMultiplier = 0.6f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    [HideInInspector] public bool isAttacking = false; // usado pelo ataque

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            // Movimento
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical") * verticalSpeedMultiplier;

            if (movement.x != 0)
                Flip(movement.x);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float velocidadeAtual = isRunning ? runSpeed : walkSpeed;
            float intensidade = movement.magnitude * velocidadeAtual;

            animator.SetFloat("Velocidade", intensidade);
            rb.linearVelocity = movement.normalized * velocidadeAtual;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetFloat("Velocidade", 0f);
        }
    }

    void Flip(float direcao)
    {
        transform.localScale = new Vector3(Mathf.Sign(direcao), 1f, 1f);
    }
}
