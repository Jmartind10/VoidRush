using UnityEngine;

public class Bala : MonoBehaviour
{
    // velocidad a la que viaja la bala hacia la derecha
    public float velocidad = 10f;

    void Update()
    {
        // mueve la bala hacia la derecha continuamente
        transform.Translate(transform.right * velocidad * Time.deltaTime);

        // si la bala sale de la pantalla por la derecha se destruye para no desperdiciar memoria
        if (transform.position.x > 15f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        // si la bala toca algo con tag Asteroide o Enemigo lo destruye y se destruye ella también
        if (otro.CompareTag("Asteroide") || otro.CompareTag("Enemigo"))
        {
            Destroy(otro.gameObject);
            Destroy(gameObject);
        }
    }
}