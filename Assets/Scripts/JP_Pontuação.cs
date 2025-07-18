using UnityEngine;
using TMPro;   

public class JP_Pontuacao : MonoBehaviour
{
    public static JP_Pontuacao instance;   

    private int pontos = 0;                // contador interno
    public TextMeshProUGUI pontosText;     // referência ao texto na cena

    void Awake()
    {
        // garante só um objeto deste tipo
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
        AtualizarTexto();
    }

    void AtualizarTexto()
    {
        if (pontosText != null)
            pontosText.text = "Pontos: " + pontos;
    }
}




