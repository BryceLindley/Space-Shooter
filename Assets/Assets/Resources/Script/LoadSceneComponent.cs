using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneComponent : MonoBehaviour {

	float timer = 0;
	public string loadThisScene;

	// Use this for initialization
	void Start () {
		GameManager.Instance.GetComponentInChildren<ScoreManager>().ResetScore();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > 3)
        {
			SceneManager.LoadScene(loadThisScene);
        }
	}
}
