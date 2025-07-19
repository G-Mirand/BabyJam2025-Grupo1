using System.Collections;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    public Transform attackPoint;              // Ponto onde o ataque ocorre
    public float attackRange = 0.5f;           // Alcance do ataque
    public LayerMask attackLayer;              // Layer dos objetos que podem ser atacados

    private Animator animator;                 // Animator do personagem
    private CatMov catMov;                     // Script de movimentação do personagem

    [Header("Efeitos Visuais dos Ataques")]
    public GameObject efeitoArranhao;          // Efeito de arranhar (sem sprite do personagem)
    public GameObject efeitoMordida;           // Efeito de mordida (sem sprite do personagem)

    void Start()
    {
        animator = GetComponent<Animator>();
        catMov = GetComponent<CatMov>();

        // Garante que os efeitos comecem desativados
        if (efeitoArranhao) efeitoArranhao.SetActive(false);
        if (efeitoMordida) efeitoMordida.SetActive(false);
    }

    void Update()
    {
        if (!catMov.isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine(Attack("Arranhar", efeitoArranhao));
                SoundManager.PlaySound(SoundType.ARRANHAO);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(Attack("Morder", efeitoMordida));
                SoundManager.PlaySound(SoundType.MORDIDA);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(Regar());
                SoundManager.PlaySound(SoundType.REGAR);
            }
        }
    }

    // Corrotina para os ataques visuais + dano
    IEnumerator Attack(string tipo, GameObject efeitoVisual)
    {
        catMov.isAttacking = true;

        // Ativa e reinicia a animação do efeito
        if (efeitoVisual != null)
        {
            efeitoVisual.SetActive(true);
            Animator anim = efeitoVisual.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play(0); // Reinicia a animação
            }
        }

        yield return new WaitForSeconds(0.1f); // Pequeno delay para sincronizar com a animação

        // Aplica o efeito nos objetos atingidos
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);
        foreach (var obj in hits)
        {
            obj.GetComponent<ObjetoDestruivel>()?.Destruir(tipo);
        }

        yield return new WaitForSeconds(0.5f); // Tempo da animação

        if (efeitoVisual != null)
            efeitoVisual.SetActive(false); // Esconde o efeito

        catMov.isAttacking = false;
    }

    // Corrotina específica para o ataque "Regar" com triggers no Animator
    IEnumerator Regar()
    {
        catMov.isAttacking = true;

        // Usa trigger diferente para cada lado
        if (transform.localScale.x > 0)
        {
            animator.SetTrigger("RegandoDireita");
        }
        else
        {
            animator.SetTrigger("RegandoEsquerda");
        }

        yield return new WaitForSeconds(3f); // Duração total da animação de regar (3 segundos)

        // Aplica efeito nos objetos atingidos
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);
        foreach (var obj in hits)
        {
            obj.GetComponent<ObjetoDestruivel>()?.Destruir("Regando");
        }

        catMov.isAttacking = false;
    }

    // Visualização do alcance no editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
