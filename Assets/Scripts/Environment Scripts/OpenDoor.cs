using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public int openGate = 0;
    public ControladorPuerta CorrectDoor;
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Correct"))
        {
            Debug.Log("activado");
            CorrectDoor.OpenDoor();
        }
    }
}
