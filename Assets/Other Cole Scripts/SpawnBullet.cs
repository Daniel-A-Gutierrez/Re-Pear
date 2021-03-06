﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour, IFireable
{
    public GameObject bullet;

    public void OnFire()
    {
        if (bullet != null) {
            bullet = Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
