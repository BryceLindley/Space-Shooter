using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, InterfaceActorTemplate
{
    int travelSpeed;
    int health;
    int hitPower;
    GameObject actor;
    GameObject fire;
    // reference to the _Player game object in the scene
    GameObject _Player;
    // float width;
    // float height;
    float camTravelSpeed;
    float movingScreen;
    // holds the players touch screen location
    Vector3 direction;
    // reference to players ship
    Rigidbody rb;
    // static switch that lets the rest of the game know the player's controls
    public static bool mobile = false;
    // holds two points to represent our screen's boundaries
    GameObject[] screenPoints = new GameObject[2];


    public float CamTravelSpeed
    {
        get { return camTravelSpeed; }
        set { camTravelSpeed = value; }
    }

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
        mobile = false;
        #if UNITY_ANDROID && !UNITY_EDITOR
        
            mobile = true;
        InvokeRepeating("Attack", 0, 0.3f);
        rb = GetComponent<Rigidbody>();
        #endif

       
        // height = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).y - .5f);
        // width = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f);
        _Player = GameObject.Find("_Player");
        // movingScreen = width;
        CalculateBoundaries();
    }

    void Update()
    {
        // check to see if game has been paused
        if (Time.timeScale == 1)
        {
            PlayersSpeedWithCamera();
            if (mobile)
            {
                MobileControls();
            }
            else
            {
                Movement();
                Attack();
            }
        }
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
        GameObject explode = GameObject.Instantiate(Resources.Load("Prefab/explode")) as GameObject;
        explode.transform.position = this.gameObject.transform.position;
        GameManager.Instance.LifeLost();
        Destroy(this.gameObject);
    }


    //purpose of taking the value from the p1 or p2 game objects to get a restriction of the boundaries of the screen.This ensures the player ship doesn't go too far out of view.
   void Movement()
    {

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if(transform.localPosition.x < movingScreen + (screenPoints[1].transform.localPosition.x - screenPoints[1].transform.localPosition.x / 30.0F) + movingScreen)
            {
                transform.localPosition += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * travelSpeed, 0, 0);
            }
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (transform.localPosition.x < (screenPoints[1].transform.localPosition.x - screenPoints[1].transform.localPosition.x / 30.0F) + movingScreen)
            {
                transform.localPosition += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * travelSpeed, 0, 0);
            }

        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (transform.localPosition.y > (screenPoints[1].transform.localPosition.y - screenPoints[1].transform.localPosition.y / 3.0F))
            {
                transform.localPosition += new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * travelSpeed, 0);
            }
        }
        if (Input.GetAxisRaw("Vertical") > 0)

            if (transform.localPosition.y < (screenPoints[0].transform.localPosition.y - screenPoints[0].transform.localPosition.y / 5.0F))
            {
                {
                    transform.localPosition += new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * travelSpeed, 0);
                }
            }
    }


    public void Attack()
    {
        if (Input.GetButtonDown("Fire1") || mobile)
        {
            GameObject bullet = GameObject.Instantiate(fire, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            bullet.transform.SetParent(_Player.transform);
            bullet.transform.localScale = new Vector3(7, 7, 7);
        }
    }

    public void PlayersSpeedWithCamera()
    {
        if (camTravelSpeed > 1)
        {
            transform.position += Vector3.right * Time.deltaTime * camTravelSpeed;
            movingScreen += Time.deltaTime * camTravelSpeed;
        }
        else
        {
            movingScreen = 0;
        }
    }

    void MobileControls()
    {
        //Check to see if there has been more than one touch on the screen of the device.  
        // Checking to see if item touched was not an event system (pause menu)
        if(Input.touchCount > 0 && EventSystem.current.currentSelectedGameObject == null)
        {
            // Assign a touch variable
            Touch touch = Input.GetTouch(0);
            // Take a ready-made function from Unity to convert the screen's touch position and store it in a world space position
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 300));
            // Because we aren't affecting the players ship's Z axis we set touch position to 0.
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            // Send the player_ship gameObject to the Vecto3 position that is stored in direction.  Multiply by 5 to make it move slightly faster.
            rb.velocity = new Vector3(direction.x, direction.y, 0) * 5;
            // Apply value is in the movingScreen to the direction.x position.
            direction.x += movingScreen;
            // if the the sate of touch phase has ended, apply a zero value to the rb velocity variable.
            if(touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector3.zero;
            }
        }


    }

    void CalculateBoundaries() {
        /*
        First, it creates two new game objects and names them "p1" and "p2".We then make use of the ViewportToWorldPoint 
        function, which will give us our game's world space positions for our screens boundaries. 
        Then, we apply our new Vector3 variables, v1 and v2, to our array of game object's positions; 
        that is,"p1" and "p2".Now that "p1" and "p2" represent the boundaries, we need to make them children 
        of the Player script, which will update their Transform Position values. Finally, we update the movingScreen
        float value with our screenPoint value for when the game has a moving camera.
        */

      screenPoints[0] = new GameObject("p1");
        screenPoints[1] = new GameObject("p2");
        Vector3 v1 = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 300));
        Vector3 v2 = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 300));
        screenPoints[0].transform.position = v1;
        screenPoints[1].transform.position = v2;
        screenPoints[0].transform.SetParent(this.transform.parent);
        screenPoints[1].transform.SetParent(this.transform.parent);
        movingScreen = screenPoints[1].transform.position.x;
    }

}














