using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] float velocidadMovimiento = 1f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(velocidadMovimiento, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Suelo")
        {
            velocidadMovimiento = -velocidadMovimiento;
            voltearDireccionEnemigo();
        }
    }

    void voltearDireccionEnemigo()
    {
        transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), 1f);
    }
}
