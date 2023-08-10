using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay")]

    // Grapes
    public int currentGrapes;
    public int thresholdGrapes;
    public int increaseGrapes;
    // Barrel
    public float timeBarrel;
    private float timeLeftBarrel;
    private bool isTimingBarrel = false;
    // Bottle
    private bool collectBottle = false;
    public double valueBottle;

    // Other    
    public double gold;

    [Header("Objects")]

    public GameObject grapesSprite;
    public GameObject barrelSprite;
    public GameObject bottleSprite;

    public TextMeshProUGUI goldText;
    public Button clickField;

    public static GameManager instance;

    [Header("Managers")]
    // Upgrade Manager
    public GameObject UM;
    // Save Manager
    public GameObject SM;

    [Header("Click Effects")]
    private Vector3 position;
    public ParticleSystem grapeClickVFX;
    public AudioClip grapeClickAudioFX;


    [Header("Settings: Audio")]
    AudioSource audioSource;
    [Range(0.0f, 1.0f)]
    public float masterVolume;
    [Range(0.0f, 1.0f)]
    public float setFXVolume;
    [Range(0.0f, 1.0f)]
    public float setMusicVolume;
    public float fxVolume;
    public float musicVolume;

    [Header("Stats")]
    public double totalGold;
    public double totalVintageGold;
    public double highestGold;
    public double playtime;
    public int vintageCount;



    void Start() 
    {
        // Load saved data
        SaveData data = SM.GetComponent<SaveManager>().LoadGame();
        // Set initial gold (from saved value)
        gold = data.savedGold;

        // Set audio levels
        fxVolume = setFXVolume * masterVolume;
        musicVolume = setMusicVolume * masterVolume;

        // Set barrel timer
        timeLeftBarrel = timeBarrel;


        // Position used for the touch effects
        position = new Vector3(0, 0, 0); 
        // Load audio source
        audioSource = GetComponent<AudioSource>();
    }

    void Awake() 
    {
        instance = this;
    }

    void Update()
    {
        // Barrel aging
        if (isTimingBarrel == true)
        {
            if (timeLeftBarrel > 0)
            {
                // Play animation of barrel fermenting
                timeLeftBarrel -= Time.deltaTime;
            }
            else
            {
                collectBottle = true;
            }
        }
    }

    public void Core ()
    {
        // add +1 to grapes until it reaches limit
        if (currentGrapes < thresholdGrapes)
        {
            // Click particle
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grapeClickVFX.transform.position = new Vector3(position.x,position.y, -5f);
            grapeClickVFX.Emit(1);
            //Play audio effect on click
            audioSource.PlayOneShot(grapeClickAudioFX, fxVolume);

            currentGrapes += increaseGrapes;
            return;
        }
        else
        {
            Debug.Log("Grapes Full");
            isTimingBarrel = true;
        }
        
        // Tap again to pour bottle of wine and collect
        if (collectBottle == true)
        {
            // Play bottle pouring and corking animation
            Debug.Log("Bottle Corked!");
            // Add gold
            // Play gold coins animation
            AddGold(valueBottle);

            // Reset loop
            currentGrapes = 0;
            isTimingBarrel = false;
            timeLeftBarrel = timeBarrel;
            collectBottle = false;
        }
        
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
