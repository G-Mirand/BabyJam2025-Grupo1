using UnityEngine;
using TMPro;

public class MostrarPontuacaoGameOver : MonoBehaviour
{
    public TextMeshProUGUI textoPontuacao;

    void Start()
    {
        int pontuacaoFinal = PlayerPrefs.GetInt("PontuacaoFinal", 0);
        textoPontuacao.text = "SCORE: " + pontuacaoFinal;
    }
}
