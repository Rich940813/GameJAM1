using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footstepsSFX;
    private PlayerController playerMovement;
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
                AudioManager.instance.PlaySFX(footstepsSFX);
            }
        }
    }
}
