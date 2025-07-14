using System.Collections;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask attackLayer;

    private Animator animator;
    private CatMov catMov; // referência ao movimento

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
        }
    }

    IEnumerator Attack(string tipo)
    {
        catMov.isAttacking = true;
        animator.SetTrigger(tipo);

        yield return new WaitForSeconds(0.1f); // delay para o ataque "pegar"

        /* Cria uma bolha invisível 
        ao redor do ponto de ataque e detecta todos os objetos que estão dentro dessa área.*/
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);
        /*attackPoint.position: A posição do ponto de ataque 
        (um objeto vazio, filho do gato). Esse ponto é onde a bolha começa 
        (ou seja, a origem do ataque).*/

        /*attackRange: O raio da bolha. Define o tamanho da área em que o ataque pode atingir os objetos. 
        Se um objeto está dentro desse raio, ele é considerado atingido.
        */

        /*
        attackLayer: A camada (layer) que a gente definiu para os objetos que podem ser atingidos. 
        Isso é útil para filtrar quais objetos vão ser detectados 
        (por exemplo, apenas objetos com a layer "Destruivel").
        */
        foreach (var obj in hits)
        {
            obj.GetComponent<ObjetoDestruivel>()?.Destruir(tipo);
        }

        yield return new WaitForSeconds(0.9f); // tempo antes de poder se mover de novo
        catMov.isAttacking = false;
    }


    //Desenha o circulo na teka
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
