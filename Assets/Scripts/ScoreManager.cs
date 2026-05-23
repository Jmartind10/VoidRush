using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI textoPuntuacion;
    private float puntuacion = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        puntuacion += Time.deltaTime * 5f; //suma puntos continuamente mientras juegas
        textoPuntuacion.text = "Puntuación: " + Mathf.FloorToInt(puntuacion); //convierte el número decimal a entero para que se vea limpio
    }

    public int ObtenerPuntuacion() //permite que otros scripts consulten la puntuación actual
    {
        return Mathf.FloorToInt(puntuacion);
    }
    public void AnadirPuntos(int cantidad) // suma puntos extra al recoger la estrella
    {
        puntuacion += cantidad;
    }
}