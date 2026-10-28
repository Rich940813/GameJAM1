using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public ControladorPuerta correctDoor;
    public ControladorPuerta wrongDoor;
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Correct"))
        {
            Debug.Log("Correcto");
            AudioManager.instance.PlayJingle(1);
            correctDoor.DetectActivation();
        }
        if (other.CompareTag("Wrong"))
        {
            Debug.Log("Incorrecto");
            wrongDoor.WrongActivation();
        }
    }

}
