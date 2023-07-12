using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    // Game Manager object
    public GameObject GM;


    public GeneratorSO[] generatorSO;
    public GeneratorTemplate[] generatorPanels;
    public GameObject[] generatorPanelsGO;
    public Button[] generatorPurchaseBtns;
    
    public UpgradeItemsSO[] upgradeItemsSO;
    public UpgradeTemplate[] upgradePanels;
    public GameObject[] upgradePanelsGO;
    public Button[] PurchaseBtns;

    // Start is called before the first frame update
    void Start()
    {
        // Enable necessary number of menu panels

        // Generators
        for (int i = 0; i < generatorSO.Length; i++)
        {
           generatorPanelsGO[i].SetActive(true); 
        }

        // Upgrades
        for (int i = 0; i < upgradeItemsSO.Length; i++)
        {
           upgradePanelsGO[i].SetActive(true); 
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
            if (GameManager.instance.gold >= generatorPanels[i].costVal)  
                // Allow upgrade to be purchased
                generatorPurchaseBtns[i].interactable = true;
            else
                generatorPurchaseBtns[i].interactable = false;
        }
    
        // Upgrades
        for (int i = 0; i < upgradeItemsSO.Length; i++)
        {
            // Check if user has enough gold
            if (GameManager.instance.gold >= upgradeItemsSO[i].cost)  
                // Allow upgrade to be purchased
                PurchaseBtns[i].interactable = true;
            else 
                PurchaseBtns[i].interactable = false;
        }
    }

    public void PurchaseGenerator(int BtnNo)
    {


        if (GameManager.instance.gold >= generatorPanels[BtnNo].costVal)
        {
            GameManager.instance.RemoveGold(generatorPanels[BtnNo].costVal);

            // Grant Bonuses
            GM.GetComponent<ClickManager>().IncreaseAutoRateBonus(generatorPanels[BtnNo].rateVal);
            // Increase count
            generatorSO[BtnNo].count += 1;
            // Add rate to total count
            generatorSO[BtnNo].totalRate += generatorPanels[BtnNo].rateVal;
            // increase cost of next purchase by cost modifier
            generatorPanels[BtnNo].costVal *= generatorPanels[BtnNo].costModVal;
            // increase cost of next purchase by cost modifier
            generatorPanels[BtnNo].rateVal *= generatorPanels[BtnNo].rateModVal;

            // Update generator text
            generatorPanels[BtnNo].countText.text = "No:" + generatorSO[BtnNo].count.ToString();
            generatorPanels[BtnNo].effectText.text = "+" + GameManager.instance.ConvertNum(generatorSO[BtnNo].totalRate);
            generatorPanels[BtnNo].costText.text = "Cost:" + GameManager.instance.ConvertNum(generatorPanels[BtnNo].costVal);
        }
    }

    public void PurchaseItem(int BtnNo)
    {


        if (GameManager.instance.gold >= upgradeItemsSO[BtnNo].cost)
        {
            GameManager.instance.RemoveGold(upgradeItemsSO[BtnNo].cost);

            // Grant Upgrades
            GM.GetComponent<ClickManager>().IncreaseAutoRateMod(upgradeItemsSO[BtnNo].autoMod);
            GM.GetComponent<ClickManager>().IncreaseAutoRateBonus(upgradeItemsSO[BtnNo].autoBonus);
            GM.GetComponent<ClickManager>().IncreaseGoldPerClickMod(upgradeItemsSO[BtnNo].clickMod);
            GM.GetComponent<ClickManager>().IncreaseGoldPerClickBonus(upgradeItemsSO[BtnNo].clickBonus);

            // Remove from purchased upgrade from list
            upgradePanelsGO[BtnNo].SetActive(false);
        }
    }

    public void LoadPanels()
    {
        // Generators
        for (int i = 0; i < generatorSO.Length; i++)
        {
             // Reset count and total rate (TODO retrieve from save data)
            generatorSO[i].count = 0;
            generatorSO[i].totalRate = 0;
            generatorPanels[i].countVal = 0;
            generatorPanels[i].totalRateVal = 0;

            generatorPanels[i].titleText.text = generatorSO[i].title;
            generatorPanels[i].effectText.text = "+" + GameManager.instance.ConvertNum(generatorSO[i].totalRate);
            generatorPanels[i].costText.text = "Cost: " + GameManager.instance.ConvertNum(generatorSO[i].cost);
            generatorPanels[i].countText.text = "No: " + generatorSO[i].count.ToString();
            generatorPanels[i].genImage.sprite = generatorSO[i].image;
            generatorPanels[i].costVal = generatorSO[i].cost;
            generatorPanels[i].rateVal = generatorSO[i].rate;
            generatorPanels[i].costModVal = generatorSO[i].costMod;
            generatorPanels[i].rateModVal = generatorSO[i].rateMod;
            generatorPanels[i].listIndex = i;


        }

        // Upgrades
        for (int i = 0; i < upgradeItemsSO.Length; i++)
        {
            upgradePanels[i].titleText.text = upgradeItemsSO[i].title;
            upgradePanels[i].descriptionText.text = upgradeItemsSO[i].description;
            upgradePanels[i].costText.text = "Cost: " + GameManager.instance.ConvertNum(upgradeItemsSO[i].cost);
            upgradePanels[i].upgradeImage.sprite = upgradeItemsSO[i].image;
            upgradePanels[i].clickModVal = upgradeItemsSO[i].clickMod;
            upgradePanels[i].clickBonusVal = upgradeItemsSO[i].clickBonus;
            upgradePanels[i].autoModVal = upgradeItemsSO[i].autoMod;
            upgradePanels[i].autoBonusVal = upgradeItemsSO[i].autoBonus;
            upgradePanels[i].listIndex = i;
        }
        
        CheckPurchaseable();
    }
}
