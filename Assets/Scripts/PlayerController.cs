using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Velocidad de la nave
    public float moveSpeed = 12f;
    public float limiteSuperior = 4f;
    public float limiteInferior = -4f;
    // referencia a la corrutina de velocidad activa para poder cancelarla
    private Coroutine corrutinaVelocidad;

    void Update()
    {
        // Detección de flechas o w/s
        float moveY = Input.GetAxis("Vertical");
        // mueve la nave en eje y. mov fluido independiente de FPS.SpaceWorld para el movimiento
        transform.Translate(Vector3.up * moveY * moveSpeed * Time.deltaTime, Space.World);
        // Limitar posición vertical

        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, limiteInferior, limiteSuperior);
        transform.position = pos;

    }
    // guarda la referencia de la corrutina activa
    public void GuardarCorrutina(Coroutine c)
    {
        corrutinaVelocidad = c;
    }

    // cancela el efecto de velocidad activo y resetea moveSpeed a su valor base
    public void CancelarEfectoVelocidad()
    {
        if (corrutinaVelocidad != null)
        {
            StopCoroutine(corrutinaVelocidad);
            corrutinaVelocidad = null;

            // resetea la velocidad al valor base siempre
            moveSpeed = 12f;
        }
    }
    // inicia el efecto de velocidad temporal desde el jugador para que no dependa del item
    public void AplicarEfectoVelocidad(float cantidad, float duracion)
    {
        corrutinaVelocidad = StartCoroutine(CorrutinaVelocidad(cantidad, duracion));
    }

    // aplica el cambio de velocidad y lo revierte tras la duración indicada
    System.Collections.IEnumerator CorrutinaVelocidad(float cantidad, float duracion)
    {
        moveSpeed += cantidad;
        yield return new WaitForSeconds(duracion);
        moveSpeed -= cantidad;
        corrutinaVelocidad = null;
    }
}
