﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Collider2D selecting;
    void OnTriggerEnter2D(Collider2D C)
    {
        selecting = C;
    }

    void OnTriggerExit2D(Collider2D C)
    {
        selecting = null;
    }
}
