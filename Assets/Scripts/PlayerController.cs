using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public GameObject mundo; // Referencia al mundo para obtener sus dimensiones por su collider
    private Rigidbody rigidbody; // Variable para orbtener RigidBody
    private InputSystem_Actions inputActions; //nuevo sistema de inputs
    public Vector2 movimiento; //detector movimiento horizontal

    private Collider collider;

    public float speed;

    private float minX;
    private float maxX;
  
    private float minZ;
    private float maxZ;

    private float myScala; // Para cambiar la orientacion de jugador
    public Animator animator; // Referencia al componente Animator de Don

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();   //Obteniendo el RigidBody
        inputActions = new InputSystem_Actions(); //Iniciando el inputsystem
        //collider = mundo.GetComponent<Collider>(); //Obtenemos el collider del mundo
        //Obtenemos las dimensiones del mundo
        /*minX = collider.bounds.min.x;
        maxX = collider.bounds.max.x;
        minZ = collider.bounds.min.z;
        maxZ = collider.bounds.max.z;*/
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void OnEnable()
    {
        inputActions.Player.Enable(); // Activando en on enable
    }
    void OnDisable()
    {
        inputActions.Player.Disable(); // Desactivando en on disable
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movimiento = inputActions.Player.Move.ReadValue<Vector2>(); //Leyendo los valores del teclado de direccion
        //TotalReposition();// Reposiciona en el límite de cada borde
    }

    void FixedUpdate()
    {
        MuevePlayer(); //llamando a la funcion para que se mueva el player
    }

    public void MuevePlayer() //Mueve al jugador por el mundo
    {
        if(movimiento.x !=0)
        {
            float anguloObjetivo = 90*movimiento.x;
            rigidbody.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(0f, anguloObjetivo, 0f),100);
        }
        if(movimiento.y != 0)
        {
             float anguloObjetivo = 0;
            if (movimiento.y == -1)
            {
                anguloObjetivo = 180;
            }
           
            //Debug.Log (anguloObjetivo);
            rigidbody.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(0f, anguloObjetivo, 0f),100);
            
        }
        Vector3 movimientoTotal = new Vector3(movimiento.x,0f,movimiento.y);// Creando Vector 3 para sumarlo al vector de posicion y mover el player
        rigidbody.MovePosition(transform.position + movimientoTotal.normalized*speed*Time.fixedDeltaTime);
        // ===== ENVÍO DE DATOS AL ANIMATOR =====
        // Si hay movimiento, la magnitud será 1 (o mayor si se presiona a tope), si no, será 0.
        float velocidadActual = movimientoTotal.magnitude > 0 ? 1f : 0f;

        if (animator != null)
        {
            velocidadActual = movimiento.magnitude > 0.01f ? movimiento.magnitude : 0f;
            animator.SetFloat("Speed_f", velocidadActual);
        }
    }
/*
    private void RepositioX(float x)//REposiciona en x
    {
        transform.position = new Vector3(x,transform.position.y,transform.position.z);
    }

    private void RepositioZ(float z)//REposiciona en Z
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,z);
    }

    private void TotalReposition()//Función condicional para reposicionar
    {
         if( transform.position.x<minX)
        {
            RepositioX(minX);
        }  
        if (transform.position.x > maxX)
        {
             RepositioX(maxX);
        }
         if( transform.position.z<minZ)
        {
            RepositioZ(minZ);
        }  
        if (transform.position.z > maxZ)
        {
             RepositioZ(maxZ);
        }
    }*/
}
