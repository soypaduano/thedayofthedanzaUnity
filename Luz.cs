using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luz : MonoBehaviour {

    public float tiempoDeLuz;


    Light m_miLuz;
    float fReloj;



	// Use this for initialization
	void Start () {

        //Aqui le pedimos a nuestro gameObject Luz que nos de un componente de tipo luz. 
        //La guardamos en una variable para hacerla una vez, y luego, cad avez que a necesitemos, pdoemos reutilizarla.
         m_miLuz =  gameObject.GetComponent<Light>();

        fReloj = 0;

        tiempoDeLuz = 5;

		
	}
	
	// Update is called once per frame
	void Update () {

        fReloj += Time.deltaTime;

        if(fReloj >= tiempoDeLuz) {
            m_miLuz.enabled = !m_miLuz.enabled;
            fReloj = 0;
        }

       // m_miLuz.enabled = !m_miLuz.enabled;
		
	}
}
