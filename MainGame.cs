


/*
    C O D E 
    M A D E 
    B Y 
    S E B A S T I A N 
    P A D U A N O 
    2 0 1 7
    T H E D A Y O F T H E D A N Z A

    C R E D I T S T O :
    P L A T Z I 
    V I D E O G A M E S A R M Y
    ^_^
*/





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainGame : MonoBehaviour {

    //We connect the gameObject on the Canvas to this MainGame Script, in order to control them
    //R E F E R E N C E S 
    public Text score;
    public Text currentScore;
    public Text MaxScore;
    public Image comeOn; //CARTTEL DE LETS DANZA
    public GameObject arrow;
    public GameObject wrong;
    public GameObject inGame;
    public GameObject gameOver;

    public AudioSource m_fuenteDeSonido;

    public GameObject m_camaraPrincipal;
    public GameObject m_maestro;
    public GameObject m_jugador;

    public Animator m_animadorMaestro;
    public Animator m_animadorJugador;

    //Game Variable
    int i_myPoints; //points made
    public GameObject[] lives; //array of lives 
    int i_lives; //counter of lives

    
    public enum StepTipe {None, Right, Up, Left, Down}; //Enum for all the posible steps (0,1,2,3,4)

    //Array of type Quaternion that defines our ARROW rotation
    static Quaternion[] StepBase = new Quaternion[] {
        Quaternion.Euler(0,0,0f),
        Quaternion.Euler(0,0,0f),
        Quaternion.Euler(0,0,90f),
        Quaternion.Euler(0,0,180f),
        Quaternion.Euler(0,0,270f),
         };


    //CURIOS: A LIST IS KIND OF A DINAMIC ARRAY :D


    public AudioClip[] EfectosPaso;


    public enum Efectos
    {
        Empezar1, Empezar2, Bien1, Bien2, Bien3, Mal1, Mal2, Ganar, Perder
    };


    public AudioClip[] EfectosDeSonido;

    public int highScore;



    //THIS IS A LIST OF THE STEPS DONE
    List<StepTipe> stepsDone = new List<StepTipe>();

    //LETS CREATE AN ENUMERATOR
    //An enumerator is what controls the steps. Is a way to point a list. We can ask it too for example the next steps.
    //Un enumerador es lo que controla porque paso vamos. Un enumerador es una forma de apuntar a una lista.. podemos pedirles los siguientes pasos
    List<StepTipe>.Enumerator CurrentStep;

    // Use this for initialization
    void Start () {

        
        MaxScore.enabled = false;
        currentScore.enabled = false;

        //Comparamos maximas puntuaciones
        compararMaximasPuntuaciones();
           
        

        //On start we restart the game and we add 3 new steps.
        RestartGame();
        // new steps
        AddNewStep();
        AddNewStep();
        AddNewStep();

        //Le decimos a la clase estado base que cambie al estado estado ir profesor
        EstadoBase.SetGame(this);
        EstadoBase.CambiarEstado(EstadoIrProfesor.Instancia);
    }

    public void compararMaximasPuntuaciones()
    {
        //Obtenemos la mayor puntuacion:
        highScore = PlayerPrefs.GetInt("highscore");

        if (highScore == null)
        {
            PlayerPrefs.SetInt("highscore", 0);
        }

        if (i_myPoints > highScore)
        {
            highScore = ((int)i_myPoints);
            PlayerPrefs.SetInt("highscore", highScore);
        }


    }
	
	// Update is called once per frame
	void Update () {
        EstadoBase.Actualizar();
        ActualizaEmpezar();
    }


    //RESTART GAME
    public void RestartGame() {
        //Ocultar el letrero de vamos
        comeOn.enabled = false;
        gameOver.SetActive(false);

        //3 lives for our player
        i_lives = 3;
        //0 points to our player
        i_myPoints = 0;
        // Update lives to our player
        updateLives();
        //Ponemos en 0 la puntuacion
        addPoints(0);
        //Limpiamos la lista que habiamos hecho
        stepsDone.Clear();
        //Que la equivocacion / flecha esten no visibles
        RestartStep();
    }


    //UPDATE LIVES
    public void updateLives()
    {

        //Establece un contador 
        //Un bucle for que sea desde 0 hasta el numero de vidas, y haciendo que los indicadores de vida que tenemos dentro de nuestro array, se activen. 
        int i = 0;

        for (i = 0; i < i_lives; i++)
        {
            lives[i].SetActive(true);
        }

        for (; i < 3; ++i)
        {
            lives[i].SetActive(false);
        }
    }


    //ADD POINTS
    //La ponemos publica porque la queremos llamar desde otra clase.
    public void addPoints(int recievedPoints)
    {
        i_myPoints += recievedPoints;
        score.text = i_myPoints.ToString("D6");
    }


    //RESTART STEP
    public void RestartStep()
    {
        wrong.SetActive(false);
        arrow.SetActive(false);
    }


    //ADD NEW STEP
    public void AddNewStep() {

        //Añadimos un nuevo paso: nuestra maquina elegira un numero del 0...4 que finalmente será un paso de la enumeracion StepTipe
        //we add a new step. Our machine will choose a number between 0 and 4, which finally will be a step from the enum StepTipe. 
        stepsDone.Add((StepTipe)Random.Range(1, 5));
    }


    //Apuntar al primer paso de la lista
   public void PointFirstStep()
    {
        //Get enumerator directamente nos da el primer paso! 
        CurrentStep = stepsDone.GetEnumerator();
    }

    //SHOW ME NEXT STEP

        /*
         * Le pasamos un controlador de animacion porque esta f me va a servir para mostrar el siguiente paso
         * tanto de profesor como personaje y mostrarlo....
         * 
         */
    public bool ShowMeNextStep(Animator animator) {

        //ENUMERATOR SEES IF THERE IS A NEXT STEP??????????? 
        if (CurrentStep.MoveNext())
            {
                //Si hay siguiente paso..
                //Activamos la flecha
                arrow.SetActive(true);
                //Guardamos en la variable paso, el paso actual
                int paso = (int)CurrentStep.Current;
                //Y al animator, le mandamos un set Integer donde cambiara en el animator el parametro y añadira un +1;
                animator.SetInteger("Direccion", paso);
                 //A la flecha le transformarmos
                 arrow.transform.rotation = StepBase[paso];

                //hacemos el sonido
                m_fuenteDeSonido.PlayOneShot(EfectosPaso[paso - 1]);

                return true;
            }

        //Cuando termine de hacer todos los pasos... le dejamos en direccion 0 que es no hacer nada.
        animator.SetInteger("Direccion", 0);
        return false;
    

    }

    public bool CompruebaPaso(Animator animator, StepTipe _paso)
    {
        if (CurrentStep.MoveNext())
        {
            arrow.SetActive(true);
            int paso = (int)_paso;
            animator.SetInteger("Direccion", paso);
            //Le ponemos el sonido
            m_fuenteDeSonido.PlayOneShot(EfectosPaso[paso - 1]);

            //A la flecha le transformarmos
            arrow.transform.rotation = StepBase[paso];

            //Comprobamos que el paso actual sea igual al paso que le he pasado. 
            if(CurrentStep.Current == _paso)
            {
                animator.Update(0);
                animator.SetInteger("Direccion", 0);
                return true;
            }

        }

        animator.SetInteger("Direccion", 0);
        return false;
    }


    float m_timerEmpezar;

    public void muestraEmpezar( )
    {
        TocarSonidos(MainGame.Efectos.Empezar1, MainGame.Efectos.Empezar2);
        comeOn.enabled = true;
        comeOn.color = Color.white;
        m_timerEmpezar = 2;
        
    }


    public void ActualizaEmpezar()
    {
        if (comeOn.enabled)
        {
            m_timerEmpezar -= Time.deltaTime * 2.0f;
            if(m_timerEmpezar < 0)
            {
                m_timerEmpezar = 0;
                comeOn.enabled = false;
            }

           Color tmp = comeOn.color;
            tmp.a = m_timerEmpezar;
            comeOn.color = tmp;
        }
    }

    public void MalPaso()
    {

        TocarSonidos(MainGame.Efectos.Mal1, MainGame.Efectos.Mal2);
        //indicador de mal
        wrong.SetActive(true);
        m_animadorJugador.SetInteger("Direccion", 5);
        //Evitamos que una animacion no se ejecute siempre

        m_animadorJugador.Update(0); // Lo que hace es ejecutar un 0 para refrescar la animacion digamos, y luego le pasmaos un 0.
        m_animadorJugador.SetInteger("Direccion", 0);


        //decrementar vidas
        i_lives--;
        updateLives();

        if (i_lives < 1)
        {
            EstadoBase.CambiarEstado(EstadoFinDeJuego.Instancia);
        }
        else
        {
            EstadoBase.CambiarEstado(EstadoFinJugador.Instancia);
        }

    }


    //Funcion que comprueba si ha terminado
    public bool HaTerminado()
    {
        //hacemos copia del enumerador
        var copia = CurrentStep;
        //Y devolvemos si la copia no tiene un paso siguiente, sin mover el ennumerador, para ver si es el ultimo.
        return !copia.MoveNext();
    }


    public void GameOver()
    {
        compararMaximasPuntuaciones();
        inGame.SetActive(false);
        gameOver.SetActive(true);
        MaxScore.enabled = true;
        currentScore.enabled = true;
        currentScore.text = "Current Score: " + i_myPoints;
        MaxScore.text = "Max Score: " + highScore;
    }




    public void TocarSonidos(params Efectos[] efectos)
    {
        int indice = Random.Range(0, efectos.Length);
        m_fuenteDeSonido.PlayOneShot(EfectosDeSonido[(int)efectos[indice]]);
    } 
}

