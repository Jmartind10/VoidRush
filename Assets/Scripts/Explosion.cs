using UnityEngine;

public class Explosion : MonoBehaviour
{
    // tiempo que dura la explosión antes de desaparecer
    public float duracion = 1.5f;

    void Start()
    {
        // se destruye solo cuando termina la animación
        Destroy(gameObject, duracion);
    }
}