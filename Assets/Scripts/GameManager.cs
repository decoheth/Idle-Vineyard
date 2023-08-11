using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay")]

    // Other    
    public double gold;
    public double autoRate;
    public double valueBottle;

    // Grapes
    public int currentGrapes;
    public int thresholdGrapes;
    private int increaseGrapes;
    // Barrel
    public float timeBarrel;
    private float timeLeftBarrel;
    private bool isTimingBarrel = false;
    // Bottle
    private bool collectBottle = false;


    [Header("Objects")]

    public GameObject grapesSprite;
    public GameObject barrelSprite;
    public GameObject bottleSprite;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI goldRateText;

    public static GameManager instance;

    [Header("Managers")]
    // Upgrade Manager
    public GameObject UM;
    // Save Manager
    public GameObject SM;
    // UI Manager
    public GameObject UIM;

    [Header("Click Effects")]

    public ParticleSystem grapeClickVFX;
    public AudioClip grapeClickAudioFX;
    private Vector3 position;


    [Header("Settings: Audio")]
    [Range(0.0f, 1.0f)]
    public float masterVolume;
    [Range(0.0f, 1.0f)]
    public float setFXVolume;
    [Range(0.0f, 1.0f)]
    public float setMusicVolume;
    public float fxVolume;
    public float musicVolume;
    AudioSource audioSource;

    [Header("Stats")]
    public double totalGold;
    public double totalVintageGold;
    public double highestGold;
    public double playtime;
    public int vintageCount;

    [Header("AFK Idle")]
    public double afkModifier;
    private DateTime currentTime;
    private double afkTime;
    double afkIncome;



    void Start() 
    {
        // Load saved data
        SaveData data = SM.GetComponent<SaveManager>().LoadGame();
        // Set saved values
        gold = data.savedGold;
        autoRate = data.savedRate;
        valueBottle = data.savedValueBottle;
        afkModifier = data.savedAfkModifier;


        // Set audio levels
        fxVolume = setFXVolume * masterVolume;
        musicVolume = setMusicVolume * masterVolume;




        // AFK gold
        DateTime savedExitTime = JsonUtility.FromJson<JsonDateTime>(data.savedExitTime);
        AfkGold(savedExitTime);



        // Auto clicker - Call click function every 1 second
        InvokeRepeating("AutoClick", 1.0f, 1.0f);

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

        // Auto text management
        if (autoRate == 0)
            goldRateText.text = "";
        else
            goldRateText.text = GameManager.instance.ConvertNum(autoRate) + " /sec";
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



    void AutoClick()
    {
        AddGold(autoRate);
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

    public void IncreaseAutoRateMod(double rate)
    {
        autoRate *= rate;
    }

    public void IncreaseAutoRateBonus(double bonus)
    {
        autoRate += bonus;
    }

    public void IncreaseBottleValue(double bonus)
    {
        valueBottle += bonus;
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

    void AfkGold (DateTime savedTime)
    {
        // afkModifier = data.savedAfkModifier
        currentTime = System.DateTime.Now;

        afkTime = (currentTime - savedTime).TotalMinutes;
        Debug.Log("AFK minutes: " + afkTime);
        // If afktime is above afk threshold (3 mins)
        if(afkTime > 0.3)
        {
            // Cap time at 72 hours (4320 mins)
            if(afkTime >= 4320)
            {
                afkTime = 4320;
            }
            // multiply time afk by modifier and add that much gold
            afkIncome = (afkTime * (autoRate * 60));
            afkIncome *= afkModifier;        
        }
        // display on ui popup
        UIM.GetComponent<UIManager>().AfkPopup(afkIncome, afkTime);
    }

    public void ClaimAfkGold()
    {
        gold += afkIncome;
        goldText.text = ConvertNum(gold);
        Debug.Log("Income claimed: " + afkIncome);
        UIM.GetComponent<UIManager>().ClosePopup();
    }
    public void ClaimBoostedAfkGold()
    {
        gold += (afkIncome*2);
        goldText.text = ConvertNum(gold);
        Debug.Log("Boosted income claimed: " + afkIncome*2);
        UIM.GetComponent<UIManager>().ClosePopup();
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
