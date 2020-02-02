using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumber : MonoBehaviour
{

    Text text;
    LevelManager levelManager;

    void Start()
    {
        text = GetComponent<Text>();
        levelManager = GameManager.instance.managers.GetComponent<LevelManager>();
    }

    void Update()
    {
        int level = levelManager.getLevel(); 
        text.text = level.ToString();
    }
}
