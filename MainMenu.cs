
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {



    void Awake() {

        Debug.Log("Hola Mundo Awake");

    }

    // Use this for initialization
    void Start () {

        Debug.Log("Hola Mundo Start");


	}




    // Update is called once per frame
    void Update()
    {

    }


      
    




		
    public void irAlJuego()
    {
        SceneManager.LoadScene("SceneMainGame");
    }




	}





