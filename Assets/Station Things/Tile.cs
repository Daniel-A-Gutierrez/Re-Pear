using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int hp;
    public GameObject emplacement;
    public string damagingTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///<summary>put something (give it a prefab) on the tile (like a gun) </summary>
    void Emplace(GameObject prefab)
    {
        Instantiate(prefab,transform.position,Quaternion.identity,transform);
    }

    void Die()
    {
        //do more stuff later
        Destroy(gameObject);
    }

    //enemy should call this, not this component
    void Damage(int amount)
    {
        hp -= amount;
        if(hp<=0)
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D c2d)
    {
        
    }
}
