using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBullet : Bullet
{   
    Animator animator;
    Rigidbody2D rb;
    private bool terminating = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }

    void Update()
    {
        //rb.velocity = direction * speed;
        if(Time.time-birthTime > lifetime)
            Terminate();
    }

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction*speed;
        animator.SetTrigger("OnAwake");
    }

    public override void Terminate()
    {
        if (terminating == false){
            terminating = true;
            animator.SetTrigger("OnImpact");
            rb.velocity = Vector2.zero;
            StartCoroutine(TerminateAfterSeconds());
        }
    }

    IEnumerator TerminateAfterSeconds()
    {
        yield return new WaitForSeconds(1);
        terminating = false;
        GameManager.instance.TerminatePlayerBullet(gameObject);
    }
}