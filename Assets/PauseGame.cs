using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    
    public KeyCode key = KeyCode.P;
    public bool paused = false;
    public SpriteRenderer[] spriteRenderers;

    void Start() {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        foreach (SpriteRenderer renderer in spriteRenderers) {
            renderer.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(key) && !paused) {
            paused = true;
            foreach (SpriteRenderer renderer in spriteRenderers) {
                renderer.enabled = true;
            }
            Time.timeScale = 0.0f;
        } else if (Input.GetKeyDown(key) && paused) {
            paused = false;
            foreach (SpriteRenderer renderer in spriteRenderers) {
                renderer.enabled = false;
            }
            Time.timeScale = 1.0f;
        }
    }
}
