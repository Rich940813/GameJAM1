using System.Collections;
using UnityEngine;

public class ControladorPuerta : MonoBehaviour
{
    public int activation = 0;
    public int requiredActivation;
    private int stopDoor = 0;
    public float speed = 1;
    private GameObject puertas;
    private bool isOpening = false;
    private bool wrongActivation = false;
    public bool doorYesOrNo = true;
    private bool doorChosen;
    //public GameObject player; Objeto para realizar la colison 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (!doorChosen)
        {
            if (activation == requiredActivation && doorYesOrNo && wrongActivation == false)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (!isOpening)
            {
                isOpening = true;
                StartCoroutine(MoveDoor());
            }
        }
        else if (wrongActivation == true && !doorYesOrNo && stopDoor < 1)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (!isOpening)
            {
                isOpening = true;
                StartCoroutine(MoveDoor());
            }
        }
        

        }
    }

    IEnumerator MoveDoor()
    {
        yield return new WaitForSeconds(2.0f);
        activation = 3;
        stopDoor++;
        // 0 = wrong, 1 = small success, 2 = big success
        if (wrongActivation == true)
        {
            AudioManager.instance.PlayJingle(0);
        }
        else
        {
            AudioManager.instance.PlayJingle(2);
        }
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

    public void WrongActivation()
    {
        wrongActivation = true;
    }
}
