using UnityEngine;

public class ColisionJugador : MonoBehaviour
{
    // prefab de la explosión que se instancia al morir
    public GameObject explosionPrefab;

    // sonido de muerte
    public AudioClip sonidoMuerte;

    // evita que se active la muerte más de una vez
    private bool haMuerto = false;

    void OnTriggerEnter2D(Collider2D otro)
    {
        // si ya ha muerto ignora cualquier colisión posterior
        if (haMuerto) return;

        if (otro.CompareTag("Asteroide"))
        {
            Morir();
        }
    }

    public void MorirPorBala()
    {
        // si ya ha muerto ignora la llamada
        if (haMuerto) return;
        Morir();
    }

    void Morir()
    {
        // marca como muerto para no volver a entrar
        haMuerto = true;

        // crea la explosión exactamente donde está la nave
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // reproduce el sonido de muerte
        if (sonidoMuerte != null)
            AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position);

        // desactiva todos los SpriteRenderer de la nave y sus hijos
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            sr.enabled = false;

        // desactiva el movimiento y el disparo de la nave
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Disparar>().enabled = false;

        // llama al GameOver tras la explosión
        GameManager.Instance.Invoke("GameOver", 1f);
    }
}