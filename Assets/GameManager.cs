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
    public static GameManager instance;

    void Awake()
    {
        instance = this;
        stationBullets = new Queue<GameObject>();
        transform.position = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StationShoot(Vector3 position, Vector3 direction, float lifetime)
    {
        if(stationBullets.Count == 0)
        {
            GameObject bullet = Instantiate(stationBullet,position,Quaternion.identity ,transform);
            bullet.transform.up = direction.normalized;
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Bullet>().lifetime = lifetime;
            bullet.GetComponent<Bullet>().birthTime = Time.time;
        }
        else
        {
            GameObject bullet = stationBullets.Dequeue();
            bullet.SetActive( true );
            bullet.transform.up = direction.normalized;
            bullet.transform.position = position;
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Bullet>().lifetime = lifetime;
            bullet.GetComponent<Bullet>().birthTime = Time.time;
        }
    }

    public void TerminateStationBullet(GameObject bullet)
    {
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
            bullet.SetActive( true );
            bullet.transform.up = direction.normalized;
            bullet.transform.position = position;
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Bullet>().lifetime = lifetime;
            bullet.GetComponent<Bullet>().birthTime = Time.time;
        }
    }

    public void TerminateEnemyBullet(GameObject bullet)
    {
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
            bullet.SetActive( true );
            bullet.transform.up = direction.normalized;
            bullet.transform.position = position;
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Bullet>().lifetime = lifetime;
            bullet.GetComponent<Bullet>().birthTime = Time.time;
        }
    }

    public void TerminatePlayerBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        stationBullets.Enqueue(bullet);
    }


}
