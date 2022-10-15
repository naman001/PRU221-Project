using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public int map;
    public float[] position;
    public int health;
    public int coins;

    /*public static SaveData Current
    {
        get
        {
            if (_current == null)
                _current = new SaveData();
            return _current;
        }
    }*/

    public SaveData(Character character)
    {
        map = character.map;
        coins = Character.CoinNums;
        health = character.health;
        position = new float[3];
        position[0] = Character.lastCheckPointPos.x;
        position[1] = Character.lastCheckPointPos.y;
        position[2] = Character.lastCheckPointPos.z;
    }
}
