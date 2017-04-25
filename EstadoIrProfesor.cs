
using UnityEngine;


public class EstadoIrProfesor : InstanciaEstadoBase<EstadoIrProfesor>  {

    //Sera un estado temporizado, por eso necesitamos un timer

    float m_reloj = 0;
    float m_duracion = 2;

    //Le asignamos un target a ver a quien tiene que apuntar
    Vector3 m_currentTarget;



    public override void Start(MainGame game)
    {
       
        m_reloj = 0;
        //Calculamos la distancia entre la camara y el maestro desde un punto a otro
        float distance = (game.m_camaraPrincipal.transform.position - game.m_maestro.transform.position).magnitude;
        m_currentTarget = game.m_camaraPrincipal.transform.position + game.m_camaraPrincipal.transform.forward * distance;
    }

    public override void Update(MainGame game)
    {
        m_reloj += Time.deltaTime;

        //Si el reloj ha llegado a la duracion....
        if(m_reloj > m_duracion)
        {

            EstadoBase.CambiarEstado(EstadoProfesor.Instancia);

        } else
        {
            m_currentTarget = Vector3.Lerp(m_currentTarget, game.m_maestro.transform.position, m_reloj / m_duracion);
            game.m_camaraPrincipal.transform.LookAt(m_currentTarget + Vector3.up);
        }
    }

}
