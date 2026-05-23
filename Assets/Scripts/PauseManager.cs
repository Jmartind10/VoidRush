using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseManager : MonoBehaviour
{
   
    public GameObject panelPausa;

   
    public GameObject panelOpciones;

    
    private bool pausado = false;

    void Update()
    {
        // si el jugador pulsa Escape, cambia entre pausado y no pausado
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado)
                Reanudar();
            else
                Pausar();
        }
    }

    public void Pausar()
    {
        // congela el juego y muestra el panel de pausa
        Time.timeScale = 0f;
        panelPausa.SetActive(true);
        pausado = true;
    }

    public void Reanudar()
    {
        // reanuda el juego y oculta todo
        Time.timeScale = 1f;
        panelPausa.SetActive(false);
        panelOpciones.SetActive(false);
        pausado = false;
    }

    public void AbrirOpciones()
    {
        // oculta la pausa y muestra las opciones
        panelPausa.SetActive(false);
        panelOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        // vuelve al panel de pausa desde opciones
        panelOpciones.SetActive(false);
        panelPausa.SetActive(true);
    }

    public void VolverAlMenu()
    {
        // resetea el tiempo antes de cambiar de escena
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}