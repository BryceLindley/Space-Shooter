using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] SOActorModel actorModel;
    [SerializeField] float spawnRate;
    [SerializeField] [Range(0, 100)] int quantity;
    GameObject enemies;
    GameObject fire;
    public GameObject Fire
    {
        get { return fire; }
        set { fire = value; }
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        GameObject bullet = GameObject.Instantiate(fire, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        bullet.transform.SetParent(enemies.transform);
        bullet.transform.localScale = new Vector3();
        return enemy;
    }

  
}




