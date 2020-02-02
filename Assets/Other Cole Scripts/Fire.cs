using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private List<IFireable> fireables = new List<IFireable>();

    void Start() {
        
        foreach (IFireable component in gameObject.GetComponentsInChildren<IFireable>()) {
            fireables.Add(component);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space")) {
            foreach (IFireable component in fireables) {
                component.OnFire();
            }
        }
    }
}
