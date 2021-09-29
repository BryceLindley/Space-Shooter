using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	//Inside the PlayerSpawner class, we add two global variables: the first variable is the actorModel, which holds a scriptable object asset 
	//that will contain values for the player ship, and the second variable will hold our player ship once it's created from our CreatePlayer method.
	SOActorModel actorModel;
	GameObject playerShip;
	bool upgradedShip = false;

	// Use this for initialization
	void Start() {
		CreatePlayer();
		GetComponentInChildren<Player>().enabled = true;
	}

	// Update is called once per frame
	void Update() {

	}

	void CreatePlayer()
	{
		/*//CREATE PLAYER
		actorModel = Object.Instantiate(Resources.Load("Script/ScriptableObject/Player_Default")) as SOActorModel;
		playerShip = GameObject.Instantiate(actorModel.actor) as GameObject;
		playerShip.GetComponent<Player>().ActorStats(actorModel);
		//SET PLAYER UP
		// sets the rotation of ship to face the right way
		playerShip.transform.rotation = Quaternion.Euler(0, 180, 0);
		// sets the size(scale) of the ship on all axis
		playerShip.transform.localScale = new Vector3(60, 60, 60);
		// sets the size(scale) of the thruster
		playerShip.GetComponentInChildren<ParticleSystem>().transform.localScale = new Vector3(25, 25, 25);
		// rename instatiated ship to Player instead of (Clone)
		playerShip.name = "Player";
		// make the playerShip game object a child of the _Player game object in the Hierarchy window so we can find it
		playerShip.transform.SetParent(this.transform);
		// reset the player ship's position
		playerShip.transform.position = Vector3.zero;
		return playerShip;
		*/

		//been shopping
		if (GameObject.Find("UpgradedShip"))
		{
			upgradedShip = true;
		}

		//not shopped or died
		//default ship build
		if (!upgradedShip || GameManager.Instance.Died)
		{
			GameManager.Instance.Died = false;
			actorModel = Object.Instantiate(Resources.Load("Script/ScriptableObject/Player_Default")) as SOActorModel;
			playerShip = GameObject.Instantiate(actorModel.actor, this.transform.position, Quaternion.Euler(270, 180, 0)) as GameObject;
			playerShip.GetComponent<InterfaceActorTemplate>().ActorStats(actorModel);
		}
		else
		{
			playerShip = GameObject.Find("UpgradedShip");
		}
			playerShip.transform.rotation = Quaternion.Euler(0, 180, 0);
			playerShip.transform.localScale = new Vector3(60, 60, 60);
			// Turn the Player script off so the player can't control the ship while it carries out its intro animation
			playerShip.GetComponentInChildren <ParticleSystem>().transform.localScale = new Vector3(25, 25, 25);
			playerShip.name = "Player";
			playerShip.transform.SetParent(this.transform);
			playerShip.transform.position = Vector3.zero;
	}
}



		

