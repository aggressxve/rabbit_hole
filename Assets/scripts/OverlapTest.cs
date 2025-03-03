using UnityEngine;

public class OverlapTest : MonoBehaviour
{
    //aqui va el tamaño del rectangulo
    public Vector2 rectangulo;

    private Color color = Color.white;


    void Update()
    {
        //esto marca el centro del rectangulo
        Vector2 centro = transform.position;
        float angulo = 0;

        //con esto se el filtro de la capa donde se va a detectar el monito
        LayerMask capa = LayerMask.GetMask("Default");

        //pone un rectangulo imaginario y nos regresa los colliders que esten dentro de el
        Collider2D[] arreglo = Physics2D.OverlapBoxAll(centro, rectangulo, angulo, capa);

        //obtenemos el tamaño del arreglo
        int colliderDentro = arreglo.Length;

        //si hay colliders dentro del rectangulo va a cambiar de color a morado y cuando no a blanco
        color = colliderDentro > 0 ? Color.magenta : Color.white;
    }

    
    private void OnDrawGizmos() //con este metodo se dibujan gizmos en unity
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position,rectangulo);
    }
}
