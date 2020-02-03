using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//note: every level comes with 4exp.
//1 armor is worth 1 exp
//1 turret is worth 4 exp

public class LevelManager : MonoBehaviour
{
    private int exp;
    private int currentLevel;
    public int difficultyPerMinute = 3;
    // Start is called before the first frame update
    void Start()
    {
        exp = 0;
        currentLevel = 0;
    }

    float lastIncreaseTime;
    // Update is called once per frame
    void Update()
    {
        if(Time.time>lastIncreaseTime)
        {
            lastIncreaseTime=Time.time;
            lastIncreaseTime += 60/difficultyPerMinute;
            addPart("armor");
        }
    }

    //===used when a part is added to the station, which is the only reason to increment exp
    public void addPart(string partName)
    {
        print("added exp");
        switch (partName)
        {
            case "armor":
                exp += 1;
                print("it's armor" + exp);
                break;

            case "turret":
                exp += 4;
                break;

            default:
                print("Unexpected part added  in 'addedPart' method in levelManager");
                break;
        }

        currentLevel = exp / 4;
    }

    void subtractPart(string partName)
    {
        switch (partName)
        {
            case "armor":
                //exp -= 1;
                break;

            case "turret":
                //exp -= 4;
                break;

            default:
                print("Unexpected part added  in 'addedPart' method in levelManager");
                break;
        }

        currentLevel = exp / 4;
    }

    //===getters
    public int getLevel()
    {
        return currentLevel;
    }

    public int getExp()
    {
        return exp;
    }

    //use this for ui to show currentExp/Exp for next level
    public int[] getExpPerLevel()
    {
        return new int[] {exp, (currentLevel + 1) * 4};
    }


}
