using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] SOActorModel actorModel;
    [SerializeField] float spawnRate;
    [SerializeField] [Range(0, 100)] int quantity;
    GameObject enemies;
    GameObject fire;
    public Rigidbody theRB;
    public Animator anim;
    public GameObject[] deathSplatters;
    public GameObject hitEffect;
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    bool StopExploding;
    public float time = 5;

    public GameObject Fire
    {
        get { return fire; }
        set { fire = value; }
    }


    // Update is called once per frame
    void Update()
    {
       if(shouldShoot)
        {
            fireCounter -= Time.deltaTime;
            if(fireCounter <= 0)
            {
                fireCounter = fireRate;
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
    }

    void Awake()
    {
        // make instance of empty game object
        enemies = GameObject.Find("_Enemies");
        StartCoroutine(FireEnemy(quantity, spawnRate));
    }

    IEnumerator FireEnemy(int qty, float spwnRte)
    {
        for (int i = 0; i < qty; i++)
        {
            GameObject enemyUnit = CreateEnemy();
            enemyUnit.gameObject.transform.SetParent(this.transform);
            enemyUnit.transform.position = transform.position;
            yield return new WaitForSeconds(spwnRte);
        }
        yield return null;
    }

    GameObject CreateEnemy()
    {
        GameObject enemy = GameObject.Instantiate(actorModel.actor) as GameObject;
        enemy.GetComponent<InterfaceActorTemplate>().ActorStats(actorModel);
        enemy.name = actorModel.actorName.ToString();
        return enemy;
    }

  
}




