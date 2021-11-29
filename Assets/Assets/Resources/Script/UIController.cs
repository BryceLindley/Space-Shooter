using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Image fadeScreen;
	public float fadeSpeed;
    bool fadeToBlack;
	bool fadeOutBlack;
	public static UIController instance;
	public Slider healthSlider;
	public Text healthText;


	// Use this for initialization
	void Start () {
		fadeOutBlack = true;
		fadeToBlack = false;
	}

	void Awake()
    {
		instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0.0F, fadeSpeed * Time.deltaTime));
		if (fadeScreen.color.a == 0.0F)
        {
			fadeOutBlack = false;
        }

    
    }

	public void StartFadeToBlack()
    {
		fadeToBlack = true;
		fadeOutBlack = false;
    }
}
