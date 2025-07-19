using System.Collections;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    public Transform attackPoint;              // Ponto onde o ataque ocorre
    public float attackRange = 0.5f;           // Alcance do ataque
    public LayerMask attackLayer;              // Layer dos objetos que podem ser atacados

    private Animator animator;                 // Animator do gato
    private CatMov catMov;                     // Script de movimentação do gato

    [Header("Efeitos Visuais dos Ataques")]
    public GameObject efeitoArranhao;          // Prefab ou objeto da animação de arranhar (sem sprite do player)
    public GameObject efeitoMordida;           // Prefab ou objeto da animação de mordida (sem sprite do player)

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

        if (efeitoVisual != null)
        {
            efeitoVisual.SetActive(true); // Ativa o efeito visual
            Animator anim = efeitoVisual.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play(0); // Reinicia a animação
            }
        }

        yield return new WaitForSeconds(0.1f); // Delay para sincronizar

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);
        foreach (var obj in hits)
        {
            obj.GetComponent<ObjetoDestruivel>()?.Destruir(tipo);
        }

        yield return new WaitForSeconds(0.5f); // Duração da animação do ataque

        if (efeitoVisual != null)
            efeitoVisual.SetActive(false); // Esconde o efeito

        catMov.isAttacking = false;
    }

    IEnumerator Regar()
    {
        catMov.isAttacking = true;
        animator.SetTrigger("Regando");

        yield return new WaitForSeconds(0.1f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);
        foreach (var obj in hits)
        {
            obj.GetComponent<ObjetoDestruivel>()?.Destruir("Regando");
        }

        yield return new WaitForSeconds(2.9f); // Regar é mais demorado

        catMov.isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
