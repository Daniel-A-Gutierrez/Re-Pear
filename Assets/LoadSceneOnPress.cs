using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnPress : MonoBehaviour
{
    public int sceneIndex;

    void Update()
    {
        if (Input.anyKey) {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}
