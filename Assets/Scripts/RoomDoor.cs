using UnityEngine;

// Va en cada Puerta_N / Puerta_S / Puerta_E / Puerta_O del prefab de habitación.
// El objeto necesita un Collider con "Is Trigger" activado.
public class RoomDoor : MonoBehaviour
{
    public Dir direccion;

    Room room;

    void Awake()
    {
        room = GetComponentInParent<Room>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerController>() == null) return;
        if (MapManager.Instance == null) return;

        MapManager.Instance.PuertaCruzada(room, direccion);
    }
}
