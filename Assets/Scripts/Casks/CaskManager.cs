using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CaskManager : MonoBehaviour
{
   // Game Manager object

    public CaskSO[] caskSO;
    public CaskTemplate[] caskPanel;
    public GameObject[] caskGO;
    public Slider[] caskSlider;

    public static CaskManager instance;

    // Start is called before the first frame update
    void Start()
    {
        // Enable unlocked casks
        for (int i = 0; i < caskSO.Length; i++)
        {
           caskGO[i].SetActive(true); 
        }

        // Load panels with Scriptable Object data
        LoadPanels();

        // Start cask timers

    }


    //void SellCasks(caskId, int amount)
    

    public void LoadPanels()
    {
        for (int i = 0; i < caskSO.Length; i++)
        {
            caskPanel[i].titleText.text = caskSO[i].title;
            caskPanel[i].caskIcon.sprite = caskSO[i].image;
            caskPanel[i].rate = caskSO[i].time;
            caskPanel[i].index = i;
            //caskPanel[i].auto = caskSO[i].auto;
        }
    }
}
