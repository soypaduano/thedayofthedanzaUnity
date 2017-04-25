
using UnityEngine;


public class EstadoFinJugador : InstanciaEstadoBase<EstadoFinJugador>
{
    //Vamos a poner un contador, un temporizador que le dice al usuario cuando tiempo debe hacer para ejecutar un movimiento.


    //Sera un estado temporizado, por eso necesitamos un timer
    float m_reloj = 0;
    float m_duracion = 1.0f;

    //Le asignamos un target a ver a quien tiene que apuntar
    Vector3 m_currentTarget;



    public override void Start(MainGame game)
    {
        m_reloj = 0;
    }

    public override void Update(MainGame game)
    {
        m_reloj += Time.deltaTime;

        //Si el reloj ha llegado a la duracion....
        if (m_reloj > m_duracion)
        {
            EstadoBase.CambiarEstado(EstadoIrProfesor.Instancia);

        }
    }

    public override void End(MainGame game)
    {
        game.RestartStep();
    }

}
