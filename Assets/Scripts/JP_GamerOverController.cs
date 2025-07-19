using UnityEngine;
using UnityEngine.SceneManagement;

public class JP_GameOverController : MonoBehaviour
{
    public void VoltarParaMenu()
    {
        // Para qualquer música que esteja tocando atualmente
        SoundManager.instance.GetComponent<AudioSource>().Stop();

        // Carrega a cena do Menu
        SceneManager.LoadScene("Menu");
    }
}
