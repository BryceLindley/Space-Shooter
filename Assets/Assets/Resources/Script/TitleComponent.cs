using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleComponent : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
			SceneManager.LoadScene("shop");
        }
	}

	void Start()
    {
		if (GameManager.playerLives <= 2)
			GameManager.playerLives = 3;

    }

}
