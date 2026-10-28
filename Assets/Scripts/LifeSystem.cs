
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public int vidasIniciales = 3;
    public Image[] corazones;
    public GameObject panelGameOver;

    private int vidasActuales;

    void Start()
    {
        vidasActuales = Mathf.Min(vidasIniciales, corazones.Length);
    }

    public void PerderVida()
{
    if (vidasActuales <= 0)
        return;

    vidasActuales--;

    if (vidasActuales < corazones.Length &&
        corazones[vidasActuales] != null)
    {
        corazones[vidasActuales].gameObject.SetActive(false);
    }

    if (vidasActuales == 0)
    {
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }
    }
}
}
