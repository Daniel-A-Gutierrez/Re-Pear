using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public LayerMask enemyLayers;
    public int scanInterval = 2;
    int scanTicker = 0;
    public float range = 5f;
    Transform currentTarget = null;
    public float fireRate = 1f;
    float fireInterval;
    // Start is called before the first frame update
    void Start()
    {
        fireInterval = 1f/fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget != null)
        {
            Shoot();
        }
        else 
        {
            if(targets == 0){/*nothing to shoot at */}
            else
            {
                SetTarget();
                if(currentTarget!=null)
                    Shoot();
            }
        }
         
    }

    void Shoot()
    {
        //GameManager.StationFire(position,direction,range)
        
    }

    void FixedUpdate()
    {
        scanTicker++;
        if(scanTicker>=scanInterval)
        {
            scanTicker = 0;
            ScanEnemies(transform.position,range,enemyLayers);
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
        return enemies;
    }


}
