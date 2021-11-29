using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, InterfaceActorTemplate
{

    GameObject actor;
    int hitPower;
    int health;
    int travelSpeed;
    [SerializeField] int bulletSpeed;
    [SerializeField] SOActorModel enemyBulletModel;
    private Vector3 direction;

    private void Awake()
    {
        ActorStats (enemyBulletModel);
    }
    void Start()
    {
        direction = Player.instance.transform.position - transform.position * Time.deltaTime;
        direction.Normalize();
    }

    private void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime * 5.0F;
    }

    public void ActorStats(SOActorModel actorModel)
    {
        hitPower = actorModel.hitPower;
        health = actorModel.health;
        travelSpeed = actorModel.speed;
        actor = actorModel.actor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            if (other.GetComponent<InterfaceActorTemplate>() != null)
            {
                if (health >= 1)
                {
                    health -= other.GetComponent<InterfaceActorTemplate>().SendDamage();
                }
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }


    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public int SendDamage()
    {
        return hitPower;
    }

    void InterfaceActorTemplate.TakeDamage(int incomingDamage)
    {
        health -= incomingDamage;
    }

    void InterfaceActorTemplate.Die()
    {
        throw new System.NotImplementedException();
    }
}
    
