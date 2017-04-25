

public class EstadoBase {

    public virtual void Start(MainGame game)
    {

    }

    public virtual void Update(MainGame game)
    {

    }

    public virtual void End(MainGame game)
    {

    }



    static MainGame m_Game;
    static EstadoBase m_Actual;

    public static void SetGame(MainGame mainGame)
    {
        m_Game = mainGame; //Le pasamos como parametro el game que hemos recibido
    }


    public static void CambiarEstado(EstadoBase nuevoEstado)
    {
        if(m_Actual != null)  m_Actual.End(m_Game);

        m_Actual = nuevoEstado;

        if (m_Actual != null) m_Actual.Start(m_Game);

 
    }

    public static void Actualizar()
    {
        if (m_Actual != null) m_Actual.Update(m_Game);
    }
	
}

public class InstanciaEstadoBase<T>: EstadoBase where T: EstadoBase, new() //Creamos una plantilla, un template. Tiene que hredar de estadobase y aempas puede tener un constructor
{
    static T _instancia;
    public static T Instancia {
        get
        { if (_instancia == null)
              _instancia = new T();
            return _instancia;
        }
    }
}
