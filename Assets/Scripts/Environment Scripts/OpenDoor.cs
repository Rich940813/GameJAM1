using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public ControladorPuerta correctDoor;
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Correct"))
        {
            Debug.Log("activado");
            correctDoor.DetectActivation();
        }
    }
}
