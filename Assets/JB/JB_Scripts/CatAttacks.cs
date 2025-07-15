using System.Collections;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask attackLayer;

    private Animator animator;
    private CatMov catMov;

    void Start()
    {
        animator = GetComponent<Animator>();
        catMov = GetComponent<CatMov>();
    }

    void Update()
    {
        if (!catMov.isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                StartCoroutine(Attack("Arranhar"));

            if (Input.GetKeyDown(KeyCode.X))
                StartCoroutine(Attack("Morder"));

            if (Input.GetKeyDown(KeyCode.C))
                StartCoroutine(Regar()); 
        }
    }

    IEnumerator Attack(string tipo)
    {
        catMov.isAttacking = true;
        animator.SetTrigger(tipo);

        yield return new WaitForSeconds(0.1f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);

        foreach (var obj in hits)
        {
            obj.GetComponent<ObjetoDestruivel>()?.Destruir(tipo);
        }

        yield return new WaitForSeconds(0.9f);
        catMov.isAttacking = false;
    }


    //Realiza o ataque de Regar
    IEnumerator Regar()
    {
    catMov.isAttacking = true;
    animator.SetTrigger("Regando");

    yield return new WaitForSeconds(0.1f); // pequeno delay para dar tempo de "atingir"

    Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);

    foreach (var obj in hits)
    {
        obj.GetComponent<ObjetoDestruivel>()?.Destruir("Regando");
    }

    yield return new WaitForSeconds(2.9f); // 3s no total (0.1s + 2.9s)
    catMov.isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
