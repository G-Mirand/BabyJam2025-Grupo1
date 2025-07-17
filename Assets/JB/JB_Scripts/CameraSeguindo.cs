using UnityEngine;

public class CameraSeguindo: MonoBehaviour
{
    public Transform player;       // Referência ao player
    public Vector2 minPos;         // Limite mínimo (x e y)
    public Vector2 maxPos;         // Limite máximo (x e y)

    private Vector3 offset;        // Distância inicial entre câmera e player

    void Start()
    {
        // Calcula o offset inicial entre a câmera e o player
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Posição desejada da câmera baseada na posição do player + offset
        Vector3 desiredPosition = player.position + offset;

        // Aplica limites
        float clampX = Mathf.Clamp(desiredPosition.x, minPos.x, maxPos.x);
        float clampY = Mathf.Clamp(desiredPosition.y, minPos.y, maxPos.y);

        // Atualiza posição da câmera com limites
        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }
}
