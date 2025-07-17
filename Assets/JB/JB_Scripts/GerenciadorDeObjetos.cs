using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeObjetos : MonoBehaviour
{
    [Tooltip("Lista de todos os objetos destruíveis do cômodo")]
    public List<ObjetoDestruivel> objetosDestruiveis;
    // Lista de todos os objetos que o jogador pode destruir. Cada vez que um deles é destruído, ele é removido da lista.

    [Tooltip("Sprite da porta fechada")]
    public Sprite spritePortaFechada;
    // Sprite usado para mostrar a porta fechada no início da cena.

    [Tooltip("Sprite da porta aberta")]
    public Sprite spritePortaAberta;
    // Sprite que será exibido quando todos os objetos forem destruídos (porta "abrindo").

    [Tooltip("Referência ao SpriteRenderer da porta")]
    public SpriteRenderer portaSpriteRenderer;
    // Referência ao componente SpriteRenderer da porta (para trocar visualmente o sprite quando necessário).

    void Start()
    {
        // Começa com a porta fechada
        if (portaSpriteRenderer != null && spritePortaFechada != null)
        {
            portaSpriteRenderer.sprite = spritePortaFechada;
            // No início do jogo, a porta é fechada visualmente
        }
    }

    // Método chamado por cada objeto destruível quando ele for destruído
    public void AvisarObjetoDestruido(ObjetoDestruivel obj)
    {
        if (objetosDestruiveis.Contains(obj))
        {
            objetosDestruiveis.Remove(obj);
            Debug.Log("Objeto destruído: " + obj.name);
            // O objeto é removido da lista. Assim, o sistema sabe quantos ainda restam.

            // Se todos os objetos da lista foram destruídos, abre a porta
            if (objetosDestruiveis.Count == 0)
            {
                AbrirPorta();
            }
        }
    }

    // Método que muda o sprite da porta para a versão aberta
    void AbrirPorta()
    {
        Debug.Log("Todos os objetos destruídos! Porta aberta.");
        if (portaSpriteRenderer != null && spritePortaAberta != null)
        {
            portaSpriteRenderer.sprite = spritePortaAberta;
            // A porta muda visualmente para aberta
        }
    }
}
