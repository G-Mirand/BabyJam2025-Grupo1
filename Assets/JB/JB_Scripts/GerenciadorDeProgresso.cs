using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GerenciadorDeProgresso : MonoBehaviour
{
    public static GerenciadorDeProgresso instance;

    private bool salaConcluida = false;
    private bool quartoConcluido = false;
    private bool vitoriaRegistrada = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SalaCompleta()
    {
        salaConcluida = true;
        VerificarVitoria();
    }

    public void QuartoCompleto()
    {
        quartoConcluido = true;
        VerificarVitoria();
    }

    void VerificarVitoria()
    {
        if (salaConcluida && quartoConcluido && !vitoriaRegistrada)
        {
            vitoriaRegistrada = true;
            StartCoroutine(TransicaoParaCenaDeVitoria());
        }
    }

    IEnumerator TransicaoParaCenaDeVitoria()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Congratulations");
    }
}
