using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public void Jugar() // se llama cuando el jugador pulsa el botón JUGAR
    {

        SceneManager.LoadScene("GameScene");
    }

    public void Salir() // se llama cuando el jugador pulsa el botón SALIR
    {

        Application.Quit();
    }
}