using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    
    public KeyCode key = KeyCode.P;
    public bool paused = false;
    public SpriteRenderer[] spriteRenderers;

    void Start() {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
            spriteRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(key) && !paused) {
            paused = true;
            foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
                spriteRenderer.enabled = true;
            }
            Time.timeScale = 0.0f;
        } else if (Input.GetKeyDown(key) && paused) {
            paused = false;
            foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
                spriteRenderer.enabled = false;
            }
            Time.timeScale = 1.0f;
        }
    }
}
