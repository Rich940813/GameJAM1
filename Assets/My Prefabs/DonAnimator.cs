using UnityEngine;

public class DonAnimacion : MonoBehaviour
{
    Animator anim;
    PlayerController jugador;

    void Awake()
    {
        anim = GetComponent<Animator>();
        jugador = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        anim.SetFloat("Speed", jugador.movimiento.magnitude);
    }
}