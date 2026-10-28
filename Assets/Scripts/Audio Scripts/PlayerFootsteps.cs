using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip[] footstepsSFX; // Array de sonidos
    public float stepsFrequency  =  0.5f; // Determina la velocidad de los pasos
    private PlayerController playerMovement; // Hace referencia al movimiento dentro del controlador
    void Start()
    {
        playerMovement = GetComponent<PlayerController>();
        StartCoroutine(Footsteps());
    }

    // Para que funcione tenemos que actualizar el movimiento de private a public
    IEnumerator Footsteps()
    {
        while (true)
        {
            if (playerMovement.movimiento.magnitude > 0.1f)
            {
                int randomSound = Random.Range(0, 3);
                AudioManager.instance.PlaySFX(footstepsSFX[randomSound], 0.5f, 0.95f, 1.02f);
                yield return new WaitForSeconds(stepsFrequency);
            }
            else
            {
                yield return null;
            }
        }
    }
}
