using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePickup : Interactable
{
    public Sprite defaultSprite;
    public Sprite hilightSprite;
    
    public override void Highlight(PlayerController player){
        GetComponent<SpriteRenderer>().sprite = hilightSprite;
    }
    public override void UnHighlight(PlayerController player){
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }

    public override void Use(PlayerController player)
    {
        player.Pickup(gameObject);
    }
}
