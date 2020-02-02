using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject core;

    public GameObject antPrefab;
    public GameObject flyPrefab;
    public GameObject waspPrefab;

    private GameObject[] spawnPoints;
    private GameObject usingSpawnPoint;
    private int[] possibleEncounters = new int[7];

    //these codes refer to the rows to pull from in the encounter double array
    private int[] encounterTable = new int[] 
    {0,0,0,1,1,2,2,2,3,3,4,5,5,5,6,6,7,8,8,8,9,9,10,10,11,
    12,12,12,13,13,14,15,15,15,16,16,17,18,18,18,19,19,20};

    //0-2 in the first column represents the prefabs ant thru wasp
    //the number in the second column represents number OF them
    private int[][] encounters = new int[][]
    {
        new int[2]{2,1},
        new int[2]{1,1},
        new int[2]{0,2},
        new int[2]{1,2},
        new int[2]{2,1},
        new int[2]{0,3},
        new int[2]{1,3},
        new int[2]{2,2},
        new int[2]{0,4},
        new int[2]{1,4},
        new int[2]{2,3},
        new int[2]{0,5},
        new int[2]{1,5},
        new int[2]{2,4},
        new int[2]{0,6},
        new int[2]{1,6},
        new int[2]{2,5},
        new int[2]{0,7},
        new int[2]{1,7},
        new int[2]{2,6},
        new int[2]{0,8}
    };

    // Start is called before the first frame update
    void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        //on a set time interval
       //testing with manual fire, need timer compatability 
       if(Input.GetButtonDown("Fire1"))
        {
            spawnAt(0);
        }
        
    }

    public void spawnAt(int level)
    {
        //pick a spawn point
        int spawnToPick = Random.Range(0, spawnPoints.Length);
        //print("using spawn " + spawnToPick + " of " + (spawnPoints.Length - 1));
        usingSpawnPoint = spawnPoints[spawnToPick];

        //look through the potential encounters
        
        if (encounterTable[6 + level] < encounterTable.Length)
        {
            possibleEncounters[0] = encounterTable[0 + level];
            possibleEncounters[1] = encounterTable[1 + level];
            possibleEncounters[2] = encounterTable[2 + level];
            possibleEncounters[3] = encounterTable[3 + level];
            possibleEncounters[4] = encounterTable[4 + level];
            possibleEncounters[5] = encounterTable[5 + level];
            possibleEncounters[6] = encounterTable[6 + level];
        }

        //36 is the curretn max level allowed in the encounter list
        else
        {
            level = 36;
            possibleEncounters[0] = encounterTable[0 + level];
            possibleEncounters[1] = encounterTable[1 + level];
            possibleEncounters[2] = encounterTable[2 + level];
            possibleEncounters[3] = encounterTable[3 + level];
            possibleEncounters[4] = encounterTable[4 + level];
            possibleEncounters[5] = encounterTable[5 + level];
            possibleEncounters[6] = encounterTable[6 + level];
        }
        
        /*
        for(int i = 0; i < possibleEncounters.Length; i++)
        {
            print(possibleEncounters[i] + " ");
        }
        */
      

        //roll a d100 
        int spawnRoll = Random.Range(0, 99);
        //print("rolled: " + spawnRoll);

        //spawn odds: 25, 20, 20, 10, 10, 10, 5
        if (spawnRoll <= 24)
        {
            spawn(0);
        }

        else if (spawnRoll <= 44)
        {
            spawn(1);
        }

        else if (spawnRoll <= 64)
        {
            spawn(2);
        }

        else if (spawnRoll <= 74)
        {
            spawn(3);
        }

        else if (spawnRoll <= 84)
        {
            spawn(4);
        }

        else if (spawnRoll <= 94)
        {
            spawn(5);
        }

        else if (spawnRoll <= 99)
        {
            spawn(6);
        }

        else
        {
            print("rolled out of bounds for spawn... somehow?");
        }

    }

    private void spawn(int possibleEncounterNum)
    {
        int encounterCode = possibleEncounters[possibleEncounterNum];
        //print("Encounter rolled: " + encounterCode);
        int numToSpawn = encounters[encounterCode][1];     

        //ant spawn
        if (encounters[encounterCode][0] == 0)
        {
            print("Spawning: " + numToSpawn + "ants");
            for (int i = 0; i < numToSpawn; i++)
            {
                GameObject newAnt = Instantiate(antPrefab, usingSpawnPoint.GetComponent<Transform>().position, Quaternion.identity);
                newAnt.GetComponent<Enemy_Ant>().setStationCore(core.GetComponent<Transform>());
                //WaitForSeconds(1);
            }
        }

        //make sure to add fly enemy script over enemy_ant
        else if (encounters[encounterCode][0] == 1)
        {
            print("Spawning: " + numToSpawn + "flies");
            for (int i = 0; i < numToSpawn; i++)
            {
                GameObject newFly = Instantiate(flyPrefab, usingSpawnPoint.GetComponent<Transform>().position, Quaternion.identity);
                newFly.GetComponent<Enemy_Ant>().setStationCore(core.GetComponent<Transform>());
                //WaitForSeconds(1);
            }
        }

        //ant spawn
        else if (encounters[encounterCode][0] == 2)
        {
            print("Spawning: " + numToSpawn + "wasps");
            for (int i = 0; i < numToSpawn; i++)
            {
                GameObject newWasp = Instantiate(waspPrefab, usingSpawnPoint.GetComponent<Transform>().position, Quaternion.identity);
                newWasp.GetComponent<Enemy_Wasp>().setStationCore(core.GetComponent<Transform>());
                //WaitForSeconds(1);
            }
        }

        else
        {
            print("Invalid encounter code in spawn()");
        }
    }

}

