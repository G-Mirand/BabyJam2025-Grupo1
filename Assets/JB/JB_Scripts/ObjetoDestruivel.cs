using UnityEngine;

public class ObjetoDestruivel : MonoBehaviour
{
    public Sprite spriteArranhado;
    public Sprite spriteMordido;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Destruir(string tipo)
    {
        if (tipo == "Arranhar" && spriteArranhado != null)
        {
            sr.sprite = spriteArranhado;
        }
        else if (tipo == "Morder" && spriteMordido != null)
        {
            sr.sprite = spriteMordido;
        }
    }
}
