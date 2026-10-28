
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavegacion : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Main");
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

