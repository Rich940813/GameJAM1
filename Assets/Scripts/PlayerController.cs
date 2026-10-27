using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mundo; // Referencia al mundo para obtener sus dimensiones por su collider
    private Rigidbody rigidbody; // Variable para orbtener RigidBody
    private InputSystem_Actions inputActions; //nuevo sistema de inputs
    private Vector2 movimiento; //detector movimiento horizontal

    public float speed;
  
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();   //Obteniendo el RigidBody
        inputActions = new InputSystem_Actions(); //Iniciando el inputsystem
    }

    void OnEnable()
    {
        inputActions.Player.Enable(); // Activando en on enable
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movimiento = inputActions.Player.Move.ReadValue<Vector2>(); //Leyendo los valores del teclado de direccion
    }

    void FixedUpdate()
    {
        MuevePlayer(); //llamando a la funcion para que se mueva el player
    }

    public void MuevePlayer()
    {
        Vector3 movimientoTotal = new Vector3(movimiento.x,0f,movimiento.y);// Creando Vector 3 para sumarlo al vector de posicion y mover el player
        rigidbody.MovePosition(transform.position + movimientoTotal.normalized*speed*Time.fixedDeltaTime);
    }
}
