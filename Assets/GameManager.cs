using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject stationBullet;
    Queue<GameObject> stationBullets;
    public GameObject playerBullet;
    Queue<GameObject> playerBullets;
    public GameObject enemyBullet;
    Queue<GameObject> enemyBullets;
    public GameObject ant;
    Queue<GameObject> ants;
    public GameObject wasp;
    Queue<GameObject> wasps;
    public GameObject fly;
    Queue<GameObject> fliess;
    public static GameManager instance;

    public GameObject managers;

    //these handle the enemy spawn times, ranges, etc (it's a little random)
    public float randSpawnTimeRange;
    public float minNextSpawnTime;
    private float nextSpawnTime;
    private float lastSpawnTime;


    void Awake()
    {
        instance = this;
        stationBullets = new Queue<GameObject>();
        playerBullets = new Queue<GameObject>();
        enemyBullets = new Queue<GameObject>();

        transform.position = Vector3.zero;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //the junk spawning is working independently from here and is accessible in the the main scene
        //why? I don't know, not the cleanest way to do it but it's 12pm and I need to hurry
        //spawning loop
        //won't spanw enemies at level 0
        if (managers.GetComponent<LevelManager>().getLevel() == 0 && Time.time >= nextSpawnTime)
        {
            managers.GetComponent<SpawnManager>().spawnAt(managers.GetComponent<LevelManager>().getLevel());
            nextSpawnTime = Time.time + minNextSpawnTime + Random.Range(0, randSpawnTimeRange);          
        }
    }

    public void StationShoot(Vector3 position, Vector3 direction, float lifetime)
    {
        if(stationBullets.Count == 0)
        {
            GameObject bullet = Instantiate(stationBullet,position,Quaternion.identity ,transform);
            bullet.transform.up = direction.normalized;
            bullet.GetComponent<Bullet>().direction = direction.normalized;
            bullet.GetComponent<Bullet>().lifetime = lifetime;
            bullet.GetComponent<Bullet>().birthTime = Time.time;
        }
        else
        {
            GameObject bullet = stationBullets.Dequeue();
            bullet.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.None;
            bullet.transform.up = direction.normalized;
            bullet.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeRotation;
            bullet.transform.position = position;
            bullet.GetComponent<Bullet>().direction = direction.normalized;
            bullet.GetComponent<Bullet>().lifetime = lifetime;
            bullet.GetComponent<Bullet>().birthTime = Time.time;
            bullet.SetActive( true );
        }
    }

    public void TerminateStationBullet(GameObject bullet)
    {
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        bullet.GetComponent<Rigidbody2D>().SetRotation(0);
        bullet.SetActive(false); 
        stationBullets.Enqueue(bullet);
    }

    public void EnemyShoot(Vector3 position, Vector3 direction, float lifetime)
    {
        if(stationBullets.Count == 0)
        {
            GameObject bullet = Instantiate(enemyBullet,position,Quaternion.identity ,transform);
            bullet.transform.up = direction.normalized;
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Bullet>().lifetime = lifetime;
            bullet.GetComponent<Bullet>().birthTime = Time.time;
        }
        else
        {
            GameObject bullet = enemyBullets.Dequeue();
            bullet.transform.up = direction.normalized;
            bullet.transform.position = position;
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Bullet>().lifetime = lifetime;
            bullet.GetComponent<Bullet>().birthTime = Time.time;
            bullet.SetActive( true );
        }
    }

    public void TerminateEnemyBullet(GameObject bullet)
    {
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        bullet.GetComponent<Rigidbody2D>().SetRotation(0);
        bullet.SetActive(false);
        enemyBullets.Enqueue(bullet);
    }

    public void PlayerShoot(Vector3 position, Vector3 direction, float lifetime)
    {
        if(playerBullets.Count == 0)
        {
            GameObject bullet = Instantiate(playerBullet,position,Quaternion.identity ,transform);
            bullet.transform.up = direction.normalized;
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Bullet>().lifetime = lifetime;
            bullet.GetComponent<Bullet>().birthTime = Time.time;
        }
        else
        {
            GameObject bullet = playerBullets.Dequeue();
            bullet.transform.up = direction.normalized;
            bullet.transform.position = position;
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Bullet>().lifetime = lifetime;
            bullet.GetComponent<Bullet>().birthTime = Time.time;
            bullet.SetActive( true );
        }
    }

    public void TerminatePlayerBullet(GameObject bullet)
    {
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        bullet.GetComponent<Rigidbody2D>().SetRotation(0);
        bullet.SetActive(false);
        playerBullets.Enqueue(bullet);
    }


}
