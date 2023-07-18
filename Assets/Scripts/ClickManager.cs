using System.Collections;
using System.Collections.Generic;
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

    public static ClickManager instance;
    
    void Start()
    {
        // Load saved rate
        SaveData data = SM.GetComponent<SaveManager>().LoadGame();
        autoRate = data.savedRate;

        // Position used for the touch effects
        position = new Vector3(0, 0, 0); 

        audioSource = GetComponent<AudioSource>();

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

     public void Click()
    {
        GameManager.instance.AddGold(goldPerClick);

        // Get position of click
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Emit particles at position
        clickVFX.transform.position = new Vector3(position.x,position.y, -5f);
        clickVFX.Emit(1);
        
        //Play audio effect on click
        audioSource.PlayOneShot(clickAudioFX, GameManager.instance.fxVolume);

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
    

}

