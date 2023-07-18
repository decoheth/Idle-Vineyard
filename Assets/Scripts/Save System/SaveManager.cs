using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveManager: MonoBehaviour
{

    string path = "";

    public GameObject GM;
    

    void Awake()
    {
        SetPaths();
    }

    public SaveData GetData ()
    {
        double gold = GM.GetComponent<GameManager>().gold;
        double rate = GM.GetComponent<ClickManager>().autoRate;
        double goldPerClick = GM.GetComponent<ClickManager>().goldPerClick;;

        SaveData data = new SaveData(gold, rate, goldPerClick);

        return data;
    }

    // Reset all saved data. For testing purposes.
    public void ClearData ()
    {
        File.Delete(path);

        return;
    }

    
    private void SetPaths()
    {
        path = Application.persistentDataPath + "/savaData.json";
    }


    public void SaveGame ()
    {
        SaveData saveData = GetData();

        Debug.Log("Saving data at " + path);
        string json = JsonUtility.ToJson(saveData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
    }

    public SaveData LoadGame ()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        SaveData data = JsonUtility.FromJson<SaveData>(json);
        
        return data;
    }
}

