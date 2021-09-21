using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipBuild : MonoBehaviour {

	[SerializeField]
	GameObject[] shopButtons;
	GameObject target;
	GameObject tmpSelection;
	GameObject textBoxPanel;
	[SerializeField]
	GameObject[] visualWeapons;
	[SerializeField]
	SOActorModel defaultPlayerShip;
	GameObject playerShip;
	GameObject buyButton;
	GameObject bankObj;
	int bank = 600;
	bool purchaseMade = false;

	// Use this for initialization
	void Start () {
		TurnOffSelectionHighlights();
		textBoxPanel = GameObject.Find("textBoxPanel");
		purchaseMade = false;
		bankObj = GameObject.Find("bank");
		bankObj.GetComponentInChildren<TextMesh>().text = bank.ToString();
		buyButton = textBoxPanel.transform.Find("BUY ?").gameObject;
		TurnOffPlayerShipVisuals();
		PreparePlayerShipForUpgrade();
	}

	void Update()
	{
		AttemptSelection();
	}

	// Within the TurnOffSelectionHighlights method, we run a for loop that makes sure all of the buttons have their blue rectangles turned off.
	  void TurnOffSelectionHighlights()
    {
	for (int i = 0; i < shopButtons.Length; i++)
        {
			shopButtons[i].SetActive(false);
       }
    }

	//Within this method, we reset the target game object to remove any previous data.
	//We then take reference from the camera to find where the player tapped or clicked
	//their mouse on the screen and store the result in the form of a ray. 
	GameObject ReturnClickedObject(out RaycastHit hit)
    {
		GameObject target = null;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray.origin, ray.direction * 100, out hit))
        {
			target = hit.collider.gameObject;
        }
		return target;
    }

	// The AttemptSelection method will check when a condition is made when the player
	// has made contact by tapping the screen or clicking a mouse button in our shop scene.
	  void AttemptSelection()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo;
			target = ReturnClickedObject(out hitInfo);
			if (target != null)
			{
				if (target.transform.Find("itemText"))
				{
					TurnOffSelectionHighlights();
					Select();
					UpdateDescriptionBox();

					//Not Already Sold
					if (target.transform.Find("itemText").GetComponent<TextMesh>().text != "SOLD")
                    {
						//can afford
						Affordable();

						//can not afford
						LackOfCredits();
                    }
					else if (target.transform.Find("itemText").GetComponent<TextMesh>().text == "SOLD")
                    {
						SoldOut();
                    }
				}
			}
		}
	}

	//The Select method doesn't need to check any conditions with if statements as this
	//has mostly been done for us with the previous code.
	//We carry out a search for SelectionQuad and store its reference as tmpSelection.
	//Finally, we set the tmpSelection game objects activity to true so that it is seen in our shop Scene window.

  void Select()
	{
		tmpSelection = target.transform.Find
		   ("SelectionQuad").gameObject; tmpSelection.SetActive(true);
	}


	// The UpdateDescriptionBox method will grab the selected button's asset file variable
	// content, iconName and description, and apply it to the TextMesh text component of textboxPanel.
	  void UpdateDescriptionBox()
	{
		textBoxPanel.transform.Find("name").gameObject.GetComponent<TextMesh>().text = tmpSelection.GetComponentInParent<ShopPiece>().ShopSelection.iconName;
		textBoxPanel.transform.Find("desc").gameObject.GetComponent<TextMesh>().text = tmpSelection.GetComponentInParent<ShopPiece>().ShopSelection.description;
	}



}
