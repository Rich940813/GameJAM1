using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private Vector3 corrdenadasIniciales;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        corrdenadasIniciales = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -1)
        {
            SceneManager.LoadScene("Main");
            Debug.Log("cargada la escena");
        }
    }
}
