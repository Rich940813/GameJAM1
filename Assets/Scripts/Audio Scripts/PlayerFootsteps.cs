using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip[] footstepsSFX;
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
                int randomSound = Random.Range(0, 3);
                AudioManager.instance.PlaySFX(footstepsSFX[randomSound], 1, 0.95f, 1.02f);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return null;
            }
        }
    }
}
