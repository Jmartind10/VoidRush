using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;


    private AudioSource audioSource;


    public AudioClip[] canciones;


    private int cancionActual = 0;

    void Awake()
    {
        // si ya existe un AudioManager no crea otro, así la música no se interrumpe al cambiar de escena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // mantiene el objeto entre escenas
        }
        else
        {
            Destroy(gameObject); // destruye el duplicado
            return;
        }

        // obtiene el AudioSource del objeto
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // recupera la canción guardada en local
        cancionActual = PlayerPrefs.GetInt("Cancion", 0);

        // recupera el estado del volumen guardado
        bool sonidoActivado = PlayerPrefs.GetInt("Sonido", 1) == 1;
        AudioListener.volume = sonidoActivado ? 1f : 0f;

        // empieza a reproducir la canción
        ReproducirCancion(cancionActual);
    }

    public void ReproducirCancion(int indice)
    {
        // guarda qué canción está sonando
        cancionActual = indice;
        PlayerPrefs.SetInt("Cancion", cancionActual);

        // tiempos de inicio para quitar el relleno del principio de cada canción
        float[] tiemposInicio = { 0f, 6f, 7f, 6f };

        // cambia la canción y la reproduce desde el segundo indicado
        audioSource.clip = canciones[indice];
        audioSource.time = tiemposInicio[indice];
        audioSource.Play();
    }

    public void ToggleVolumen(bool activado)
    {
        // activa o desactiva todo el audio del juego
        AudioListener.volume = activado ? 1f : 0f;
    }
    public void PararMusica() // para la música completamente
    {
        audioSource.Stop();
    }
}