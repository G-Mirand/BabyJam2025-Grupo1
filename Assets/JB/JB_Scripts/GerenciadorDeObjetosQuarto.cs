using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeObjetosQuarto : MonoBehaviour
{
    [Tooltip("Lista de todos os objetos destruíveis do cômodo")]
    public List<ObjetoDestruivel> objetosDestruiveis;

    // Flag interna para saber se todos os objetos foram destruídos
    private bool todosDestruídos = false;

    void Start()
    {
        // Apenas garante que a lista esteja limpa de objetos nulos
        objetosDestruiveis.RemoveAll(obj => obj == null);
    }

    // Método chamado por cada objeto destruível quando ele for destruído
    public void AvisarObjetoDestruido(ObjetoDestruivel obj)
    {
        if (objetosDestruiveis.Contains(obj))
        {
            objetosDestruiveis.Remove(obj);
            Debug.Log("Objeto destruído (quarto): " + obj.name);

            if (objetosDestruiveis.Count == 0)
            {
                todosDestruídos = true;
                Debug.Log("Todos os objetos do quarto foram destruídos!");

                // Informa ao GerenciadorDeProgresso que o quarto foi concluído
                if (GerenciadorDeProgresso.instance != null)
                {
                    GerenciadorDeProgresso.instance.QuartoCompleto();
                }
            }
        }
    }

    // Método público para verificar se todos os objetos foram destruídos
    public bool TodosObjetosForamDestruidos()
    {
        return todosDestruídos;
    }
}
