using UnityEngine;

//The CreateAssetMenu attribute creates an extra selection from the drop-down list in the Project window
[CreateAssetMenu(fileName = "Create Actor", menuName = "Create  Actor")]

//Inside the SOActorModel we will be naming most, if not, all of these variables in the Player script. Similar to how an interface signs a contract with a class, the SOActorModel
//does the same because it's being inherited, but isn't as strict as an interface by throwing an error if the content from the scriptable object isn't applied.


public class SOActorModel : ScriptableObject
{
    public string actorName;
    public AttackType attackType;
    public enum AttackType
    {
        wave, player, flee, bullet
    }
    public string description;
    public int health;
    public int speed;
    public int hitPower;
    public GameObject actor;
    public GameObject actorsBullets;
}
