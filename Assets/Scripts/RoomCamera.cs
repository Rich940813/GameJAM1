using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public Transform player;
    public Vector2 roomSize = new Vector2(16f, 10f); // ancho (X) y alto (Z)
    public float speed = 8f;

    Vector3 offset;

    Vector3 RoomCenter(Vector3 pos)
    {
        float x = Mathf.Round(pos.x / roomSize.x) * roomSize.x;
        float z = Mathf.Round(pos.z / roomSize.y) * roomSize.y;
        return new Vector3(x, 0f, z);
    }

    void Start()
    {
        // Guarda cómo está colocada la cámara respecto a la sala inicial
        offset = transform.position - RoomCenter(player.position);
    }

    void LateUpdate()
    {
        Vector3 target = RoomCenter(player.position) + offset;
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }
}