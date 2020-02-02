using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : MonoBehaviour
{
    //tile prefabs and count
    public GameObject appleTile;
    public GameObject bananaTile;
    public GameObject melonTile;
    public GameObject orangeTile;
    //turret prefab
    public GameObject turretTile;

    //array and handlers
    private GameObject[] junkSpawns;
    public int tileTypes;
    public int spawnsPerWave;
    public float tileSpawnOffseRadiusMax;

    //intial spawns, have to set manually to the ones you want, sorru uwu
    public int initialSpawn0;
    public int initialSpawn1;
    public int initialSpawn2;
    public int initialSpawn3;
   
    //these handle the spawn times, ranges, etc (it's a little random for tension's sake)
    public float randSpawnTimeRange;
    public float minNextSpawnTime;
    private float nextSpawnTime;
    private float lastSpawnTime;

    // Start is called before the first frame update
    void Awake()
    {
        junkSpawns = GameObject.FindGameObjectsWithTag("JunkSpawn");
        SpawnJunk(initialSpawn0);
        SpawnJunk(initialSpawn1);
        SpawnJunk(initialSpawn2);
        SpawnJunk(initialSpawn3);

        nextSpawnTime = Time.time + minNextSpawnTime + Random.Range(0, randSpawnTimeRange);
        print(Time.time + " :: " + nextSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextSpawnTime)
        {
            for(int i = 0; i < spawnsPerWave; i++)
            {
                int spawnAt = Random.Range(0, junkSpawns.Length - 1);
                SpawnJunk(spawnAt);
            }

            nextSpawnTime = Time.time + minNextSpawnTime + Random.Range(0, randSpawnTimeRange);
            //print(Time.time + " :: " + nextSpawnTime);
        }
    }

    public void SpawnJunk(int spawner)
    {
        //1 in 5 chance to spawn a turret
        int turretRoll = Random.Range(0, 4);
        if (turretRoll == 0)
        {
           //spawn a tile
           GameObject newTile =  Instantiate(turretTile, junkSpawns[spawner].GetComponent<Transform>().position, Quaternion.identity);

          //scootch it around in a semi-random raius
           Vector2 tilePos = newTile.GetComponent<Transform>().position;
           tilePos.x += Random.Range(tileSpawnOffseRadiusMax * -1, tileSpawnOffseRadiusMax);
           tilePos.y += Random.Range(tileSpawnOffseRadiusMax * -1, tileSpawnOffseRadiusMax);
           newTile.GetComponent<Transform>().position = tilePos;

            //rotate that puppy on z axis            
            newTile.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }

        else
        {
            //spawn a tile
            int tileType = Random.Range(0, 4);
            GameObject newTile = new GameObject();
            switch (tileType)
            {
                case 0:
                newTile = Instantiate(appleTile, junkSpawns[spawner].GetComponent<Transform>().position, Quaternion.identity);
                break;

                case 1:
                newTile = Instantiate(bananaTile, junkSpawns[spawner].GetComponent<Transform>().position, Quaternion.identity);
                break;

                case 2:
                newTile = Instantiate(melonTile, junkSpawns[spawner].GetComponent<Transform>().position, Quaternion.identity);
                 break;

                case 3:
                 newTile = Instantiate(orangeTile, junkSpawns[spawner].GetComponent<Transform>().position, Quaternion.identity);
                 break;

                default:
                print("error in tile spawn switch ase. out of bounds rand");
                 break;

            }

            //scootch it around in a semi-random raius
            Vector2 tilePos = newTile.GetComponent<Transform>().position;
            tilePos.x += Random.Range(tileSpawnOffseRadiusMax * -1, tileSpawnOffseRadiusMax);
            tilePos.y += Random.Range(tileSpawnOffseRadiusMax * -1, tileSpawnOffseRadiusMax);
            newTile.GetComponent<Transform>().position = tilePos;

            //rotate that puppy on z axis            
            newTile.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
    }
}
