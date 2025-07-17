// ObjetoDestruivel.cs
using UnityEngine;
using System.Collections;

public class ObjetoDestruivel : MonoBehaviour
{
    public Sprite spriteArranhado;
    public Sprite spriteMordido;
    public Sprite spriteRegado;

    public int golpesParaArranhar = 3;
    public int golpesParaMorder = 3;
    public int golpesParaRegar = 1;

    public bool aceitaArranhar = true;
    public bool aceitaMorder = true;
    public bool aceitaRegar = true;

    private int contadorArranhar = 0;
    private int contadorMorder = 0;
    private int contadorRegar = 0;

    private bool foiDestruido = false;

    private SpriteRenderer sr;

    // Referência para o gerenciador da sala, deve ser atribuída na Unity
    public GerenciadorDeObjetos gerenciador;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Destruir(string tipo)
    {
        if (foiDestruido) return;

        if ((tipo == "Arranhar" && !aceitaArranhar) ||
            (tipo == "Morder" && !aceitaMorder) ||
            (tipo == "Regando" && !aceitaRegar))
        {
            return;
        }

        StartCoroutine(Tremor(0.5f, 0.10f));

        switch (tipo)
        {
            case "Arranhar":
                contadorArranhar++;
                if (contadorArranhar >= golpesParaArranhar && spriteArranhado != null)
                {
                    sr.sprite = spriteArranhado;
                    foiDestruido = true;
                }
                break;

            case "Morder":
                contadorMorder++;
                if (contadorMorder >= golpesParaMorder && spriteMordido != null)
                {
                    sr.sprite = spriteMordido;
                    foiDestruido = true;
                }
                break;

            case "Regando":
                contadorRegar++;
                if (contadorRegar >= golpesParaRegar && spriteRegado != null)
                {
                    sr.sprite = spriteRegado;
                    foiDestruido = true;
                }
                break;
        }

        // Quando for destruído, avisa o gerenciador
        if (foiDestruido && gerenciador != null)
        {
            gerenciador.AvisarObjetoDestruido(this);
        }
    }

    IEnumerator Tremor(float duracao, float intensidade)
    {
        Vector3 posicaoOriginal = transform.localPosition;
        float tempo = 0f;

        while (tempo < duracao)
        {
            float offsetX = Random.Range(-1f, 1f) * intensidade;
            float offsetY = Random.Range(-1f, 1f) * intensidade;

            transform.localPosition = posicaoOriginal + new Vector3(offsetX, offsetY, 0f);

            tempo += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = posicaoOriginal;
    }
}
