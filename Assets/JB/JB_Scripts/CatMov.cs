using UnityEngine;

public class CatMov : MonoBehaviour
{
    public float walkSpeed = 3f;                    // Velocidade ao andar
    public float runSpeed = 6f;                     // Velocidade ao correr
    public float verticalSpeedMultiplier = 0.6f;    // Reduz velocidade no eixo Y (efeito 2.5D)

    private Rigidbody2D rb;             // Referência ao Rigidbody2D (movimento físico)
    private Animator animator;          // Referência ao Animator (para animar o personagem)
    private Vector2 movement;           // Direção normalizada do movimento

    [HideInInspector] public bool isAttacking = false; // Impede o movimento enquanto ataca

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       // Pega o Rigidbody2D
        animator = GetComponent<Animator>();    // Pega o Animator
    }

    void Update()
    {
        if (!isAttacking)
        {
            // Entrada do jogador (sem multiplicador ainda)
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            // Cria vetor normalizado (pra corrigir diagonais)
            Vector2 input = new Vector2(inputX, inputY).normalized;

            // Aplica efeito visual falso de profundidade no eixo Y
            movement = new Vector2(input.x, input.y * verticalSpeedMultiplier);

            // Flip visual horizontal
            if (inputX != 0)
                Flip(inputX);

            // Define se está correndo
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float velocidadeAtual = isRunning ? runSpeed : walkSpeed;

            // Aplica velocidade no Rigidbody
            rb.linearVelocity = movement * velocidadeAtual;

            // Atualiza parâmetros no Animator (usando os nomes corretos)
            animator.SetFloat("Horizontal", input.x);
            animator.SetFloat("Vertical", input.y);
            animator.SetFloat("Velocidade", input.magnitude * velocidadeAtual);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetFloat("Velocidade", 0f);
        }
    }

    // Inverte a escala horizontal do personagem
    void Flip(float direcao)
    {
        transform.localScale = new Vector3(Mathf.Sign(direcao), 1f, 1f);
    }
}
