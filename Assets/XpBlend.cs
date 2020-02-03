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
        float levelXp = ((float)levelManager.getExp());
        levelXp -= levelManager.getLevel() * 4.0f;
        spriteRenderer.material.SetFloat("_Blend", (1.0f - (levelXp / 4.0f)));   
    }
}
