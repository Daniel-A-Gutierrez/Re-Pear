using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpBlend : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    LevelManager levelManager;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        levelManager = GameManager.instance.managers.GetComponent<LevelManager>();
    }

    void Update()
    {
        spriteRenderer.material.SetFloat("_Blend", 1 - (levelManager.getExp() / 4.0f));   
    }
}
