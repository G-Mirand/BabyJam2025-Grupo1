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
    private bool pontuado = false; // garante que a pontuação só será dada uma vez

    private SpriteRenderer sr;

    public int valorPontuacao = 10; // valor de pontos ao destruir

    // Referências para os gerenciadores (um ou outro será usado)
    public GerenciadorDeObjetos gerenciadorSala;
    public GerenciadorDeObjetosQuarto gerenciadorQuarto;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Destruir(string tipo)
    {
        if (foiDestruido) return;

        // Verifica se aceita o tipo de golpe
        if ((tipo == "Arranhar" && !aceitaArranhar) ||
            (tipo == "Morder" && !aceitaMorder) ||
            (tipo == "Regando" && !aceitaRegar))
        {
            return;
        }

        StartCoroutine(Tremor(0.5f, 0.10f)); // tremor visual

        switch (tipo)
        {
            case "Arranhar":
                contadorArranhar++;
                if (contadorArranhar >= golpesParaArranhar && spriteArranhado != null)
                {
                    sr.sprite = spriteArranhado;
                    ChecarSom();
                    AdicionarPontuacao();
                    foiDestruido = true;
                    AvisarGerenciador();
                }
                break;

            case "Morder":
                contadorMorder++;
                if (contadorMorder >= golpesParaMorder && spriteMordido != null)
                {
                    sr.sprite = spriteMordido;
                    ChecarSom();
                    AdicionarPontuacao();
                    foiDestruido = true;
                    AvisarGerenciador();
                }
                break;

            case "Regando":
                contadorRegar++;

                // Garante que não chama a corrotina mais de uma vez
                if (!foiDestruido && contadorRegar >= golpesParaRegar && spriteRegado != null)
                {
                    foiDestruido = true; // Marca como destruído antes de esperar
                    StartCoroutine(AguardarAnimacaoRegar());
                }
    break;
        }
    }

    // Executa tremor visual
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

    // Aguarda um tempo antes de aplicar a destruição "regado"
    IEnumerator AguardarAnimacaoRegar()
    {
        yield return new WaitForSeconds(2.9f); // tempo da animação do player regando

        sr.sprite = spriteRegado;
        ChecarSom();
        AdicionarPontuacao();
        foiDestruido = true;
        AvisarGerenciador();
    }

    // Envia som baseado na tag do objeto
    void ChecarSom()
    {
        if (gameObject.CompareTag("Vidro"))
        {
            SoundManager.PlaySound(SoundType.VIDROQUEBRANDO);
        }
        else if (gameObject.CompareTag("Madeira"))
        {
            SoundManager.PlaySound(SoundType.MADEIRAQUEBRANDO);
        }
        else if (gameObject.CompareTag("Tecido"))
        {
            SoundManager.PlaySound(SoundType.TECIDORESGANDO);
        }
    }

    // Adiciona pontuação uma única vez
    void AdicionarPontuacao()
    {
        if (!pontuado)
        {
            JP_Pontuacao.instance.AdicionarPontos(valorPontuacao);
            SoundManager.PlaySound(SoundType.PONTUEI); // Som de pontuação
            pontuado = true;
        }
    }

    // Avisa o gerenciador correto da destruição
    void AvisarGerenciador()
    {
        if (gerenciadorSala != null)
            gerenciadorSala.AvisarObjetoDestruido(this);
        else if (gerenciadorQuarto != null)
            gerenciadorQuarto.AvisarObjetoDestruido(this);
    }
}
