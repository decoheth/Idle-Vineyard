using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CaskManager : MonoBehaviour
{
    [Header("Managers")]
    public GameObject SM;
    
    [Header("Casks")]
    public CaskSO[] caskSO;
    [SerializeField] GameObject caskPrefab;
    [SerializeField] GameObject parentCanvasObjCask;

    private List<Button> caskCollectBtns = new List<Button>();
    [HideInInspector]
    public List<CaskTemplate> caskPanel = new List<CaskTemplate>();
    private List<GameObject> caskGO = new List<GameObject>();

    public static CaskManager instance;
    private DateTime sysDateTime;
    public TMP_Text stockDisplay;
    public TMP_Text stockText1;

    [Header("Sell Casks")]
    public double stock;
    public double caskValue;
    public TMP_Text stockText2;
    public List<TMP_Text> sellText = new List<TMP_Text>();


    

    // Start is called before the first frame update
    void Start()
    {
        sysDateTime = System.DateTime.Now;

        // Set the value of a cask from saved data
        SaveData data = SM.GetComponent<SaveManager>().LoadGame();
        stock = data.savedStock;

        for (int i = 0; i < caskSO.Length; i++)
        {
            // Instantiate from prefab
            var cask = Instantiate (caskPrefab, transform.position , Quaternion.identity);
            // Set menu panel as parent
            cask.transform.SetParent(parentCanvasObjCask.transform, false);
        


            // Disable all buttons
            cask.GetComponent<Button>().interactable = false;
            int tmp = i;
            cask.GetComponent<Button>().onClick.AddListener(() => CollectCask(tmp));
            // Add to Button list
            caskCollectBtns.Add(cask.GetComponent<Button>() as Button);
            // Add to GameObject list
            caskGO.Add(cask as GameObject);
            // Add to Template list
            caskPanel.Add(cask.GetComponent<CaskTemplate>() as CaskTemplate);

            //Enable all panels
            cask.SetActive(true);
        }

        // Load panels with Scriptable Object data
        LoadPanels();

        // Load sell cask panels with dynamic text;
        SellPanels();

    }

    public void Update()
    {
        sysDateTime = System.DateTime.Now;

        // Set stock text
        // If below 1000 (To prevent decimal)
        if (stock < 1000)
        {
            stockText1.text = "Casks in Stock: " + stock;
            stockText2.text = "Casks in Stock: " + stock;
            stockDisplay.text = stock.ToString();
        }
        else  
        {
            stockText1.text = "Casks in Stock: " + GameManager.instance.ConvertNum(stock);
            stockText2.text = "Casks in Stock: " + GameManager.instance.ConvertNum(stock);
            stockDisplay.text = GameManager.instance.ConvertNum(stock).ToString();
        }  



        // Cask Timers
        for (int i = 0; i < caskSO.Length; i++)
        {
            // If progress value is less than 1
            if(caskPanel[i].progress < 1)
            {
                // Compare start time with current time
                TimeSpan diffTime = sysDateTime.Subtract(caskPanel[i].startTime);
                double diffTimeMins = diffTime.TotalMinutes;

                // Compare to rate then update the progress value accordingly.
                caskPanel[i].progress = Mathf.Clamp01(Convert.ToSingle(diffTimeMins) / caskPanel[i].rate);
            }
            else
            {
                // Allow the cask to be collected
                // Enable button
                caskCollectBtns[i].interactable = true;

                continue;
            }


        }


    }

    // Not Assigned to buttons yet
    public void CollectCask(int btn)
    {
        // Add output to stock
        stock += caskPanel[btn].output;
        // Reset progress
        caskPanel[btn].progress = 0;
        // Reset Timer
        caskPanel[btn].startTime = sysDateTime;
        // Disable button
        caskCollectBtns[btn].interactable = false;
    }

    public void SellCasks(int amount)
    {
        // Decrease stok amount by amount sold
        stock -= amount;
        // Add gold
        switch(amount)
        {
            case 1:
                GameManager.instance.AddGold(caskValue);
                break;
            case 25:
                GameManager.instance.AddGold(caskValue * 1.5);
                break;
            case 50:
                GameManager.instance.AddGold(caskValue * 2);
                break;
            case 100:
                GameManager.instance.AddGold(caskValue * 5);
                break;
            case 250:
                GameManager.instance.AddGold(caskValue * 10);
                break;
            case 500:
                GameManager.instance.AddGold(caskValue * 50);
                break;
        }
    }
    

    public void LoadPanels()
    {
        for (int i = 0; i < caskSO.Length; i++)
        {
            caskPanel[i].titleText.text = caskSO[i].title;
            caskPanel[i].caskIcon.sprite = caskSO[i].image;
            caskPanel[i].rate = caskSO[i].time;
            caskPanel[i].index = i;
            caskPanel[i].auto = caskSO[i].auto;
            caskPanel[i].output = caskSO[i].output;
            caskPanel[i].startTime = sysDateTime;
            caskPanel[i].progress = 0;
            caskPanel[i].caskButton = caskSO[i].button;
        }
    }
    
    public void SellPanels()
    {
        sellText[0].text = GameManager.instance.ConvertNum(caskValue) + " per Cask";
        sellText[1].text = GameManager.instance.ConvertNum(caskValue * 1.5) + " per Cask";
        sellText[2].text = GameManager.instance.ConvertNum(caskValue * 2) + " per Cask";
        sellText[3].text = GameManager.instance.ConvertNum(caskValue * 5) + " per Cask";
        sellText[4].text = GameManager.instance.ConvertNum(caskValue * 10) + " per Cask";
    }
}
