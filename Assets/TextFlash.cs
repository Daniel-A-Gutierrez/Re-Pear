using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class TextFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float speed = 0.1f;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            ((Mathf.Sin(Time.time / speed * 3.14f)) + 1.0f) / 4.0f + 0.5f
        );
    }
}
