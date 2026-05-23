using UnityEngine;

public class Asteroide : MonoBehaviour
{
    public float velocidad = 3f;

    void Update()
    {
        // mueve los asteroides hacia la izquierda.mov fluido independiente de FPS.SpaceWorld para el movimiento
        transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);

        // Si el asteroide sale de la pantalla por la izquierda, se destruye
        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }
}
