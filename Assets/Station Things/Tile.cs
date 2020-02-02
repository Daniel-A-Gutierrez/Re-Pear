using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Tile : MonoBehaviour
{
    public int hp;
    public int halfHp = 1;
    public GameObject emplacement;
    public string damagingTag;

    public Sprite defaultSprite;
    public Sprite damagedSprite;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        if (defaultSprite != null) {
            spriteRenderer.sprite = defaultSprite;
        }
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

        animator.SetTrigger("OnHit");
        if (hp <= halfHp) {
            if (damagedSprite != null) {
                spriteRenderer.sprite = damagedSprite;
            }
        }

        if(hp<=0)
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D c2d)
    {
        if(c2d.gameObject.layer == LayerMask.NameToLayer("Ant")
            ||c2d.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet")
            ||c2d.gameObject.layer == LayerMask.NameToLayer("Fly")  )
        {
            Damage(1);
            if(c2d.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet"))
            {
                c2d.gameObject.GetComponent<EnemyBullet>().Terminate();
            }
        }

        if (c2d.gameObject.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            print("player bullet collision");
            c2d.gameObject.GetComponent<PlayerBullet>().Terminate();
        }
    }
}
