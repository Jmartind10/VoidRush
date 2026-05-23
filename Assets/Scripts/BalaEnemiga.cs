using UnityEngine;

public class BalaEnemiga : MonoBehaviour
{
    // velocidad de la bala enemiga, va hacia la izquierda hacia el jugador
    public float velocidad = 8f;

    void Update()
    {
        // mueve la bala hacia la izquierda
        transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);

        // si sale de la pantalla se destruye
        if (transform.position.x < -15f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        // si toca al jugador activa el ColisionJugador para que gestione la muerte
        if (otro.CompareTag("Player"))
        {
            Destroy(gameObject);
            // llama al ColisionJugador de la nave para reutilizar la lógica de muerte
            otro.GetComponent<ColisionJugador>().MorirPorBala();
        }
    }
}