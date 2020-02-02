using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public LayerMask enemyLayers;
    public int scanInterval = 2;
    int scanTicker = 0;
    public float bulletLife = 3f;
    Transform currentTarget = null;
    public float fireRate = 1f;
    float fireInterval;
    float lastShotTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        fireInterval = 1f/fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastShotTime > fireInterval )
        {
            SetTarget();
            if(currentTarget != null)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        transform.up = (currentTarget.position-transform.position).normalized;
        lastShotTime = Time.time;
        GameManager.instance.StationShoot( (transform.position +
             (currentTarget.position-transform.position).normalized * .25f ),
             (currentTarget.position-transform.position).normalized,
             bulletLife);
    }

    void FixedUpdate()
    {
        scanTicker++;
        if(scanTicker>=scanInterval)
        {
            scanTicker = 0;
            ScanEnemies(transform.position,bulletLife,enemyLayers);
            SetTarget();

        }
    }

    void SetTarget()
    {
        float closest = float.PositiveInfinity;
        for(int i = 0; i < targets ; i++)
        {
            if(enemies[i] == null)
                continue;
            float d = Vector3.Distance(enemies[i].transform.position,transform.position) ;
            if( d < closest )
            {
                closest = d;
                currentTarget = enemies[i].transform;
            }
        }
        if(closest == float.PositiveInfinity)
        {
            currentTarget = null;
        }
    }

    Collider2D[] enemies;
    int targets = 0;
    Collider2D[] ScanEnemies( Vector2 center, float radius, LayerMask checkLayers)
    {
        if(enemies == null)
            enemies = new Collider2D[48];
        // ContactFilter2D c = new ContactFilter2D();
        // c.layerMask = checkLayers;
        // c.useLayerMask = true;
        targets = Physics2D.OverlapCircleNonAlloc(center ,radius ,enemies,checkLayers) ;
        for(int i = targets;i<enemies.Length;i++)
        {
            enemies[i] = null;
        }
        return enemies;
    }


}
