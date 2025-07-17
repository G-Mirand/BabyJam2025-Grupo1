using UnityEngine;

// Script que faz a câmera seguir o jogador com limites de movimento
public class CameraSeguindo : MonoBehaviour
{
    public Transform player;       // Referência ao Transform do jogador (arraste no Inspector)
    public Vector2 minPos;         // Posição mínima que a câmera pode alcançar (x e y)
    public Vector2 maxPos;         // Posição máxima que a câmera pode alcançar (x e y)

    private Vector3 offset;        // Distância inicial entre a câmera e o jogador

    void Start()
    {
        // Calcula a diferença de posição entre a câmera e o jogador no início
        // Assim a câmera sempre manterá essa mesma distância (offset) do jogador
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Calcula a posição desejada da câmera com base na posição do jogador e no offset
        Vector3 desiredPosition = player.position + offset;

        // Garante que a posição X desejada da câmera não ultrapasse os limites definidos
        float clampX = Mathf.Clamp(desiredPosition.x, minPos.x, maxPos.x);

        // Garante que a posição Y desejada da câmera não ultrapasse os limites definidos
        float clampY = Mathf.Clamp(desiredPosition.y, minPos.y, maxPos.y);

        // Atualiza a posição da câmera com os valores limitados
        // O valor de Z é mantido igual para não alterar a profundidade da câmera
        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }
}
