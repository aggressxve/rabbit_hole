using System.Collections;
using System.Runtime.InteropServices;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class JugadorScript : MonoBehaviour
{
    #region Variables
    [Header("Variables Jugador")]
    [Space(10)]
    [Tooltip("indica si el jugador fue golpeado, y ha muerto")]
    [SerializeField] private bool muerto;
    [Space(2)]
    [Range(0,20)]

    [Tooltip("Velocidad del jugador. Valor de 0-20")]
    [SerializeField] private float velocidad;
    [Space(2)]
    [Range(0,50)]
    [Tooltip("Fuerza del salto del jugador. Valor de 0-50")]
    [SerializeField] private float fuerzaSalto;
    [Space(10)]
    [Header("Colisionadores, Transforms y demas.")]
    [Space(10)]
    [Tooltip("Colisionador de pile")]
    [SerializeField] private CapsuleCollider2D pieCollider;
    [Space(2)]
    [Tooltip("Colisionador agachado")]
    [SerializeField] private CapsuleCollider2D agachadoCollider;
    [Space(2)]
    [Tooltip("Transform que verifica si el jugador esta tocando el suelo")]
    [SerializeField] private Transform checkSuelo;
    [Space(2)]
    [Tooltip("Layer que indica que objectos son parte del suelo")]
    [SerializeField] private LayerMask layerSuelo;


    //Variables no visibles
    private float horizontal; // variable para sistema de movimiento de unity

    private bool agachado; // booleano que indica si el jugador esta agachado
    private bool volteado; // variable para controlar el voltear el sprite del jugador
    private Rigidbody2D rb; //RigidBody del Jugador

    private Animator anim; //Animador del jugador

    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Agarramos el RigidBody2D de este objeto
        anim = GetComponentInChildren<Animator>(); //El animator esta en un child, asi que lo buscamos en los childs de este objeto y asignamos
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // tomamos el eje horizontal del sistema de inputs de unity, 
        //y se lo asignamos a un float, esto controlara el movimiento

        if(Input.GetButtonDown("Jump") && TocandoSuelo()) //Si estamos en el suelo, y presionamos el salto de inputs de unity, saltamos
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, fuerzaSalto); // Hacemos que nuestra velocidad en Y sea igual a nuestra fuerza de salto
        }
        Girar(); //Funcion que se encarga de girar al jugador al lado a donde esta avanzando

        Agacharse(); //Funcion que se encarga de agachar al jugador si presionamos el boton de agacharse.

        ControlesAnimaciones(); //Funcion que se encarga de controlar las animaciones del jugador
    }

    void FixedUpdate()
    {
          rb.linearVelocity = new Vector2(horizontal * velocidad, rb.linearVelocityY); //Usamos un fixedupdate para asignar la velocidad horizontal del jugador
          // manteniendo su velocidad en Y en todo momento, por si esta saltando, solo mutliplicamos el input de horizontal por la velocidad del jugador 
    }

    private bool TocandoSuelo()
    {
        return(Physics2D.OverlapCircle(checkSuelo.position, 0.2f, layerSuelo)); //Funcion que verifica si el jugador esta tocando el suelo, regresando true si es el caso.
    }

    private void Girar() //Funcion que se encarga de girar al jugador al lado a donde esta avanzando
    {
        if(volteado && horizontal <0f || !volteado && horizontal >0f) 
        {
            volteado = !volteado;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Agacharse() //activa un collider de agachado, y desactiva el collider de pie.
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            agachado = true;
            pieCollider.enabled = false;
            agachadoCollider.enabled = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            agachado = false;
            pieCollider.enabled = true;
            agachadoCollider.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void ControlesAnimaciones()
    {
        if(rb.linearVelocityX != 0f) //Si la velocidad en X es distinta a 0, asignamos que el jugador esta corriendo
        {
            anim.SetBool("Corriendo", true); //Asignamos al parametro de corriendo del animator, que el jugador esta corriendo
        }
        else
        {
            anim.SetBool("Corriendo", false); //Si no, asignamos que el jugador no esta corriendo
        }
        if(rb.linearVelocityY > 0f)
        {
            anim.SetBool("Saltando", true); //Si la velocidad en Y del jugador es mayor a 0, asignamos que el jugador esta saltando
        }
        else
        {
            anim.SetBool("Saltando", false); //Si no, asignamos que el jugador no esta saltando
        }
        if(rb.linearVelocityY < 0f)
        {
            anim.SetBool("Cayendo", true); //Si la velocidad en Y del jugador es menor a 0, asignamos que el jugador esta cayendo
        }
        else

        {
            anim.SetBool("Cayendo", false); //Si no, asignamos que el jugador no esta cayendo
        }

        anim.SetBool("Agachado", agachado); //Asignamos si el jugador esta agachado al parametro de agachado del animator
        if(muerto == true)
        {
            StartCoroutine(MuerteJugador(.3f));

        }
 
    }


    IEnumerator MuerteJugador(float delayTime)
    {
        anim.SetBool("Hit", true); //Si el jugador muere al recibir daño, activamos el trigger de Hit en el animator

        velocidad = 0f;

        yield return new WaitForSeconds(delayTime);

        velocidad = 2.5f;

        transform.position = new Vector3(3.055f, 0.36f, 0f);


        muerto = false;

        anim.SetBool("Hit", false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemigo")
        {
           muerto = true;
        }
    }


}
