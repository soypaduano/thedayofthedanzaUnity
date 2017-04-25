using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimaciones : MonoBehaviour {

    //reference to the animator of Chica
    Animator miAnimator;

    //Vector(X,Y) where the touch is detected at first
    Vector2 inicioToque;

    //some sounds
    public AudioClip sound_1;
    public AudioListener player;
    public AudioSource myOriginSound;

    // Use this for initialization
    void Start() {
        miAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame 
    void Update() {



        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            miAnimator.SetInteger("Direccion", 1);
            myOriginSound.PlayOneShot(sound_1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            miAnimator.SetInteger("Direccion", 2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            miAnimator.SetInteger("Direccion", 3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            miAnimator.SetInteger("Direccion", 4);
        }





        //CONTROLAMIENTO DE GESTOS EN LA PANTALLA UNITY  

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                inicioToque = Input.touches[0].position;

            } else {

                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    Vector2 dif = Input.touches[0].position - inicioToque;


                    if (Mathf.Abs(dif.x) > Mathf.Abs(dif.y))
                    {
                        if (dif.x < 0)
                        { }//Movimiento izquierda
                        else
                        { } //Movimiento derecha


                    } else
                    {
                        if (dif.y < 0) //movimiento arriba
                        { }
                        else
                        { } //movimiento abajo
                    }
                }
            }
        }
    }
}


 