using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;


public class SaveManager: MonoBehaviour
{

    string path = "";

    [Header("Managers")]
    public GameObject GM;
    public GameObject CM;
    public GameObject UM;
    

    void Awake()
    {
        SetPaths();
    }

    public SaveData GetData ()
    {
        double gold = GM.GetComponent<GameManager>().gold;
        double rate = GM.GetComponent<ClickManager>().autoRate;
        double goldPerClick = GM.GetComponent<ClickManager>().goldPerClick;
        // Get generator levels
        int[] genLevel = new int[7] {4,1,1,1,1,0,0};
        //int[] genLevel = UM.GetComponent<UpgradeManager>().generatorSO.count;
        //Debug.Log(UM.GetComponent<UpgradeManager>().generatorPanels);

        double stock = CM.GetComponent<CaskManager>().stock;
        
        DateTime exitTime = System.DateTime.Now;

        SaveData data = new SaveData(gold,rate,goldPerClick,genLevel, stock, exitTime);

        return data;
    }

    // Reset all saved data. For testing purposes.

    
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

