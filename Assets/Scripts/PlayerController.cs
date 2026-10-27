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
        rigidbody = GetComponent<Rigidbody>();   
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movimiento = inputActions.Player.Move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        MuevePlayer();
    }

    public void MuevePlayer()
    {
        Vector3 movimientoTotal = new Vector3(movimiento.x,0f,movimiento.y);
        rigidbody.MovePosition(transform.position + movimientoTotal.normalized*speed*Time.fixedDeltaTime);
    }
}
