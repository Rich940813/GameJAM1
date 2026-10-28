using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    private Vector3 posicionInicio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        posicionInicio = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < 0.5f)
        {
            SceneManager.LoadScene("main");
        }
    }
}
