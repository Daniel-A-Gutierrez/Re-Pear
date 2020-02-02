using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    
    public KeyCode key = KeyCode.P;
    public bool paused = false;
    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(key) && !paused) {
            paused = true;
            spriteRenderer.enabled = true;
            Time.timeScale = 0.0f;
        } else if (Input.GetKeyDown(key) && paused) {
            paused = false;
            spriteRenderer.enabled = false;
            Time.timeScale = 1.0f;
        }
    }
}
