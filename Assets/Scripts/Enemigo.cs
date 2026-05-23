using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // velocidad a la que se mueve el enemigo hacia la izquierda
    public float velocidad = 2f;

    // prefab de la bala que dispara el enemigo
    public GameObject balaPrefab;

    // tiempo entre disparos del enemigo
    public float cadenciaDisparo = 3f;

    // contador interno para los disparos
    private float temporizador = 0f;

    void Update()
    {
        // mueve el enemigo hacia la izquierda igual que los asteroides
        transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);

        // si sale por la izquierda de la pantalla se destruye
        if (transform.position.x < -12f)
            Destroy(gameObject);

        // suma tiempo y dispara cada segundo
        temporizador += Time.deltaTime;
        if (temporizador >= cadenciaDisparo)
        {
            Disparar();
            temporizador = 0f;
        }
    }

    void Disparar()
    {
        // crea una bala en la posición del enemigo que va hacia la izquierda
        GameObject bala = Instantiate(balaPrefab, transform.position, Quaternion.identity);

        // le da la vuelta a la bala para que vaya hacia la izquierda
        bala.transform.localScale = new Vector3(-bala.transform.localScale.x, bala.transform.localScale.y, bala.transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        // si la bala del jugador le toca, se destruye
        if (otro.CompareTag("BalaPJ"))
            Destroy(gameObject);
    }
}