using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePickup : Interactable
{
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use(PlayerController player)
    {
        player.Pickup(gameObject);
    }
}
