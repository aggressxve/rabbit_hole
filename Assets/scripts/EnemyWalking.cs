using UnityEngine;

public class EnemyWalking : MonoBehaviour
{
    // Create a reference to Rigidbody2D to move the enemy around
    Rigidbody2D myRigidBody;
    [SerializeField] float moveSpeed = 1f;
    void Start()
    {
        // Obtener el rigidbody para manipularlo
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Definirle la velocidad al rigidbody usando el atributo linearvelocity y un vector2D
        myRigidBody.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision) {
        /* Al dejar de recibir el trigger del piso, 
         * el movespeed cambia de signo e invoca el método FlipEnemyFacing
        */
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing() {
        // Cambia la dirección obteniendo el signo actual de linearVelocity e invirtiendo el local scale multiplicándolo por el signo contrario
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.linearVelocity.x)), 1f);
    }

}
