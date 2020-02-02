using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingerLogic : MonoBehaviour
{
    public GameObject stinger;

    public float decayTime;
    private float timeToDecayOn;

    // Start is called before the first frame update
    void Start()
    {
        timeToDecayOn = Time.time + decayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timeToDecayOn)
        {
            Destroy(stinger);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject toDestroy = other.gameObject;
        if (other.tag == "StationArmor")
        {
            //Destroy(toDestroy);
        }
        
    }
}
