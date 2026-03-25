using UnityEngine;

public class PortaTeleporte : MonoBehaviour
{
    public CameraManager cameraManager;

    [Tooltip("Referência ao Gerenciador de Objetos")]
    public GerenciadorDeObjetos gerenciador;

    [Tooltip("Transform do jogador")]
    public Transform jogador;

    [Tooltip("Posição para onde o jogador será teleportado")]
    public Vector2 destino;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gerenciador != null && gerenciador.PortaoEstaAberto())
        {
            jogador.position = destino;
            Debug.Log("Jogador teleportado!");
            cameraManager.SwitchCamera(cameraManager.quarto);
        }
    }
}
