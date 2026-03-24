using UnityEngine;
using TMPro;

public class JP_Pontuacao : MonoBehaviour
{
    public static JP_Pontuacao instance;

    private int pontos = 0;
    public TextMeshProUGUI pontosText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AtualizarTexto();
    }

    public void AdicionarPontos(int valor)
    {
        pontos += valor;

        // Salva a pontuação atual no PlayerPrefs
        PlayerPrefs.SetInt("PontuacaoFinal", pontos);

        AtualizarTexto();
    }

    void AtualizarTexto()
    {
        if (pontosText != null)
            pontosText.text = "Pontos: " + pontos;
    }
}
