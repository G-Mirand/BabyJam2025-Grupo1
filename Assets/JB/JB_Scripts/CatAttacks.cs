using System.Collections;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    // Ponto do ataque, onde a área do ataque será verificada
    public Transform attackPoint;

    // Raio do ataque para detectar objetos dentro do alcance
    public float attackRange = 0.5f;

    // Layer que indica quais objetos podem ser atacados
    public LayerMask attackLayer;

    // Referência para o Animator do gato, para controlar animações
    private Animator animator;

    // Referência ao script que controla o movimento do gato
    private CatMov catMov;

    void Start()
    {
        // Busca os componentes Animator e CatMov no GameObject do gato
        animator = GetComponent<Animator>();
        catMov = GetComponent<CatMov>();
    }

    void Update()
    {
        // Só permite atacar se não estiver atacando no momento (para evitar ataques sobrepostos)
        if (!catMov.isAttacking)
        {
            // Se apertar Z, inicia ataque "Arranhar"
            if (Input.GetKeyDown(KeyCode.Z))
                StartCoroutine(Attack("Arranhar"));

            // Se apertar X, inicia ataque "Morder"
            if (Input.GetKeyDown(KeyCode.X))
                StartCoroutine(Attack("Morder"));

            // Se apertar C, inicia ataque "Regar"
            if (Input.GetKeyDown(KeyCode.C))
                StartCoroutine(Regar()); 
        }
    }

    // Corrotina para os ataques "Arranhar" e "Morder"
    IEnumerator Attack(string tipo)
    {
        catMov.isAttacking = true;           // Bloqueia o movimento durante o ataque
        animator.SetTrigger(tipo);           // Dispara a animação correspondente (Arranhar ou Morder)

        yield return new WaitForSeconds(0.1f);  // Pequeno delay para sincronizar o ataque com a animação

        // Detecta todos os objetos no raio do ataque que pertencem à layer especificada
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);

        // Para cada objeto detectado, tenta chamar o método Destruir passando o tipo de ataque
        foreach (var obj in hits)
        {
            obj.GetComponent<ObjetoDestruivel>()?.Destruir(tipo);
            // O '?' evita erros se o objeto não tiver o componente ObjetoDestruivel
        }

        yield return new WaitForSeconds(0.9f);  // Tempo restante para completar a animação/ataque

        catMov.isAttacking = false;          // Libera o movimento ao fim do ataque
    }

    // Corrotina para o ataque especial "Regar"
    IEnumerator Regar()
    {
        catMov.isAttacking = true;           // Bloqueia movimento durante o ataque
        animator.SetTrigger("Regando");      // Dispara a animação de regar

        yield return new WaitForSeconds(0.1f);  // Delay para sincronizar o ataque

        // Detecta objetos no alcance para aplicar efeito do ataque regar
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);

        // Para cada objeto detectado, chama o método Destruir com o tipo "Regando"
        foreach (var obj in hits)
        {
            obj.GetComponent<ObjetoDestruivel>()?.Destruir("Regando");
        }

        yield return new WaitForSeconds(2.9f);  // Duração total da animação do regar (0.1 + 2.9 = 3 segundos)

        catMov.isAttacking = false;          // Libera o movimento após o ataque
    }

    // Método para desenhar gizmos na cena (apenas no editor)
    void OnDrawGizmosSelected()
    {
        // Se não definiu o ponto de ataque, não desenha nada
        if (attackPoint == null) return;

        // Cor vermelha para o gizmo do raio de ataque
        Gizmos.color = Color.red;

        // Desenha um círculo no ponto de ataque com o raio definido
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
