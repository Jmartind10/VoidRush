using UnityEngine;

public class FondoScroll : MonoBehaviour
{
    public float velocidad = 2f;

    // ancho exacto calculado con los valores reales de tu escena
    private float ancho = 18.18f;

    void Update()
    {
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        if (transform.position.x <= -ancho)
        {
            transform.position = new Vector3(transform.position.x + ancho * 2, transform.position.y, transform.position.z);
        }
    }
}