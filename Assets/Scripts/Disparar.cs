using UnityEngine;

public class Disparar : MonoBehaviour
{
    // prefab de la bala que se instancia al disparar
    public GameObject balaPrefab;

    // tiempo mínimo entre disparos para no disparar infinito
    public float cadencia = 0.2f;

    // contador interno del tiempo desde el último disparo
    private float temporizador = 0f;

    void Update()
    {
        // suma el tiempo transcurrido cada frame
        temporizador += Time.deltaTime;

        // si el jugador pulsa Espacio y ha pasado suficiente tiempo desde el último disparo
        if (Input.GetKey(KeyCode.Space) && temporizador >= cadencia)
        {
            LanzarBala(); // nombre cambiado para no coincidir con la clase
            temporizador = 0f; // resetea el contador
        }
    }

    public AudioClip sonidoDisparo;

    void LanzarBala()
    {
        Instantiate(balaPrefab, transform.position, Quaternion.identity);

        // reproduce el sonido de disparo
        if (sonidoDisparo != null)
            AudioSource.PlayClipAtPoint(sonidoDisparo, transform.position);
    }
}