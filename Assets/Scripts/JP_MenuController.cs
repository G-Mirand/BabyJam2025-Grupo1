using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
public class JP_MenuController : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;


    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }
    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }
    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
    public GameObject tela1;        
    public GameObject proximaTela;  

    void Start()
    {
        if (proximaTela != null)
            proximaTela.SetActive(false);  // começa invisível
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (proximaTela != null)
                proximaTela.SetActive(true);

            if (tela1 != null)
                tela1.SetActive(false);  

            enabled = false;  // desativa o script depois da troca
        }
    }
}

