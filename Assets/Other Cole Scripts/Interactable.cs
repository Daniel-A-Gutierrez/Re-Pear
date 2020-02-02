using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>If this thing isnt in the "useable" layer nothing will happen </summary>
public abstract class Interactable : MonoBehaviour
{
    public virtual bool Use(PlayerController player){return false;}
    public virtual void Highlight(PlayerController player){}
    public virtual void UnHighlight(PlayerController player){}
    public bool topickup = true;
    //forgiv me programming gods
    public GameObject tile;
}
