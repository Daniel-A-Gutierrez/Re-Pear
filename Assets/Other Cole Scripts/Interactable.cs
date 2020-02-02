using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>If this thing isnt in the "useable" layer nothing will happen </summary>
public abstract class Interactable : MonoBehaviour
{
    public virtual void Use(PlayerController player){}
}
