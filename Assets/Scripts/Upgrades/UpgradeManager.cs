using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Managers")]
    // Game Manager object
    public GameObject GM;
    public GameObject SM;
    
    [Header("Upgrades")]
    public UpgradeSO[] upgradeSO;
    [SerializeField] GameObject upgradePrefab;
    [SerializeField] GameObject parentCanvasObjUpgrade;
    private List<Button> upgradePurchaseBtns = new List<Button>();
    private List<UpgradeTemplate> upgradePanels = new List<UpgradeTemplate>();
    private List<GameObject> upgradePanelsGO = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        // Enable necessary number of Upgrades
        for (int i = 0; i < upgradeSO.Length; i++)
        {
            var upgrade = Instantiate (upgradePrefab, transform.position , Quaternion.identity);
            // Set menu panel as parent
            upgrade.transform.SetParent(parentCanvasObjUpgrade.transform, false);

            // Set button behaviour
            Button upgradeBtn = upgrade.GetComponent<Button>();
            int tmp = i;
            upgradeBtn.onClick.AddListener(() => PurchaseUpgrade(tmp));

            // Add to Button list
            upgradePurchaseBtns.Add(upgradeBtn as Button);
            // Add to GameObject list
            upgradePanelsGO.Add(upgrade as GameObject);
            // Add to Template list
            upgradePanels.Add(upgrade.GetComponent<UpgradeTemplate>() as UpgradeTemplate);

            //Disable all panels
            upgrade.SetActive(false);
        }

        // Load panels with Scriptable Object data
        LoadPanels();
        CheckPurchaseable();
    }

    public void CheckPurchaseable()
    {
        // Upgrades
        for (int i = 0; i < upgradeSO.Length; i++)
        {
            // Check if user has enough gold
            if (GameManager.instance.gold >= upgradeSO[i].cost)  
                // Allow upgrade to be purchased
                upgradePurchaseBtns[i].interactable = true;
            else 
                upgradePurchaseBtns[i].interactable = false;
        }
    }


    public void PurchaseUpgrade(int index)
    {
        if (GameManager.instance.gold >= upgradeSO[index].cost)
        {
            GameManager.instance.RemoveGold(upgradeSO[index].cost);

            // Pass upgrade flag
            UpgradeFlag(upgradePanels[index].flagVal);

            // Remove from purchased upgrade from list
            upgradePanelsGO[index].SetActive(false);
        }
    }

    public void LoadPanels()
    {
        // Load saved data
        SaveData data = SM.GetComponent<SaveManager>().LoadGame();

        // Upgrades
        for (int i = 0; i < upgradeSO.Length; i++)
        {
            upgradePanels[i].titleText.text = upgradeSO[i].title;
            upgradePanels[i].descriptionText.text = upgradeSO[i].description;
            upgradePanels[i].costText.text = "Cost: " + GameManager.instance.ConvertNum(upgradeSO[i].cost);
            upgradePanels[i].upgradeImage.sprite = upgradeSO[i].image;
            upgradePanels[i].listIndex = i;
            upgradePanels[i].flagVal = upgradeSO[i].flag;
        }
        
        CheckPurchaseable();
    }
    

    public void GeneratorFlag(int count, int flag)
    {
        int a;
        int b;
        // Switch statement to check which generator is being passed
        switch(flag)
        {
            case 0:
                // Check count value, in increments of 25
                a = count / 25;
                a -= 1;
                b = (flag * 10) + a;
                upgradePanelsGO[b].SetActive(true);
                break;

            case 1:
                // Check count value, in increments of 25
                a = count / 25;
                a -= 1;
                b = (flag * 10) + a;
                upgradePanelsGO[b].SetActive(true);
                break;

            case 2:
                // Check count value, in increments of 25
                a = count / 25;
                a -= 1;
                b = (flag * 10) + a;
                upgradePanelsGO[b].SetActive(true);
                break;

            case 3:
                // Check count value, in increments of 25
                a = count / 25;
                a -= 1;
                b = (flag * 10) + a;
                upgradePanelsGO[b].SetActive(true);
                break;

            case 4:
                // Check count value, in increments of 25
                a = count / 25;
                a -= 1;
                b = (flag * 10) + a;
                upgradePanelsGO[b].SetActive(true);
                break;

            case 5:
                // Check count value, in increments of 25
                a = count / 25;
                a -= 1;
                b = (flag * 10) + a;
                upgradePanelsGO[b].SetActive(true);
                break;

            case 6:
                // Check count value, in increments of 25
                a = count / 25;
                a -= 1;
                b = (flag * 10) + a;
                upgradePanelsGO[b].SetActive(true);
                break;

            case 7:
                // Check count value, in increments of 25
                a = count / 25;
                a -= 1;
                b = (flag * 10) + a;
                upgradePanelsGO[b].SetActive(true);
                break;
        
        }   
    }


    public void UpgradeFlag(int flag)
    {
        switch(flag)
        {
            case 0:
                // Do stuff
                break;


            default:
                break;
        }
    }
}

