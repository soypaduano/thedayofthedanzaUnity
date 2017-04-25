
using UnityEngine;


public class EstadoJugador : InstanciaEstadoBase<EstadoJugador>
{
    //Vamos a poner un contador, un temporizador que le dice al usuario cuando tiempo debe hacer para ejecutar un movimiento.


    //Sera un estado temporizado, por eso necesitamos un timer
    float m_reloj = 0;
    float m_duracion = 2.0f;

    //Le asignamos un target a ver a quien tiene que apuntar
    Vector3 m_currentTarget;



    public override void Start(MainGame game)
    {
        m_reloj = 0;
        game.PointFirstStep();
        game.muestraEmpezar();
        //game.m_camaraPrincipal.transform.LookAt(game.m_jugador.transform.position + Vector3.up);
    }

    public override void Update(MainGame game)
    {
        m_reloj += Time.deltaTime;
        //Si el reloj ha llegado a la duracion....
        if (m_reloj < m_duracion)
        {
            MainGame.StepTipe step = ControlTeclado();
            if (step != MainGame.StepTipe.None) //Comprobamos que el paso sea diferente a NONE.
            {
                m_reloj = 0;

                // le preguntamos al juego si el paso introducido por el usuario es el esperado.
                if (game.CompruebaPaso(game.m_animadorJugador, step)) //En caso de que si....
                {
                    game.addPoints(100); //Sumamos 100 puntos al usuario

                    if (game.HaTerminado()) //Y vemos si el juego esta terminado... Si es false...
                    {
                        game.TocarSonidos(MainGame.Efectos.Bien1, MainGame.Efectos.Bien2, MainGame.Efectos.Bien3);
                        game.AddNewStep(); //Añadimos un nuevo paso que será introducido por el usuario para que lo repita la maquina 
                        EstadoBase.CambiarEstado(EstadoFinJugador.Instancia);
                    }
                    return;
                } else
                    game.MalPaso();
            }
            else
                return;
            //Puedo marcar mi movimiento
        } else
        {
            game.MalPaso();
        }
    }
      


    MainGame.StepTipe ControlTeclado()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) return MainGame.StepTipe.Down;
        if (Input.GetKeyDown(KeyCode.UpArrow)) return MainGame.StepTipe.Up;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return MainGame.StepTipe.Left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return MainGame.StepTipe.Right;

        return ControlGesto();

    }

    Vector2 m_inicio; //Inicio del toque

    MainGame.StepTipe ControlGesto()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            // Analizamos

                if(touch.phase == TouchPhase.Began)
                {
                m_inicio = touch.position;
             } else if(touch.phase == TouchPhase.Ended)
                {
                    Vector2 delta = touch.position - m_inicio;
                    if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                        {
                    if (delta.x < 0)
                        return MainGame.StepTipe.Left;
                    else return MainGame.StepTipe.Right;
                        } else {
                    if (delta.y< 0) return MainGame.StepTipe.Down;
                    else return MainGame.StepTipe.Up;
                }
            }
        }

        return MainGame.StepTipe.None;
    }

}
