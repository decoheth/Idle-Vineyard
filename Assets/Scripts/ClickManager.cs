using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickManager : MonoBehaviour
{
    public double autoRate;
    public TextMeshProUGUI goldRateText;
    public double goldPerClick;



    [Header("Managers")]
    // Save Manager
    public GameObject SM;

    [Header("Click Effects")]
    private Vector3 position;
    public ParticleSystem clickVFX;
    public AudioClip clickAudioFX;
    AudioSource audioSource;

    [Header("AFK Idle")]
    public double afkModifier;
    public DateTime currentTime;
    public DateTime savedExitTime;
    private double afkTime;


    public static ClickManager instance;
    
    void Start()
    {
        // Load saved data
        SaveData data = SM.GetComponent<SaveManager>().LoadGame();
        // Load saved rate
        autoRate = data.savedRate;



        // AFK gold
        //AfkGold(data.savedExitTime);


        // Auto clicker - Call click function every 1 second
        InvokeRepeating("AutoClick", 1.0f, 1.0f);
    }


    void Update()
    {
        if (autoRate == 0)
            goldRateText.text = "";
        else
            goldRateText.text = GameManager.instance.ConvertNum(autoRate) + " /sec";

    }







    void AutoClick()
    {
        GameManager.instance.AddGold(autoRate);
    }

    // Not used currently
    public void Click()
    {
        GameManager.instance.AddGold(goldPerClick);



    }

    public void IncreaseAutoRateMod(double rate)
    {
        autoRate *= rate;
    }

    public void IncreaseGoldPerClickMod(double rate)
    {
        goldPerClick *= rate;
    }

    public void IncreaseAutoRateBonus(double bonus)
    {
        autoRate += bonus;
    }

    public void IncreaseGoldPerClickBonus(double bonus)
    {
        goldPerClick += bonus;
    }
    
    void AfkGold (DateTime savedTime)
    {
        // afkModifier = data.savedAfkModifier
        currentTime = System.DateTime.Now;
        savedExitTime = savedTime;
        afkTime = (savedExitTime - currentTime).TotalMinutes;
        Debug.Log("AFK minutes: " + afkTime);
        // multiply time afk by modifier
    }

}

