using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	//Inside the PlayerSpawner class, we add two global variables: the first variable is the actorModel, which holds a scriptable object asset 
	//that will contain values for the player ship, and the second variable will hold our player ship once it's created from our CreatePlayer method.
	SOActorModel actorModel;
	GameObject playerShip;
  
	// Use this for initialization
	  void Start () {
		CreatePlayer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreatePlayer()
	{
		//CREATE PLAYER
		actorModel = Object.Instantiate(Resources.Load("Script/ScriptableObject/Player_Default")) as SOActorModel;
		playerShip = GameObject.Instantiate(actorModel.actor) as GameObject;
		playerShip.GetComponent<Player>().ActorStats(actorModel);
		//SET PLAYER UP
		// sets the rotation of ship to face the right way
		playerShip.transform.rotation = Quaternion.Euler(0, 180, 0);
		// sets the size(scale) of the ship on all axis
		playerShip.transform.localScale = new Vector3(60, 60, 60);
		// rename instatiated ship to Player instead of (Clone)
		playerShip.name = "Player";
		// make the playerShip game object a child of the _Player game object in the Hierarchy window so we can find it
		playerShip.transform.SetParent(this.transform);
		// reset the player ship's position
		playerShip.transform.position = Vector3.zero;
	}
}



		

