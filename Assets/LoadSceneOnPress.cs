using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnPress : MonoBehaviour
{
    public int sceneIndex;
    public AudioSource sound; //FIXME: sound sometimes plays correctly, most times doesn't

    void Update()
    {
        if (Input.anyKey) {
            wait();
            sound.Play();
            wait();
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(3.0f); 
    }
}
