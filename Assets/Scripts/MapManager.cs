using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Genera un camino aleatorio sobre una malla de habitaciones y maneja el loop:
// puerta correcta = sigues; puerta incorrecta = vuelves al inicio por el lado opuesto.
// Va en un objeto vacío de la escena llamado "MapManager".
public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [Header("Malla")]
    public int columnas = 5;
    public int filas = 3;
    public Vector2 tamanoHabitacion = new Vector2(16f, 10f); // X = ancho, Z = alto

    [Header("Camino")]
    [Tooltip("Habitaciones que se recorren antes de la meta. Cuenta la de inicio.")]
    public int minHabitacionesAntesDeMeta = 3;
    public int maxHabitacionesAntesDeMeta = 5;
    [Tooltip("Evita que dos habitaciones NO consecutivas queden pegadas (así ninguna puerta mala da a otra habitación real).")]
    public bool sinAtajos = true;
    [Tooltip("Si está activo, la puerta por la que viniste también te manda al inicio.")]
    public bool volverAtrasEsError = false;
    [Tooltip("0 = camino distinto en cada partida. Cualquier otro número repite siempre el mismo camino (útil para depurar).")]
    public int semilla = 0;

    [Header("Prefabs (arrastra el prefab de cada habitación)")]
    public Room prefabInicio;
    public Room prefabMeta;
    public Room[] prefabsIntermedios;

    [Header("Escena")]
    public Transform jugador;
    public Transform contenedorMapa;     // objeto vacío "Mapa" en (0,0,0)
    public float margenAparicion = 2.5f; // distancia a la puerta al reaparecer

    public UnityEvent alGanar;

    readonly List<Vector2Int> camino = new List<Vector2Int>();
    readonly List<Room> habitaciones = new List<Room>();
    bool terminado;
    bool atajosProhibidosAhora;

    void Awake()
    {
        Instance = this;
        if (contenedorMapa == null) contenedorMapa = transform;
    }

    void Start()
    {
        GenerarPartida();
    }

    [ContextMenu("Regenerar mapa")]
    public void GenerarPartida()
    {
        if (!DatosValidos()) return;

        if (semilla != 0) Random.InitState(semilla);

        LimpiarMapa();
        terminado = false;

        if (!GenerarCamino(sinAtajos))
        {
            Debug.LogWarning("MapManager: no se pudo generar el camino sin atajos; se genera permitiéndolos.");
            if (!GenerarCamino(false))
            {
                Debug.LogError("MapManager: no se pudo generar ningún camino. Revisa columnas, filas y los mínimos.");
                return;
            }
        }

        ConstruirHabitaciones();
        Teletransportar(habitaciones[0].PuntoCercaDePuerta(Dir.Oeste, margenAparicion));
    }

    // ---------------------------------------------------------------- camino

    bool GenerarCamino(bool prohibirAtajos)
    {
        atajosProhibidosAhora = prohibirAtajos;

        int minTotal = minHabitacionesAntesDeMeta + 1;                       // + la meta
        int maxTotal = Mathf.Max(minTotal, maxHabitacionesAntesDeMeta + 1);

        for (int intento = 0; intento < 200; intento++)
        {
            int largo = Random.Range(minTotal, maxTotal + 1);

            camino.Clear();
            camino.Add(new Vector2Int(0, Random.Range(0, filas))); // columna 0 = extremo izquierdo

            if (Extender(largo)) return true;
        }
        return false;
    }

    // Camino aleatorio sin repetir celdas, con retroceso si se queda atascado.
    bool Extender(int largo)
    {
        if (camino.Count == largo) return true;

        Vector2Int actual = camino[camino.Count - 1];

        List<Dir> opciones = new List<Dir>(DirUtil.Cuatro);
        Mezclar(opciones);

        foreach (Dir d in opciones)
        {
            Vector2Int siguiente = actual + DirUtil.ACelda(d);
            if (!CeldaValida(siguiente)) continue;

            camino.Add(siguiente);
            if (Extender(largo)) return true;
            camino.RemoveAt(camino.Count - 1);
        }
        return false;
    }

    bool CeldaValida(Vector2Int c)
    {
        if (c.x < 0 || c.x >= columnas || c.y < 0 || c.y >= filas) return false;
        if (camino.Contains(c)) return false;

        if (atajosProhibidosAhora)
        {
            // La celda nueva solo puede tocar a la última del camino.
            Vector2Int ultima = camino[camino.Count - 1];
            foreach (Dir d in DirUtil.Cuatro)
            {
                Vector2Int vecina = c + DirUtil.ACelda(d);
                if (vecina != ultima && camino.Contains(vecina)) return false;
            }
        }
        return true;
    }

    void Mezclar(List<Dir> lista)
    {
        for (int i = lista.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Dir tmp = lista[i];
            lista[i] = lista[j];
            lista[j] = tmp;
        }
    }

    // ------------------------------------------------------------ construcción

    void ConstruirHabitaciones()
    {
        for (int i = 0; i < camino.Count; i++)
        {
            bool esInicio = i == 0;
            bool esMeta = i == camino.Count - 1;

            Room prefab;
            if (esInicio) prefab = prefabInicio;
            else if (esMeta) prefab = prefabMeta;
            else prefab = prefabsIntermedios[Random.Range(0, prefabsIntermedios.Length)];

            Room room = Instantiate(prefab, PosicionDeCelda(camino[i]), Quaternion.identity, contenedorMapa);
            room.name = esInicio ? "Inicio" : esMeta ? "Meta" : "Habitacion_" + i;

            room.tamano = tamanoHabitacion;
            room.indice = i;
            room.esInicio = esInicio;
            room.esMeta = esMeta;
            room.dirSiguiente = esMeta ? Dir.Ninguna : DirUtil.Entre(camino[i], camino[i + 1]);
            room.dirAnterior = esInicio ? Dir.Ninguna : DirUtil.Entre(camino[i], camino[i - 1]);

            habitaciones.Add(room);
        }
    }

    void LimpiarMapa()
    {
        habitaciones.Clear();
        for (int i = contenedorMapa.childCount - 1; i >= 0; i--)
            Destroy(contenedorMapa.GetChild(i).gameObject);
    }

    Vector3 PosicionDeCelda(Vector2Int c)
    {
        return contenedorMapa.position + new Vector3(c.x * tamanoHabitacion.x, 0f, c.y * tamanoHabitacion.y);
    }

    Vector2Int CeldaDe(Vector3 pos)
    {
        Vector3 local = pos - contenedorMapa.position;
        return new Vector2Int(
            Mathf.RoundToInt(local.x / tamanoHabitacion.x),
            Mathf.RoundToInt(local.z / tamanoHabitacion.y));
    }

    // ------------------------------------------------------------------- juego

    // Lo llama cada RoomDoor cuando el jugador cruza una puerta.
    public void PuertaCruzada(Room room, Dir puerta)
    {
        if (terminado || room == null) return;

        if (puerta == room.dirSiguiente) return;                          // puerta correcta
        if (puerta == room.dirAnterior && !volverAtrasEsError) return;    // volver a la habitación anterior

        // Puerta incorrecta: al inicio, entrando por el lado opuesto.
        Dir ladoDeEntrada = DirUtil.Opuesta(puerta);
        Teletransportar(habitaciones[0].PuntoCercaDePuerta(ladoDeEntrada, margenAparicion));
    }

    void Update()
    {
        if (terminado || habitaciones.Count == 0 || jugador == null) return;

        if (CeldaDe(jugador.position) == camino[camino.Count - 1])
        {
            terminado = true;
            Debug.Log("¡Llegaste a la meta!");
            alGanar.Invoke();
        }
    }

    void Teletransportar(Vector3 destino)
    {
        destino.y = jugador.position.y;

        Rigidbody rb = jugador.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.position = destino;
        }
        jugador.position = destino;
        Physics.SyncTransforms();
    }

    bool DatosValidos()
    {
        if (jugador == null) { Debug.LogError("MapManager: falta asignar el Jugador."); return false; }
        if (prefabInicio == null || prefabMeta == null) { Debug.LogError("MapManager: faltan los prefabs de Inicio o Meta."); return false; }
        if (prefabsIntermedios == null || prefabsIntermedios.Length == 0) { Debug.LogError("MapManager: falta al menos un prefab intermedio."); return false; }
        return true;
    }

    // Dibuja la solución en la vista Scene mientras juegas (solo tú la ves).
    void OnDrawGizmos()
    {
        if (camino == null || camino.Count == 0) return;
        if (contenedorMapa == null) contenedorMapa = transform;

        Gizmos.color = Color.green;
        for (int i = 0; i < camino.Count; i++)
        {
            Vector3 p = PosicionDeCelda(camino[i]) + Vector3.up * 0.5f;
            Gizmos.DrawSphere(p, i == camino.Count - 1 ? 1f : 0.5f);
            if (i > 0) Gizmos.DrawLine(PosicionDeCelda(camino[i - 1]) + Vector3.up * 0.5f, p);
        }
    }
}
