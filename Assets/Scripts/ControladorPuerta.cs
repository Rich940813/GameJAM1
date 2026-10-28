using System.Collections;
using UnityEngine;

public class ControladorPuerta : MonoBehaviour
{
    private bool controladorPuerta;
    public float speed;
    private GameObject puertas;
    //public GameObject player; Objeto para realizar la colison 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controladorPuerta = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controladorPuerta)
        {
            OpenDoor();
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        transform.Translate(Vector3.right*speed*Time.deltaTime);
    }

    public void CloseDoor()
    {
        StartCoroutine(CerrarPuerta());
    }

    IEnumerator CerrarPuerta()
    {
        yield return new WaitForSeconds(3f);
        controladorPuerta = false;
    }
 
    void OnCollisionEnter(Collision collision)
    {
        controladorPuerta = true;
    }
}
