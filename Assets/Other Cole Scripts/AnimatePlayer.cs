using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatePlayer : MonoBehaviour, IFireable
{
    Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();   
    }

    public void OnFire()
    {
        animator.SetTrigger("OnFire");
    }
}
