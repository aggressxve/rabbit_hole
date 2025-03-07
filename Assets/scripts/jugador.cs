using Unity.Cinemachine;
using UnityEngine;

public class Jugador : Vivo   //aqui se heredan los atributos de la clase vivo
{
    [SerializeField] private CinemachineCamera camara;
    //se ejecuta cada frame, si tenemos 60 frames se ejecuta 60 veces
    //si tenemos 30 se ejecuta 30 veces
    private void Update()
    {
        Update_Input();
    }

    #region Input
    private void Update_Input()
    {
        //con esto se registra el axis del W/A/S/D /flechas/ controles
        axis.x = Input.GetAxisRaw("Horizontal");

        //si se preciona la tecla de espacio entonces salta
        if (Input.GetKeyDown(KeyCode.Space))
            Saltar();
    }

    #endregion Input

    #region Vida

    public override void Morir()
    {
        //ejecuta el metodo de la clase madre
        base.Morir();

        camara.Follow = null;
    }

    #endregion Vida
}
