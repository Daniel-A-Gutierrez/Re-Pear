using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private List<IFireable> fireables = new List<IFireable>();
    public AudioSource sound;
    void Start() {
        
        foreach (IFireable component in gameObject.GetComponentsInChildren<IFireable>()) {
            fireables.Add(component);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space")) {
            sound.Play();
            foreach (IFireable component in fireables) {
                component.OnFire();
            }
        }
    }
}
