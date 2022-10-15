using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    public static void SaveCharacter(Character character)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/character.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(character);
        Debug.Log("-------------------------------------");
        Debug.Log("Character heath: " + data.health);
        Debug.Log("Coins number: " + data.coins);
        Debug.Log("Character X Postition: " + data.position[0]);
        Debug.Log("Character Y Postition: " + data.position[1]);
        Debug.Log("Character Z Postition: " + data.position[2]);
        Debug.Log("We r on map: " + data.map);
        Debug.Log("-------------------------------------");
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/character.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            Debug.Log("-------------------------------------");
            Debug.Log("Character heath: "+ data.health);
            Debug.Log("");
            Debug.Log("Character X Postition: " + data.position[0]);
            Debug.Log("Character Y Postition: " + data.position[1]);
            Debug.Log("Character Z Postition: " + data.position[2]);
            Debug.Log("");
            Debug.Log("We r on map: " + data.map);
            Debug.Log("-------------------------------------");
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogErrorFormat("File(s) not Found at {0}", path);
            return null;
        }
    }
}
