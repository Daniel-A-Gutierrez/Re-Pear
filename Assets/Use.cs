using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use : MonoBehaviour
{
    PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = transform.parent.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///<summary> selecting must be something in the interactable layer, and all things in the interactable layer must extend interactable </summary>
    public Collider2D selecting;
    void OnTriggerEnter2D(Collider2D C)
    {
        selecting = C;
        C.gameObject.GetComponent<Interactable>().Highlight(pc);//if error see above comment
    }

    void OnTriggerExit2D(Collider2D C)
    {
        selecting = null;
        C.gameObject.GetComponent<Interactable>().UnHighlight(pc);//if error see above comment

    }
}
