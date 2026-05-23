using UnityEngine;

public class Item : MonoBehaviour
{
    // tipo de item: da puntos, aumenta velocidad o la reduce
    public enum TipoItem { Puntos, VelocidadMas, VelocidadMenos }
    public TipoItem tipo;

    // velocidad a la que se mueve el item hacia la izquierda
    public float velocidad = 3f;

    // puntos que da la estrella (se suman una sola vez al recogerla)
    public int puntosExtra = 50;

    // cantidad que se suma o resta temporalmente a la velocidad del jugador
    public float cantidadVelocidad = 1.5f;

    // duración en segundos del efecto de velocidad
    public float duracionEfecto = 5f;

    // sonido que suena al recoger el item
    public AudioClip sonidoRecogida;

    void Update()
    {
        // mueve el item hacia la izquierda igual que los asteroides
        transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);

        // si sale de pantalla por la izquierda se destruye solo
        if (transform.position.x < -13f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // solo reacciona si toca al jugador
        if (!other.CompareTag("Player")) return;

        PlayerController pc = other.GetComponent<PlayerController>();
        ScoreManager sm = ScoreManager.Instance;

        switch (tipo)
        {
            case TipoItem.Puntos:
                // suma los puntos extra una sola vez al recogerla
                sm.AnadirPuntos(puntosExtra);
                break;

            case TipoItem.VelocidadMas:
                // cancela efecto anterior y aplica el nuevo desde el jugador
                pc.CancelarEfectoVelocidad();
                pc.AplicarEfectoVelocidad(cantidadVelocidad, duracionEfecto);
                break;

            case TipoItem.VelocidadMenos:
                // cancela efecto anterior y aplica el nuevo desde el jugador
                pc.CancelarEfectoVelocidad();
                pc.AplicarEfectoVelocidad(-cantidadVelocidad, duracionEfecto);
                break;
        }

        // reproduce el sonido de recogida
        if (sonidoRecogida != null)
            AudioSource.PlayClipAtPoint(sonidoRecogida, transform.position);

        // desactiva el sprite y el collider para que no se recoja dos veces
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // destruye el item
        Destroy(gameObject);
    }
}