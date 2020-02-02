using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnButtonPress : MonoBehaviour
{
    public int sceneIndex;
    public KeyCode key = KeyCode.Return;

    void Update()
    {
        if (Input.GetKeyDown(key)) {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}
