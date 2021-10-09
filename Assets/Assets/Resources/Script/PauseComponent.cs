using UnityEngine.UI;
using UnityEngine;

public class PauseComponent : MonoBehaviour {

	[SerializeField] GameObject pauseScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake()
    {
		pauseScreen.SetActive(false);
		SetPauseButtonActive(false);
		Invoke("DelayPauseAppear", 5);
    }

	void SetPauseButtonActive(bool switchButton)
    {
		// changes all colors to white and transparent and disables button interaction
		ColorBlock col = GetComponentInChildren<Toggle>().colors;
		if (switchButton == false)
		{
			col.normalColor = new Color32(0, 0, 0, 0);
			col.highlightedColor = new Color32(0, 0, 0, 0);
			col.pressedColor = new Color32(0, 0, 0, 0);
			col.disabledColor = new Color32(0, 0, 0, 0);
			GetComponentInChildren<Toggle>().interactable = false;
		}
        else
        {
            col.normalColor = new Color32(245, 245, 245, 255);
            col.highlightedColor = new Color32(245, 245, 245, 255);
            col.pressedColor = new Color32(200, 200, 200, 255);
            col.disabledColor = new Color32(200, 200, 200, 128);
            GetComponentInChildren<Toggle>().interactable = true;
        }
        GetComponentInChildren<Toggle>().colors = col;
        GetComponentInChildren<Toggle>().transform.GetChild(0).GetChild(0).gameObject.SetActive(switchButton);
    }  

	void DelayPauseAppear()
    {
		SetPauseButtonActive(true);
    }

	public void PauseGame()
    {
		pauseScreen.SetActive(true);
		SetPauseButtonActive(false);
		// sets all moving, animating objects in the scene to 0
		Time.timeScale = 0;
    }
}
