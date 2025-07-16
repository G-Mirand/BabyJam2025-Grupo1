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

        // Agora sim aplica o efeito falso de 3D no Y
        movement = new Vector2(input.x, input.y * verticalSpeedMultiplier);

        // Flip visual na horizontal
        if (inputX != 0)
            Flip(inputX);

        // Verifica se está correndo
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float velocidadeAtual = isRunning ? runSpeed : walkSpeed;

        // Aplica movimento
        rb.linearVelocity = movement * velocidadeAtual;

        // Parâmetro de animação baseado no input original (antes de multiplicar o Y)
        animator.SetFloat("Velocidade", new Vector2(inputX, inputY).magnitude * velocidadeAtual);
    }
    else
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetFloat("Velocidade", 0f);
    }
}


    // Inverte a escala horizontal do personagem para "virar" pra esquerda ou direita
    void Flip(float direcao)
    {
        transform.localScale = new Vector3(Mathf.Sign(direcao), 1f, 1f);
    }
}
