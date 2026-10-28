
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavegacion : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("NivelPrincipal");
    }

    public void VerCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuInicio");
    }
}

