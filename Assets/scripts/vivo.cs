using UnityEngine;

public class Vivo : MonoBehaviour
{
    #region core

    //Awake solo se ejecuta una vez cuando inicia la escena
    private void Awake()
    {
       Awake_componentes();
    }

    //Start se ejecuta una sola vez solo despues de ejecutar el Awake
    private void Start()
    {

    }

    //Update se ejecuta = a la cantidad de fps
    private void FixedUpdate()
    {
        //se pone aqui para revisar una vez si esta tocando el piso
        SensorPiso();
        FixedUpdate_Movimiento();
    }
    #endregion core

    #region componentes

    protected Rigidbody2D rb;

    private void Awake_componentes()
    {
        //Aqui tenemos la referencia del rigidbody2d
        rb = GetComponent<Rigidbody2D>();
    }

    #endregion componentes

    #region movimiento

    //serialized hace que el atributo se vea en el inspector
    //nunca usar el public para mostrar atributos

    [SerializeField] private float velocidad = 5;
    [SerializeField] private float fuerzaSalto = 15;

    public Vector3 axis = Vector3.zero; 
    public Vector3 movimiento = Vector3.zero;

    private void FixedUpdate_Movimiento()
    {
        //calcula el movimiento x
        movimiento.x = axis.x * velocidad;

        //calcula el movimiento igual a los frame
        movimiento *= Time.fixedDeltaTime;

        //con esto se aplica el movimiento al objeto
        transform.position += movimiento;
    }

    protected void Saltar()
    {

        if (!tocandoPiso) return;
        //aqui se crea la fuerza en el vector 2 que tendra el salto
        Vector2 fuerza = new Vector2(0, fuerzaSalto);

        //aqui se aplica la fuerza de impulso al rigidbody
        rb.AddForce(fuerza, ForceMode2D.Impulse);
    }

    #endregion movimiento

    #region sensores

    private bool tocandoPiso = false;
    private Vector2 tamañoSensorPiso = new Vector2(x:0.25f, y:0.05f);

    private void SensorPiso()
    {
        Vector2 centro = transform.position;

        float angulo = 0;

        LayerMask capa = LayerMask.GetMask("Default");

        Collider2D[] arreglo = Physics2D.OverlapBoxAll(point: centro, size: tamañoSensorPiso, angulo, (int)capa);

        int collidersDentro = arreglo.Length;

        tocandoPiso = collidersDentro > 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
        
    {
        //other es el collider donde entro

        //si el tag del otro objeto es muerte entonces
        if (other.CompareTag("Muerte"))
        {
            Morir();
        }

    }
    #region Vida

    private bool muerto = false;

    public virtual void Morir()
    {
        muerto = true;
    }

    #endregion Vida

    #endregion sensores


}
