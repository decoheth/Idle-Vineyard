using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CaskManager : MonoBehaviour
{
    [Header("Casks")]
    public CaskSO[] caskSO;
    [SerializeField] GameObject caskPrefab;
    [SerializeField] GameObject parentCanvasObjCask;

    private List<Button> caskCollectBtns = new List<Button>();
    //private List<Slider> caskSlider = new List<Slider>();
    public List<CaskTemplate> caskPanel = new List<CaskTemplate>();
    private List<GameObject> caskGO = new List<GameObject>();

    public static CaskManager instance;

    private DateTime sysDateTime;

    public double stock;
    public TMP_Text stockText;

    // Start is called before the first frame update
    void Start()
    {
        sysDateTime = System.DateTime.Now;

        for (int i = 0; i < caskSO.Length; i++)
        {
            // Instantiate from prefab
            var cask = Instantiate (caskPrefab, transform.position , Quaternion.identity);
            // Set menu panel as parent
            cask.transform.SetParent(parentCanvasObjCask.transform, false);
        


            // Set button behaviour
            Button caskBtn = cask.GetComponent<Button>();
            //int tmp = i;
            //caskBtn.onClick.AddListener(() => CollectCask(tmp));

            // Add to Button list
            caskCollectBtns.Add(caskBtn as Button);
            // Add to GameObject list
            caskGO.Add(cask as GameObject);
            // Add to Template list
            caskPanel.Add(cask.GetComponent<CaskTemplate>() as CaskTemplate);

            //Enable all panels
            cask.SetActive(true);
        }

        // Load panels with Scriptable Object data
        LoadPanels();
    }

    public void Update()
    {
        sysDateTime = System.DateTime.Now;

        // Set stock text
        // If below 1000 (To prevent decimal)
        if (stock < 1000)
            stockText.text = "Casks in Stock: " + stock;
        else    
            stockText.text = "Casks in Stock: " + GameManager.instance.ConvertNum(stock);


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
                // Add one to stock
                stock += 1;
                // Reset progress
                caskPanel[i].progress = 0;
                // Reset Timer
                caskPanel[i].startTime = sysDateTime;
                continue;
            }


        }



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
            caskPanel[i].auto = caskSO[i].auto;
            caskPanel[i].startTime = sysDateTime;
            caskPanel[i].progress = 0;
            //caskPanel[i].caskButton = caskSO[i].button;
        }
    }
    
}
