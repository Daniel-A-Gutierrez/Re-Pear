using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject stationBullet;
    Queue<GameObject> stationBullets;
    GameManager instance ;

    void Awake()
    {
        instance = this;
        stationBullets = new Queue<GameObject>();
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
            GameObject bullet = Instantiate(stationBullet,position,Quaternion.identity );
            bullet.transform.up = direction.normalized;

        }
    }

    void TerminateStationBullet(GameObject bullet)
    {
        
    }
}
