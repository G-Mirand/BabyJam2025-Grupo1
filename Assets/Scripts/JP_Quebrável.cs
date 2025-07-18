using UnityEngine;

public class JP_Quebravel : MonoBehaviour
{
    public int valor = 10; 

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            JP_Pontuacao.instance.AdicionarPontos(valor);
            Destroy(gameObject); 
        }
    }
}
