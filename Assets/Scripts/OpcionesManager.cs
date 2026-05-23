using UnityEngine;
using TMPro;

public class OpcionesManager : MonoBehaviour
{
    // referencia al panel de opciones para mostrarlo y ocultarlo
    public GameObject panelOpciones;

    // referencia al botón de sonido para cambiar su texto
    public TextMeshProUGUI textoBotonSonido;

    // variable que guarda si el sonido está activado o no
    private bool sonidoActivado = true;

    void Start() // se ejecuta al cargar la escena
    {
        // recupera el estado del sonido guardado, por defecto activado (1)
        sonidoActivado = PlayerPrefs.GetInt("Sonido", 1) == 1;

        // actualiza el texto del botón según el estado del sonido
        ActualizarTextoSonido();
    }

    public void AbrirOpciones() // se llama cuando el jugador pulsa el botón OPCIONES
    {
        panelOpciones.SetActive(true);
    }

    public void CerrarOpciones() // se llama cuando el jugador pulsa el botón CERRAR
    {
        panelOpciones.SetActive(false);
    }

    public void ToggleSonido() // activa o desactiva el sonido
    {
        sonidoActivado = !sonidoActivado;
        PlayerPrefs.SetInt("Sonido", sonidoActivado ? 1 : 0);
        PlayerPrefs.Save();

        // comprueba que el AudioManager existe antes de llamarlo
        if (AudioManager.Instance != null)
            AudioManager.Instance.ToggleVolumen(sonidoActivado);

        // si no existe aplica el volumen directamente
        AudioListener.volume = sonidoActivado ? 1f : 0f;

        ActualizarTextoSonido();
    }

    public void SeleccionarCancion(int numeroCancion) // cambia la canción
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.ReproducirCancion(numeroCancion);

        PlayerPrefs.SetInt("Cancion", numeroCancion);
        PlayerPrefs.Save();
    }

    void ActualizarTextoSonido() // actualiza el texto del botón de sonido
    {
        textoBotonSonido.text = sonidoActivado ? "SONIDO: ON" : "SONIDO: OFF";
    }
}