using UnityEngine;

// Convención del proyecto: Norte = +Z, Este = +X.
public enum Dir { Norte, Sur, Este, Oeste, Ninguna }

public static class DirUtil
{
    public static readonly Dir[] Cuatro = { Dir.Norte, Dir.Sur, Dir.Este, Dir.Oeste };

    public static Dir Opuesta(Dir d)
    {
        switch (d)
        {
            case Dir.Norte: return Dir.Sur;
            case Dir.Sur:   return Dir.Norte;
            case Dir.Este:  return Dir.Oeste;
            case Dir.Oeste: return Dir.Este;
            default:        return Dir.Ninguna;
        }
    }

    // Dirección en la malla: x = columna, y = fila (Norte = fila + 1 = +Z)
    public static Vector2Int ACelda(Dir d)
    {
        switch (d)
        {
            case Dir.Norte: return new Vector2Int(0, 1);
            case Dir.Sur:   return new Vector2Int(0, -1);
            case Dir.Este:  return new Vector2Int(1, 0);
            case Dir.Oeste: return new Vector2Int(-1, 0);
            default:        return Vector2Int.zero;
        }
    }

    public static Vector3 AMundo(Dir d)
    {
        Vector2Int c = ACelda(d);
        return new Vector3(c.x, 0f, c.y);
    }

    public static Dir Entre(Vector2Int desde, Vector2Int hasta)
    {
        Vector2Int diff = hasta - desde;
        foreach (Dir d in Cuatro)
            if (ACelda(d) == diff) return d;
        return Dir.Ninguna;
    }
}

// Va en la raíz del prefab de cada habitación (Room_Base y sus variantes).
// El MapManager rellena los datos del camino al construir el mapa.
public class Room : MonoBehaviour
{
    public Vector2 tamano = new Vector2(16f, 10f); // X = ancho, Z = alto

    [HideInInspector] public int indice;                        // 0 = inicio
    [HideInInspector] public bool esInicio;
    [HideInInspector] public bool esMeta;
    [HideInInspector] public Dir dirSiguiente = Dir.Ninguna;    // puerta correcta
    [HideInInspector] public Dir dirAnterior = Dir.Ninguna;     // puerta por la que se vuelve

    // Punto dentro de la habitación, pegado a la puerta del lado indicado.
    public Vector3 PuntoCercaDePuerta(Dir lado, float margen)
    {
        Vector3 d = DirUtil.AMundo(lado);
        Vector3 offset = new Vector3(
            d.x * (tamano.x * 0.5f - margen),
            0f,
            d.z * (tamano.y * 0.5f - margen));
        return transform.position + offset;
    }
}
