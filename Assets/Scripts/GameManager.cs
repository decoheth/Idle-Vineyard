using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game")]
    
    public double gold;
    public string version;

    public TextMeshProUGUI goldText;

    public static GameManager instance;

    [Header("Managers")]
    // Upgrade Manager
    public GameObject UM;
    // Save Manager
    public GameObject SM;

    

    //Audio Settings
    [Header("Settings: Audio")]
    [Range(0.0f, 1.0f)]
    public float masterVolume;
    [Range(0.0f, 1.0f)]
    public float setFXVolume;
    [Range(0.0f, 1.0f)]
    public float setMusicVolume;
    public float fxVolume;
    public float musicVolume;


    void Start() 
    {
        // Load saved data
        SaveData data = SM.GetComponent<SaveManager>().LoadGame();
        // Set initial gold (from saved value)
        gold = data.savedGold;
        Debug.Log("Saved Gold:"+ gold);

        // Set audio levels
        fxVolume = setFXVolume * masterVolume;
        musicVolume = setMusicVolume * masterVolume;



    }

    void Awake() 
    {
        instance = this;
    }

    public void AddGold(double amount) 
    {
        gold += amount;
        goldText.text = ConvertNum(gold);

        // Call upgrade menu function to check if item is purchaseable
        UM.GetComponent<UpgradeManager>().CheckPurchaseable();
    }

    public void RemoveGold(double amount) 
    {
        gold -= amount;
        goldText.text = ConvertNum(gold);

        // Call upgrade menu function to check if item is purchaseable
        UM.GetComponent<UpgradeManager>().CheckPurchaseable();
    }

    
    public string ConvertNum(double amount)
    {
        string value;


        if (amount >= 1000000000000000000)
            value = (amount / 1000000000000000000).ToString("F1") + "Qi";  // Quintillion    
        else if (amount >= 1000000000000000)
            value = (amount / 1000000000000000).ToString("F1") + "Qa";  // Quadrillion
        else if (amount >= 1000000000000)
            value = (amount / 1000000000000).ToString("F1") + "T";  // Trillion
        else if (amount >= 1000000000)
            value = (amount / 1000000000).ToString("F1") + "B";  // Billion
        else if (amount >= 1000000)
            value = (amount / 1000000).ToString("F2") + "M";  // Million
        else if (amount >= 1000)
            value = (amount / 1000).ToString("F2") + "K"; // Thousand
        else
            value = amount.ToString("F2");


        return value;
    }

    public void SaveSettings()
    {
        fxVolume = setFXVolume * masterVolume;
        musicVolume = setMusicVolume * masterVolume;
    }

}
