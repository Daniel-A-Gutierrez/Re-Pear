using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = direction*speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-birthTime > lifetime)
            Terminate();
    }

    public override void Terminate()
    {
        GameManager.instance.TerminatePlayerBullet(gameObject);
    }

}
