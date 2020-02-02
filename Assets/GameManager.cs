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
    GameManager instance ;

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

    public void StationShoot(Vector3 position, Vector3 direction, float range)
    {
        if(stationBullets.Count == 0)
        {
            GameObject bullet = Instantiate(stationBullet,position,Quaternion.identity ,transform);
            bullet.transform.up = direction.normalized;
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Bullet>().range = range;
            bullet.GetComponent<Bullet>().startingPosition = position;
        }
        else
        {
            GameObject bullet = enemyBullets.Dequeue();
            bullet.SetActive( true );
            bullet.transform.up = direction.normalized;
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.GetComponent<Bullet>().range = range;
            bullet.GetComponent<Bullet>().startingPosition = position;
        }
    }

    void TerminateStationBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        stationBullets.Enqueue(bullet);
    }
}
