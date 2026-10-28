using System.Collections;
using UnityEngine;

public class ControladorPuerta : MonoBehaviour
{
    public int activation = 0;
    public int requiredActivation;
    public float speed = 10;
    private GameObject puertas;
    private bool isOpening;
    //public GameObject player; Objeto para realizar la colison 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoor();
        /*if (controladorPuerta)
        {
            OpenDoor();
            CloseDoor();
        }*/
    }

    public void OpenDoor()
    {
        if (activation == requiredActivation)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            StartCoroutine(MoveDoor());
        }
    }

    IEnumerator MoveDoor()
    {
        yield return new WaitForSeconds(2.0f);
        activation = 3;
    }
    /*
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
    */

    public void DetectActivation()
    {
        activation++;
    }
}
