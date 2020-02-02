using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkDebugger : MonoBehaviour
{
    SpriteRenderer s;
    public Color a ;
    public Color b;
    int ticker = 0;
    // Start is called before the first frame update
    void Start()
    {
        s= GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ticker++;
        if(ticker>3)
        {
            s.flipY = false;
            ticker= 0;
        }
        else
        {
            s.flipY = true;
        }
    }
}
