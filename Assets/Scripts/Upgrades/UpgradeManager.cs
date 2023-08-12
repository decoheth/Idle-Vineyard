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

    [Header("Generators")]
    public GeneratorSO[] generatorSO;
    [SerializeField] GameObject generatorPrefab;
    [SerializeField] GameObject parentCanvasObjGen;
    private List<Button> generatorPurchaseBtns = new List<Button>();
    [HideInInspector]
    public List<GeneratorTemplate> generatorPanels = new List<GeneratorTemplate>();
    private List<GameObject> generatorPanelsGO = new List<GameObject>();
    
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


        // Instantiate from prefab

        // Enable necessary number of Generators
        for (int i = 0; i < generatorSO.Length; i++)
        {
            var gen = Instantiate (generatorPrefab, transform.position , Quaternion.identity);
            // Set menu panel as parent
            gen.transform.SetParent(parentCanvasObjGen.transform, false);

            // Set button behaviour
            Button genBtn = gen.GetComponent<Button>();
            int tmp = i;
            genBtn.onClick.AddListener(() => PurchaseGenerator(tmp));

            // Add to Button list
            generatorPurchaseBtns.Add(genBtn as Button);
            // Add to GameObject list
            generatorPanelsGO.Add(gen as GameObject);
            // Add to Template list
            generatorPanels.Add(gen.GetComponent<GeneratorTemplate>() as GeneratorTemplate);

            //Enable all panels
            gen.SetActive(true);
        }

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
        // Generators
        for (int i = 0; i < generatorSO.Length; i++)
        {
            // Check if user has enough gold
            if (GameManager.instance.gold >= generatorPanels[i].genCost)  
                // Allow upgrade to be purchased
                generatorPurchaseBtns[i].interactable = true;
            else
                generatorPurchaseBtns[i].interactable = false;
        }
    
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

    public void PurchaseGenerator(int index)
    {
        if (GameManager.instance.gold >= generatorPanels[index].genCost)
        {
            GameManager.instance.RemoveGold(generatorPanels[index].genCost);

            // Grant Bonuses
            GM.GetComponent<GameManager>().IncreaseAutoRateBonus(generatorPanels[index].genRate);
            // Increase count
            generatorPanels[index].countVal += 1;
            // increase cost of next purchase by cost modifier
            generatorPanels[index].genCost = generatorSO[index].baseCost * System.Math.Pow((1+generatorPanels[index].costModVal), generatorPanels[index].countVal);
            // Add rate to total count
            generatorPanels[index].totalRate = generatorPanels[index].genRate * generatorPanels[index].countVal;
            
            // Check if count is a multiple of 25
            if(generatorPanels[index].countVal % 25 == 0)
            {
                // Pass flag to unlock upgrade
                GeneratorFlag(generatorPanels[index].countVal, generatorPanels[index].genFlagVal);
            }

            // Update generator text
            generatorPanels[index].effectText.text = "+" + GameManager.instance.ConvertNum(generatorPanels[index].totalRate);
            generatorPanels[index].costText.text = "Cost: " + GameManager.instance.ConvertNum(generatorPanels[index].genCost);
            generatorPanels[index].countText.text = "No: " + generatorPanels[index].countVal.ToString();
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


        // Generators
        for (int i = 0; i < generatorSO.Length; i++)
        {
            // Load saved data
            SaveData data = SM.GetComponent<SaveManager>().LoadGame();

            // Assign data
            generatorPanels[i].titleText.text = generatorSO[i].title;
            generatorPanels[i].genImage.sprite = generatorSO[i].image;

            generatorPanels[i].listIndex = i;
            generatorPanels[i].genFlagVal = generatorSO[i].genFlag; 

            generatorPanels[i].countVal = data.savedGenLevel[i];

            generatorPanels[i].genRate = generatorSO[i].baseRate;
            generatorPanels[i].costModVal = generatorSO[i].costMod;


            
            // Compound interest formula
            generatorPanels[i].genCost = generatorSO[i].baseCost * System.Math.Pow((1+generatorPanels[i].costModVal), generatorPanels[i].countVal);
            generatorPanels[i].totalRate = generatorPanels[i].genRate * generatorPanels[i].countVal;
            
            generatorPanels[i].effectText.text = "+" + GameManager.instance.ConvertNum(generatorPanels[i].totalRate);
            generatorPanels[i].costText.text = "Cost: " + GameManager.instance.ConvertNum(generatorPanels[i].genCost);
            generatorPanels[i].countText.text = "No: " + generatorPanels[i].countVal.ToString();
        }

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
