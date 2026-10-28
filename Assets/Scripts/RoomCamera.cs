using UnityEngine;

// Cámara fija por habitación: se queda quieta mientras el jugador está dentro de una
// habitación y se desliza a la siguiente cuando la cruza.
// Va en Camara2. Quita CamaraRotation de ese mismo objeto.
public class RoomCamera : MonoBehaviour
{
    public Transform player;
    public Vector2 roomSize = new Vector2(16f, 10f);       // igual que Tamano Habitacion del MapManager
    public Vector3 offset = new Vector3(0.5f, 10f, -4f);   // posición de la cámara respecto al centro de la habitación
    public float speed = 8f;

    Vector3 Objetivo()
    {
        float x = Mathf.Round(player.position.x / roomSize.x) * roomSize.x;
        float z = Mathf.Round(player.position.z / roomSize.y) * roomSize.y;
        return new Vector3(x, 0f, z) + offset;
    }

    void Start()
    {
        Snap();
    }

    // Salta de golpe a la habitación del jugador (el MapManager lo llama al volver al inicio).
    public void Snap()
    {
        if (player == null) return;
        transform.position = Objetivo();
    }

    void LateUpdate()
    {
        if (player == null) return;
        transform.position = Vector3.Lerp(transform.position, Objetivo(), speed * Time.deltaTime);
    }
}
