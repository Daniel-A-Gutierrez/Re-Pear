using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : Bullet
{   
    Animator animator;
    Rigidbody2D rigidbody;
    private bool terminating = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = direction * speed;
    }

    void Update()
    {
        if(Time.time-birthTime > lifetime)
            Terminate();
    }

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = direction*speed;
        animator.SetTrigger("OnAwake");
    }

    public override void Terminate()
    {
        if (terminating == false){
            terminating = true;
            animator.SetTrigger("OnImpact");
            rigidbody.velocity = Vector2.zero;
            StartCoroutine(TerminateAfterSeconds());
        }
    }

    IEnumerator TerminateAfterSeconds()
    {
        yield return new WaitForSeconds(1);
        terminating = false;
        GameManager.instance.TerminateEnemyBullet(gameObject);
    }
}