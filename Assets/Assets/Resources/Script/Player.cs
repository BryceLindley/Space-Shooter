
using UnityEngine;

public class Player : MonoBehaviour, InterfaceActorTemplate
{
    int travelSpeed;
    int health;
    int hitPower;
    GameObject actor;
    GameObject fire;

    //The two public properties of Health and Fire are there to give access 
    //to our two private health and fire variables from other classes that require access.
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    public GameObject Fire
    {
        get { return fire; }
        set { fire = value; }
    }
    // reference to the _Player game object in the scene
    GameObject _Player;
    float width;
    float height;

    void Start()
    {
        height = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).y - .5f);
        width = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f);
        _Player = GameObject.Find("_Player");
    }

    void Update()
    {
        Movement();
        Attack();
    }

    //The code we have just entered assigns values from the player's SOActorModel ScriptableObject asset we made earlier
    //on in the chapter. This method doesn't get run in our script but gets accessed by other classes, the reason 
    //being these variables hold values regarding our player and don't need to be anywhere else.
    // interface requirement from InterfaceActorTemplate
    public void ActorStats(SOActorModel actorModel)
    {
        health = actorModel.health;
        travelSpeed = actorModel.speed;
        hitPower = actorModel.hitPower;
        fire = actorModel.actorsBullets;
    }
}


