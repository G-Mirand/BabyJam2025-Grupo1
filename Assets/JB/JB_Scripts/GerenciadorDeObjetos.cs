using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeObjetos : MonoBehaviour
{
    [Tooltip("Lista de todos os objetos destruíveis do cômodo")]
    public List<ObjetoDestruivel> objetosDestruiveis;

    [Tooltip("Sprite da porta fechada")]
    public Sprite spritePortaFechada;

    [Tooltip("Sprite da porta aberta")]
    public Sprite spritePortaAberta;

    [Tooltip("Referência ao SpriteRenderer da porta")]
    public SpriteRenderer portaSpriteRenderer;

    void Start()
    {
        // Começa com a porta fechada
        if (portaSpriteRenderer != null && spritePortaFechada != null)
        {
            portaSpriteRenderer.sprite = spritePortaFechada;
        }
    }

    // Método chamado por cada objeto destruível quando muda para "destruído"
    public void AvisarObjetoDestruido(ObjetoDestruivel obj)
    {
        if (objetosDestruiveis.Contains(obj))
        {
            objetosDestruiveis.Remove(obj);
            Debug.Log("Objeto destruído: " + obj.name);

            // Se todos os objetos forem destruídos, abre a porta (troca o sprite)
            if (objetosDestruiveis.Count == 0)
            {
                AbrirPorta();
            }
        }
    }

    void AbrirPorta()
    {
        Debug.Log("Todos os objetos destruídos! Porta aberta.");
        if (portaSpriteRenderer != null && spritePortaAberta != null)
        {
            portaSpriteRenderer.sprite = spritePortaAberta;
        }
    }
}
