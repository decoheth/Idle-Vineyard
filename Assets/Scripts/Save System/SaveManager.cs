using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveManager: MonoBehaviour
{

    string path = "";

    [Header("Default Values")]
    public double defaultValueBottle;
    public double defaultAfkModifier;

    [Header("Managers")]
    public GameObject GM;
    public GameObject CM;
    public GameObject UM;
    public GameObject GenM;
    

    void Awake()
    {
        SetPaths();
    }

    public SaveData GetData ()
    {
        double gold = GM.GetComponent<GameManager>().gold;
        double rate = GM.GetComponent<GameManager>().autoRate;
        double valueBottle = GM.GetComponent<GameManager>().valueBottle;
        double stock = CM.GetComponent<CaskManager>().stock;
        var exitTime = JsonUtility.ToJson((JsonDateTime) System.DateTime.Now);
        double afkModifier = GM.GetComponent<GameManager>().afkModifier;

        // Get generator levels
        int[] genLevel = new int[GenM.GetComponent<GeneratorManager>().generatorSO.Length];
        for (int i = 0; i < GenM.GetComponent<GeneratorManager>().generatorSO.Length; i++)
        {
            genLevel[i] = GenM.GetComponent<GeneratorManager>().generatorPanels[i].countVal;
        }


        SaveData data = new SaveData(gold,rate,genLevel, stock, exitTime, valueBottle, afkModifier);

        return data;
    }
    
    private void SetPaths()
    {
        path = Application.persistentDataPath + "/savaData.json";
    }


    public void SaveGame ()
    {
        SaveData saveData = GetData();

        //Debug.Log("Saving data at " + path);
        string json = JsonUtility.ToJson(saveData);
        //Debug.Log(json);

        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
    }

    public void NewSaveGame ()
    {
        // Create blank save data file

        // Generator levels
        int[] genLevel = new int[GenM.GetComponent<GeneratorManager>().generatorSO.Length];
        for (int i = 0; i < GenM.GetComponent<GeneratorManager>().generatorSO.Length; i++)
        {
            genLevel[i] = 0;
        }
        
        var exitTime = JsonUtility.ToJson((JsonDateTime) System.DateTime.Now);

        SaveData saveData = new SaveData(0d,0d,genLevel, 0d, exitTime,defaultValueBottle, defaultAfkModifier);

        string json = JsonUtility.ToJson(saveData);

        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
    }

    public SaveData LoadGame ()
    {
        SaveData data;
        // Check if data exists
        if (System.IO.File.Exists(path))
        {
            //Debug.Log("Save file found");
            // Load existing data
            using StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();

            data = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.Log("No save data file found");
            // Create new data
            NewSaveGame();
            Debug.Log("New save data file created");
            // Load new data
            using StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();

            data = JsonUtility.FromJson<SaveData>(json);
        }

        return data;
    }

    public void ResetData()
    {
        // Create a blank data file
        NewSaveGame();
        // Reload app
        SceneManager.LoadScene(0);

    }

    private void OnApplicationQuit()
    {
        // Save data on app quit
        SaveGame();
    }

    private void OnApplicationFocus(bool focus)
    {
        // Save data when focus is false
        if(focus == false)
            SaveGame();
    }

    // Serialize DateTime
    [Serializable]
    struct JsonDateTime {
        public long value;
        public static implicit operator DateTime(JsonDateTime jdt) {
            return DateTime.FromFileTime(jdt.value);
        }
        public static implicit operator JsonDateTime(DateTime dt) {
            JsonDateTime jdt = new JsonDateTime();
            jdt.value = dt.ToFileTime();
            return jdt;
        }
    }


}

