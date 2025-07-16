using UnityEngine;
using System.Collections;

// Esse script é usado em objetos que podem ser atacados (ex: sofá, planta).
// Eles trocam de sprite quando recebem golpes suficientes e tremem ao serem atingidos.

public class ObjetoDestruivel : MonoBehaviour
{
    // Sprites que o objeto vai trocar quando for destruído de cada forma
    public Sprite spriteArranhado;
    public Sprite spriteMordido;
    public Sprite spriteRegado;

    // Quantidade de golpes necessários para ativar cada tipo de destruição
    public int golpesParaArranhar = 3;
    public int golpesParaMorder = 3;
    public int golpesParaRegar = 1;

    // Contadores internos para saber quantos golpes o objeto já levou
    private int contadorArranhar = 0;
    private int contadorMorder = 0;
    private int contadorRegar = 0;

    // Se o objeto já foi "destruído" (mudou o sprite), não deve reagir mais a golpes
    private bool foiDestruido = false;

    // Referência ao SpriteRenderer do objeto (pra trocar a imagem dele)
    private SpriteRenderer sr;

    // No começo do jogo (ou quando o objeto ativa), pegamos o SpriteRenderer do objeto
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Função chamada quando o objeto for atacado
    // "tipo" diz se foi um ataque de "Arranhar", "Morder" ou "Regando"
    public void Destruir(string tipo)
    {
        // Se o objeto já trocou de sprite, ignora qualquer ataque
        if (foiDestruido) return;

        // Faz o objeto tremer quando leva o ataque
        StartCoroutine(Tremor(0.5f, 0.10f)); // duração = 0.5s, força = 0.10

        // Escolhe qual tipo de ataque foi, e lida com cada um separadamente
        switch (tipo)
        {
            case "Arranhar":
                contadorArranhar++; // aumenta o número de arranhões
                // Se chegou no número necessário de arranhões e tem sprite arranhado configurado
                if (contadorArranhar >= golpesParaArranhar && spriteArranhado != null)
                {
                    sr.sprite = spriteArranhado; // troca a imagem para o sprite arranhado
                    foiDestruido = true; // marca como destruído, não pode mais reagir
                }
                break;

            case "Morder":
                contadorMorder++; // aumenta o número de mordidas
                if (contadorMorder >= golpesParaMorder && spriteMordido != null)
                {
                    sr.sprite = spriteMordido; // troca para sprite mordido
                    foiDestruido = true;
                }
                break;

            case "Regando":
                contadorRegar++; // aumenta o número de vezes que foi regado
                if (contadorRegar >= golpesParaRegar && spriteRegado != null)
                {
                    sr.sprite = spriteRegado; // troca para sprite regado
                    foiDestruido = true;
                }
                break;
        }
    }

    // Corrotina que faz o objeto "tremer" ao ser atacado
    // Recebe a duração (tempo total) e a intensidade (quanto ele balança)
    IEnumerator Tremor(float duracao, float intensidade)
    {
        // Guarda a posição original do objeto
        Vector3 posicaoOriginal = transform.localPosition;
        float tempo = 0f;

        // Enquanto o tempo de tremor não acabar
        while (tempo < duracao)
        {
            // Gera valores aleatórios para deslocar o objeto um pouquinho
            float offsetX = Random.Range(-1f, 1f) * intensidade;
            float offsetY = Random.Range(-1f, 1f) * intensidade;

            // Aplica o deslocamento na posição
            transform.localPosition = posicaoOriginal + new Vector3(offsetX, offsetY, 0f);

            // Avança o tempo de tremor
            tempo += Time.deltaTime;

            // Espera o próximo frame antes de continuar
            yield return null;
        }

        // Depois que acabar o tremor, volta o objeto pra posição original
        transform.localPosition = posicaoOriginal;
    }
}
