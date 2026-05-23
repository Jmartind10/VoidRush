using UnityEngine;

public class AsteroideSpawner : MonoBehaviour
{
    public GameObject asteroidePequeno;
    public GameObject asteroideMediano;
    public GameObject asteroidGrande;
    public GameObject itemEstrella;
    public GameObject itemRayo;
    public GameObject itemReloj;
    public GameObject enemigo;

    public float tiempoJugando = 0f;

    // --- ASTEROIDES ---
    private float tiempoEntreSpawn = 2.5f;
    private float temporizador = 0f;
    private float tiempoActualSpawn = 2.5f;

    // multiplicador de velocidad de spawn según puntuación, aumenta un 20% cada 1000 puntos
    private float multiplicadorPuntuacion = 1f;

    // --- ENEMIGOS ---
    private float temporizadorEnemigo = 0f;
    private float tiempoActualEnemigo = 8f;

    // --- ITEMS ---
    private float temporizadorItem = 0f;
    public float tiempoEntreItems = 15f;

    private int zonaEnemigo = 0;

    void Update()
    {
        // suma el tiempo transcurrido cada frame
        tiempoJugando += Time.deltaTime;
        temporizador += Time.deltaTime;
        temporizadorEnemigo += Time.deltaTime;
        temporizadorItem += Time.deltaTime;

        // actualiza el multiplicador según puntuación actual, 20% más rápido cada 1000 puntos
        int puntuacion = ScoreManager.Instance.ObtenerPuntuacion();
        multiplicadorPuntuacion = 1f + (puntuacion / 1000) * 0.20f;

        // ASTEROIDES
        if (temporizador >= tiempoActualSpawn)
        {
            SpawnAsteroide();
            temporizador = 0f;

            // reduce gradualmente el tiempo base, el multiplicador lo acorta aún más
            // nunca baja de 0.6s aunque suba mucho la puntuación
            tiempoEntreSpawn = Mathf.Max(0.6f / multiplicadorPuntuacion, tiempoEntreSpawn - 0.005f);

            // añade variación aleatoria para que el spawn no sea robótico
            tiempoActualSpawn = tiempoEntreSpawn + Random.Range(-0.15f, 0.15f);
            tiempoActualSpawn = Mathf.Max(0.6f, tiempoActualSpawn);
        }

        // ENEMIGOS
        if (temporizadorEnemigo >= tiempoActualEnemigo)
        {
            SpawnEnemigo();
            temporizadorEnemigo = 0f;

            // el intervalo entre enemigos baja de 8s a 4s progresivamente en 3 minutos
            float minEnemigo = Mathf.Max(4f, 8f - tiempoJugando / 45f);
            float maxEnemigo = minEnemigo + 2f;
            tiempoActualEnemigo = Random.Range(minEnemigo, maxEnemigo);
        }

        // ITEMS
        if (temporizadorItem >= tiempoEntreItems)
        {
            SpawnItem();
            temporizadorItem = 0f;
        }
    }

    // genera un asteroide en una posición Y aleatoria al borde derecho de la pantalla
    void SpawnAsteroide()
    {
        GameObject asteroide = ElegirAsteroide();
        float posY = Random.Range(-3f, 3f);
        Instantiate(asteroide, new Vector3(12f, posY, 0), Quaternion.identity);
    }

    // intenta spawnear el enemigo en una zona sin asteroides cerca, hasta 3 intentos
    void SpawnEnemigo()
    {
        float[] zonas = { 2.5f, 0f, -2.5f };

        for (int intento = 0; intento < 3; intento++)
        {
            // coge la siguiente zona con pequeña variación
            float posY = zonas[zonaEnemigo % 3] + Random.Range(-0.3f, 0.3f);
            zonaEnemigo++;

            // comprueba si hay asteroides en un radio de 2 unidades en el punto de spawn
            Vector2 puntoSpawn = new Vector2(11f, posY);
            Collider2D[] colisiones = Physics2D.OverlapCircleAll(puntoSpawn, 2f);

            bool hayAsteroide = false;
            foreach (Collider2D col in colisiones)
            {
                // si hay asteroide cerca descarta esta zona
                if (col.CompareTag("Asteroide"))
                {
                    hayAsteroide = true;
                    break;
                }
            }

            if (!hayAsteroide)
            {
                // zona despejada, spawneamos aquí
                Instantiate(enemigo, new Vector3(12f, posY, 0), enemigo.transform.rotation);
                return;
            }
        }

        // si los 3 intentos fallaron spawneamos igualmente para no romper la cadencia
        float posYFinal = zonas[zonaEnemigo % 3];
        Instantiate(enemigo, new Vector3(12f, posYFinal, 0), enemigo.transform.rotation);
    }

    // intenta spawnear el item en una zona sin asteroides ni enemigos cerca, hasta 3 intentos
    void SpawnItem()
    {
        int aleatorio = Random.Range(0, 3);
        GameObject prefabItem;

        if (aleatorio == 0) prefabItem = itemEstrella;
        else if (aleatorio == 1) prefabItem = itemRayo;
        else prefabItem = itemReloj;

        for (int intento = 0; intento < 3; intento++)
        {
            float posY = Random.Range(-3f, 3f);
            Vector2 puntoSpawn = new Vector2(11f, posY);
            Collider2D[] colisiones = Physics2D.OverlapCircleAll(puntoSpawn, 2f);

            bool hayObstaculo = false;
            foreach (Collider2D col in colisiones)
            {
                // descarta la zona si hay asteroide o enemigo cerca
                if (col.CompareTag("Asteroide") || col.CompareTag("Enemigo"))
                {
                    hayObstaculo = true;
                    break;
                }
            }

            if (!hayObstaculo)
            {
                // zona despejada, spawneamos el item aquí
                Instantiate(prefabItem, new Vector3(12f, posY, 0), Quaternion.identity);
                return;
            }
        }

        // si los 3 intentos fallaron retrasa el spawn 3 segundos más
        temporizadorItem -= 3f;
    }

    // devuelve el prefab de asteroide según las probabilidades de cada fase
    GameObject ElegirAsteroide()
    {
        float probabilidadGrande;
        float probabilidadMediano;

        if (tiempoJugando < 90f)
        {
            // fase 1: predominan medianos, algún grande, algún pequeño
            probabilidadGrande = Mathf.Lerp(0.25f, 0.10f, tiempoJugando / 90f);
            probabilidadMediano = Mathf.Lerp(0.55f, 0.45f, tiempoJugando / 90f);
        }
        else if (tiempoJugando < 210f)
        {
            // fase 2: grandes casi desaparecen, suben los pequeños
            probabilidadGrande = Mathf.Lerp(0.10f, 0.02f, (tiempoJugando - 90f) / 120f);
            probabilidadMediano = Mathf.Lerp(0.45f, 0.20f, (tiempoJugando - 90f) / 120f);
        }
        else
        {
            // fase 3: sin grandes, muy pocos medianos, casi todo pequeños
            probabilidadGrande = 0f;
            probabilidadMediano = Mathf.Max(0.05f, 0.20f - (tiempoJugando - 210f) / 120f * 0.15f);
        }

        float random = Random.value;

        if (random < probabilidadGrande)
            return asteroidGrande;
        else if (random < probabilidadGrande + probabilidadMediano)
            return asteroideMediano;
        else
            return asteroidePequeno;
    }
}