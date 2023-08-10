using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        
        var exitTime = JsonUtility.ToJson((JsonDateTime) System.DateTime.Now);

        SaveData data = new SaveData(gold,rate,goldPerClick,genLevel, stock, exitTime);

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
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
    }

    public void NewSaveGame ()
    {
        // Create blank save data file

        int[] genLevel = new int[7] {0,0,0,0,0,0,0};
        
        var exitTime = JsonUtility.ToJson((JsonDateTime) System.DateTime.Now);

        SaveData saveData = new SaveData(0d,0d,0d,genLevel, 0d, exitTime);

        //Debug.Log("Saving data at " + path);
        string json = JsonUtility.ToJson(saveData);
        //Debug.Log(json);

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
            return DateTime.FromFileTimeUtc(jdt.value);
        }
        public static implicit operator JsonDateTime(DateTime dt) {
            JsonDateTime jdt = new JsonDateTime();
            jdt.value = dt.ToFileTimeUtc();
            return jdt;
        }
    }


}

