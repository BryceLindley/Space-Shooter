using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerShipBuild : MonoBehaviour
{

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
	string placementId_rewardedvideo = "rewardedVideo";
	string gameId = "71f3b3d8-bdd4-4de6-9f58-ed6e2c81c214";


	// Use this for initialization
	void Start()
	{
		TurnOffSelectionHighlights();
		textBoxPanel = GameObject.Find("textBoxPanel");
		purchaseMade = false;
		bankObj = GameObject.Find("bank");
		bankObj.GetComponentInChildren<TextMesh>().text = bank.ToString();
		buyButton = textBoxPanel.transform.Find("BUY?").gameObject;
		TurnOffPlayerShipVisuals();
		PreparePlayerShipForUpgrade();
		CheckPlatform();
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
		if (Physics.Raycast(ray.origin, ray.direction * 100, out hit))
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
				else if (target.name == "BUY?")
				{
					BuyItem();
				}
				else if (target.name == "START")
				{
					StartGame();
				}
				else if (target.name == "WATCH AD")
				{
					//WatchAdvert();
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

	//The Affordable method checks whether the bank integer(which currently contains the value 600) is equal or greater than the
	//value of the button that we have selected(target). Next is an if statement that checks whether the bank integer value is
	//greater than or equal to the string cost value of the selected item.Because we can't compare the value of a string
	// variable to an int variable, we need to convert the string variable to an int variable.
	// To do this, we use System.Int32.Parse() and enter the ShopeSelection.cost string value in the parse brackets.
	void Affordable()
	{
		if (bank >= System.Int32.Parse(target.transform.GetComponent<ShopPiece>().ShopSelection.cost))
		{
			Debug.Log("CAN BUY");
			buyButton.SetActive(true);
		}
	}

	// The second method we wrote earlier was the LackOfCredits method, which checks in a similar way by casting
	// the TextMesh component value if it's less than the bank integer value.
	// If it is, we send a "CAN'T BUY" message to Unity's Console window:
	void LackOfCredits()
	{
		if (bank < System.Int32.Parse(target.transform.Find
		  ("itemText").GetComponent<TextMesh>().text))
		{
			Debug.Log("CAN'T BUY");
		}
	}

	void SoldOut()
	{
		Debug.Log("SOLD OUT");
	}

	void TurnOffPlayerShipVisuals()
	{
		for (int i = 0; i < visualWeapons.Length; i++)
		{
			visualWeapons[i].gameObject.SetActive(false);
		}
	}

	//The method creates(instantiates) a Player_Ship game object from the Resources folder. We then turn off(enabled = false)
	//its own script attachment; otherwise, we would be able to move and shoot with it in the shop scene.
	//We then move the Player_Ship object completely out of the Scene / Game window view. Finally,
	//we assign it the default PlayerShip asset file that we dragged and dropped into the scriptable object field.
	void PreparePlayerShipForUpgrade()
	{
		playerShip = GameObject.Instantiate(Resources.Load("Prefab/Player/playership")) as GameObject;
		playerShip.GetComponent<Player>().enabled = false;
		playerShip.transform.position = new Vector3(0, 10000, 0);
		playerShip.GetComponent<InterfaceActorTemplate>().ActorStats(defaultPlayerShip);
	}

	//We then set purchaseMade to true. This Boolean value is used later when we leave the shop scene to
	//start the game.If purchaseMade is true, a set of procedures follows.The next line turns off the buyButton
	//function as we no longer need to display the results. Finally, we remove the selection from the grid at the
	//bottom of the screen as a refresh.
	void BuyItem()
	{
		Debug.Log("PURCHASED");
		purchaseMade = true;
		buyButton.SetActive(false);
		tmpSelection.SetActive(false);

		for (int i = 0; i < visualWeapons.Length; i++)
		{
			// check whether slection made in the selection grid matches so we can see it
			if (visualWeapons[i].name == tmpSelection.transform.parent.gameObject.GetComponent<ShopPiece>().ShopSelection.iconName)
			{
				visualWeapons[i].SetActive(true);
			}
		}
		//send our upgrades to our player's ship, along with our bank credit
		UpgradeToShip(tmpSelection.transform.parent.gameObject.GetComponent<ShopPiece>().ShopSelection.iconName);
		bank = bank - System.Int32.Parse(tmpSelection.transform.parent.GetComponent<ShopPiece>().ShopSelection.cost);
		bankObj.transform.Find("bankText").GetComponent<TextMesh>().text = bank.ToString();
		tmpSelection.transform.parent.transform.Find("itemText").GetComponent<TextMesh>().text = "SOLD";
	}

	//which loads the game object of that particular ship part and attaches it to a ship that is away from the screen.
	void UpgradeToShip(string upgrade)
	{
		GameObject shipItem = GameObject.Instantiate(Resources.Load("Prefab/Player/" + upgrade)) as GameObject;
		shipItem.transform.SetParent(playerShip.transform); shipItem.transform.localPosition = Vector3.zero;
	}

	void StartGame()
	{
		if (purchaseMade)
		{
			playerShip.name = "UpgradeShip";
			if (playerShip.transform.Find("energy+1(Clone)"))
			{
				playerShip.GetComponent<Player>().Health = 2;
			}
			DontDestroyOnLoad(playerShip);
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("testLevel");
	}

	void CheckPlatform()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			gameId = "REPLACE-THIS-TEXT-FOR-YOUR-IPHONE-GAMEID";
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			gameId = "REPLACE-THIS-TEXT-FOR-YOUR-ANDROID-GAMEID";
		}
		//Monetization.Initialize(gameId, false);
	}

	/*void WatchAdvert()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			ShowRewardedAds();
		}
	}

	void ShowRewardedAds()
	{
		StartCoroutine(WaitForAd());
	}

	
	IEnumerator WaitForAd()
	{ string placementId = placementId_rewardedvideo;
		while (!Monetization.IsReady(placementId))
		{
			yield return null;
		}
		ShowAdPlacementContent ad = null;
		ad = Monetization.GetPlacementContent(placementId)
		   as ShowAdPlacementContent;
		if (ad != null)
		{
			ad.Show(AdFinished);
		}
	*/
}



