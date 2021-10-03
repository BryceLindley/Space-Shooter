using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameManager.playerLives = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
			SceneManager.LoadScene("shop");
        }
	}

}
