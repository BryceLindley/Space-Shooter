using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, InterfaceActorTemplate
{
    public Rigidbody theRB;
    public float moveSpeed;
    private Vector3 moveDirection;
    
    public int health = 150;
    public GameObject[] deathSplatters;
    public GameObject hitEffect;
    int hitPower;
    int travelSpeed;
    GameObject actor;
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    private Animation anim;

    public void ActorStats(SOActorModel actorModel)
    {
        hitPower = actorModel.hitPower;
        health = actorModel.health;
        travelSpeed = actorModel.speed;
        actor = actorModel.actor;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShoot)
        {
            fireCounter -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                fireCounter = fireRate;
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
    }


    public void Die()
    {
        Destroy(this.gameObject);
       
    }

    public int SendDamage()
    {
        return hitPower;
    }

    public void TakeDamage(int incomingDamage)
    {
        throw new System.NotImplementedException();
    }




}
