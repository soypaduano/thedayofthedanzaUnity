
using UnityEngine;


public class EstadoProfesor : InstanciaEstadoBase<EstadoProfesor>
{

    //Sera un estado temporizado, por eso necesitamos un timer

    float m_reloj = 0;
    float m_duracion = 0.75f;

    //Le asignamos un target a ver a quien tiene que apuntar
    Vector3 m_currentTarget;



    public override void Start(MainGame game)
    {
        m_reloj = 0;
        //Vamos a decirle al juego que nos de la referencia de la cmara principal
        game.m_camaraPrincipal.transform.LookAt(game.m_maestro.transform.position + Vector3.up);
        game.PointFirstStep();
    }

    public override void Update(MainGame game)
    {
        m_reloj += Time.deltaTime;

        if (m_reloj > m_duracion)
        {
            m_reloj = 0;

            if (!game.ShowMeNextStep(game.m_animadorMaestro))
            {
                EstadoBase.CambiarEstado(EstadoIrJugador.Instancia);
                //Cuando el profesor termina de hacer la animacion debemos quitarle el valor para que deje de ejecutar esa animacion
            }

          //  EstadoBase.CambiarEstado(EstadoProfesor.Instancia);
        }
        else
        {

        }
    }


    public override void End(MainGame game)
    {
        game.RestartStep();
    }

}
