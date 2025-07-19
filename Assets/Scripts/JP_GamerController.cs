using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; 

public class JP_GamerController : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float timeCount = 30f;       // tempo inicial
    private bool timeOver = false;

    void Update()
    {
        if (timeOver || timeCount <= 0f)
            return;

        timeCount -= Time.deltaTime;
        timeCount = Mathf.Max(timeCount, 0f); // impede valor negativo

        timeText.text = "Time: " + Mathf.CeilToInt(timeCount).ToString();

        if (timeCount <= 0f)
        {
            timeOver = true;
            StartCoroutine(FimDoTempo());  // inicia corrotina
        }
    }

    // Corrotina que espera 2 segundos e muda de cena
    IEnumerator FimDoTempo()
    {
        Debug.Log("Tempo acabou!");

        // Aqui você pode desativar controles do jogador, se quiser
        // Por exemplo:
        // GameObject.FindWithTag("Player").GetComponent<CatMov>().enabled = false;

        yield return new WaitForSeconds(2f);  // espera 2 segundos

        SceneManager.LoadScene("GameOver");   // carrega cena
    }
}
