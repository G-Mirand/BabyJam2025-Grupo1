using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeObjetos : MonoBehaviour
{
    [Tooltip("Lista de todos os objetos destruíveis do cômodo")]
    public List<ObjetoDestruivel> objetosDestruiveis;

    [Header("Porta")]
    [Tooltip("Sprite da porta fechada")]
    public Sprite spritePortaFechada;

    [Tooltip("Sprite da porta aberta")]
    public Sprite spritePortaAberta;

    [Tooltip("Referência ao SpriteRenderer da porta")]
    public SpriteRenderer portaSpriteRenderer;

    [Header("Quadro")]
    [Tooltip("Sprite do quadro intacto")]
    public Sprite spriteQuadroIntacto;

    [Tooltip("Sprite do quadro destruído")]
    public Sprite spriteQuadroDestruido;

    [Tooltip("Referência ao SpriteRenderer do quadro")]
    public SpriteRenderer quadroSpriteRenderer;

    // Flag interna para saber se a porta está aberta
    private bool portaAberta = false;

    void Start()
    {
        // Começa com a porta fechada e quadro intacto
        if (portaSpriteRenderer != null && spritePortaFechada != null)
        {
            portaSpriteRenderer.sprite = spritePortaFechada;
        }

        if (quadroSpriteRenderer != null && spriteQuadroIntacto != null)
        {
            quadroSpriteRenderer.sprite = spriteQuadroIntacto;
        }
    }

    // Método chamado por cada objeto destruível quando ele for destruído
    public void AvisarObjetoDestruido(ObjetoDestruivel obj)
    {
        if (objetosDestruiveis.Contains(obj))
        {
            objetosDestruiveis.Remove(obj);
            Debug.Log("Objeto destruído: " + obj.name);

            if (objetosDestruiveis.Count == 0)
            {
                AbrirPortaEQuebrarQuadro();

                // Informa ao GerenciadorDeProgresso que a sala foi concluída
                if (GerenciadorDeProgresso.instance != null)
                {
                    GerenciadorDeProgresso.instance.SalaCompleta();
                }
            }
        }
    }

    // Porta abre e quadro muda para destruído
    void AbrirPortaEQuebrarQuadro()
    {
        Debug.Log("Todos os objetos destruídos! Porta aberta e quadro quebrado.");

        if (portaSpriteRenderer != null && spritePortaAberta != null)
        {
            portaSpriteRenderer.sprite = spritePortaAberta;
        }

        if (quadroSpriteRenderer != null && spriteQuadroDestruido != null)
        {
            quadroSpriteRenderer.sprite = spriteQuadroDestruido;
        }

        portaAberta = true;
    }

    // Método público para verificar se a porta está aberta
    public bool PortaoEstaAberto()
    {
        return portaAberta;
    }
}
