
using UnityEngine;
using UnityEngine.SceneManagement;


public class EstadoFinDeJuego : InstanciaEstadoBase<EstadoFinDeJuego>
{
    //Vamos a poner un contador, un temporizador que le dice al usuario cuando tiempo debe hacer para ejecutar un movimiento.


    //Sera un estado temporizado, por eso necesitamos un timer
    float m_reloj = 0;
    float m_duracion = 5.0f;




    public override void Start(MainGame game)
    {
        m_reloj = 0;
        game.GameOver();
    }

    public override void Update(MainGame game)
    {
        m_reloj += Time.deltaTime;

        //Si el reloj ha llegado a la duracion....
        if (m_reloj > m_duracion)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }



}