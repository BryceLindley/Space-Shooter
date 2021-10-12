using UnityEngine;

public class Player : MonoBehaviour, InterfaceActorTemplate
{
    int travelSpeed;
    int health;
    int hitPower;
    GameObject actor;
    GameObject fire;
    // reference to the _Player game object in the scene
    GameObject _Player;
    float width;
    float height;
    private float horizontalInput;
    private float verticalInput;

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

    void Start()
    {
        height = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).y - .5f);
        width = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f);
        _Player = GameObject.Find("_Player");
    }

    void Update()
    {
        if(Time.timeScale == 1) 
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

    public void TakeDamage(int incomingDamage)
    {
        health -= incomingDamage;
    }
    public int SendDamage()
    {
        return hitPower;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (health >= 1)
            {
                if (transform.Find("energy +1(Clone)"))
                {
                    Destroy(transform.Find("energy +1(Clone)").gameObject);
                    health -= other.GetComponent<InterfaceActorTemplate>
                      ().SendDamage();
                }
                else
                {
                    health -= 1;
                }
            }
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        GameManager.Instance.LifeLost();
        Destroy(this.gameObject);
    }

    void Movement()
    {

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (transform.localPosition.x < width + width / 0.9f)
            {
                transform.localPosition += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * travelSpeed, 0, 0);
            }
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (transform.localPosition.x > width + width / 6)
            {
                transform.localPosition += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * travelSpeed, 0, 0);
            }
        }
        if (Input.GetAxisRaw("Vertical") < 0 && transform.localPosition.y < 150)

        {
            transform.localPosition += new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * travelSpeed, 0);
        }

        else
        {

            {
                transform.localPosition += new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * travelSpeed, 0);
            }
        }
        
    }
    

    public void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = GameObject.Instantiate(fire, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            bullet.transform.SetParent(_Player.transform);
            bullet.transform.localScale = new Vector3(7, 7, 7);
        }
    }
}







    






