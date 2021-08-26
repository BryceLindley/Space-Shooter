using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	static GameManager instance;
	public static GameManager Instance
    {
		get { return instance; }
    }
	

  
	// Use this for initialization
	  void Start () {
		CameraSetup();
		LightSetup();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake ()
    {
		CheckGameManagerIsInTheScene();
    }



	void CameraSetup() {
		GameObject gameCamera = GameObject.FindGameObjectWithTag("MainCamera");

		//Camera Transform
		gameCamera.transform.position = new Vector3(0, 0,-300);
		gameCamera.transform.eulerAngles = new Vector3(0, 0, 0);

		//Camera Properties
		gameCamera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
		gameCamera.GetComponent<Camera>().backgroundColor = new Color32(0, 0, 0, 255);
	}

	void LightSetup()
	{
		GameObject dirLight = GameObject.Find("Directional Light");
		dirLight.transform.eulerAngles = new Vector3(50, -30, 0);
		dirLight.GetComponent<Light>().color = new Color32(152, 204, 255, 255);
	}

	void CheckGameManagerIsInTheScene()
    {
        {
			if(instance == null)
            {
				instance = this;
            }
            else
            {
				Destroy(this.gameObject);
            }
			DontDestroyOnLoad(this);
        }

    }

	
}
