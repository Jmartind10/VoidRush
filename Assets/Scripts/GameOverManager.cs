using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{

    public TextMeshProUGUI textoPuntuacion;
    public TextMeshProUGUI textoRecord;

    void Start() // se ejecuta automáticamente al cargar la escena
    {
        // recupera la puntuación de la última partida guardada 
        int puntuacion = PlayerPrefs.GetInt("UltimaPuntuacion", 0);

        // recupera el mejor récord guardado 
        int record = PlayerPrefs.GetInt("Record", 0);

        // escribe la puntuación 
        textoPuntuacion.text = "Puntuación: " + puntuacion;

        // escribe el récord 
        textoRecord.text = "Récord: " + record;
    }

    public void Reintentar() // se llama cuando el jugador pulsa el botón de reintentar
    {
        // recupera la canción guardada y la reproduce desde el inicio
        int cancion = PlayerPrefs.GetInt("Cancion", 1);
        AudioManager.Instance.ReproducirCancion(cancion);
        SceneManager.LoadScene("GameScene");
    }
    public void VolverAlMenu() // se llama cuando el jugador pulsa el botón de volver al menú
    {

        SceneManager.LoadScene("MenuPrincipal");
    }
}