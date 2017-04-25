using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoMaxScore : MonoBehaviour {

    public Text MaxScore;

    public int HighScore;

	// Use this for initialization
	void Start () {

        HighScore = PlayerPrefs.GetInt("highscore");


        MaxScore.text = "Max Score: " + HighScore;




    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
