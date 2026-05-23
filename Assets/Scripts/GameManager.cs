using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // todos los scripts pueden llamar a GameManager

    void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        // para la música inmediatamente al morir para que no siga sonando en GameOver
        if (AudioManager.Instance != null)
            AudioManager.Instance.PararMusica();

        int puntuacion = ScoreManager.Instance.ObtenerPuntuacion();
        PlayerPrefs.SetInt("UltimaPuntuacion", puntuacion); // guardar datos en local con nombre
        int record = PlayerPrefs.GetInt("Record", 0); // recupera el récord guardado
        if (puntuacion > record) // actualizar en caso de superar el actual
        {
            PlayerPrefs.SetInt("Record", puntuacion);
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOverScene"); // carga la escena de GameOver
    }
}