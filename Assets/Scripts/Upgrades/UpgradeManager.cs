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
    [SerializeField] GameObject generatorPrefab;
    [SerializeField] GameObject parentCanvasObj;
    private List<Button> generatorPurchaseBtns = new List<Button>();
    private List<GeneratorTemplate> generatorPanels = new List<GeneratorTemplate>();
    private List<GameObject> generatorPanelsGO = new List<GameObject>();
    
    public UpgradeSO[] upgradeSO;
    public UpgradeTemplate[] upgradePanels;
    public GameObject[] upgradePanelsGO;
    public Button[] PurchaseBtns;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate generators from prefab

        // Enable necessary number of Generators
        for (int i = 0; i < generatorSO.Length; i++)
        {
            //genName = "Gen_" + i;
            var gen = Instantiate (generatorPrefab, transform.position , Quaternion.identity);
            // Set menu panel as parent
            gen.transform.SetParent(parentCanvasObj.transform, false);

            
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
        for (int i = 0; i < upgradeSO.Length; i++)
        {
            // Check if user has enough gold
            if (GameManager.instance.gold >= upgradeSO[i].cost)  
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

            
            // Check if count is a multiple of 25
            if(generatorSO[BtnNo].count % 25 == 0)
            {
                // Pass flag to unlock upgrade
                GeneratorFlag(generatorSO[BtnNo].count, generatorPanels[BtnNo].genFlagVal);
            }

            // Update generator text
            generatorPanels[BtnNo].countText.text = "No:" + generatorSO[BtnNo].count.ToString();
            generatorPanels[BtnNo].effectText.text = "+" + GameManager.instance.ConvertNum(generatorSO[BtnNo].totalRate);
            generatorPanels[BtnNo].costText.text = "Cost:" + GameManager.instance.ConvertNum(generatorPanels[BtnNo].costVal);
	    } 
    }

    public void PurchaseUpgrade(int BtnNo)
    {
        if (GameManager.instance.gold >= upgradeSO[BtnNo].cost)
        {
            GameManager.instance.RemoveGold(upgradeSO[BtnNo].cost);

            // Pass upgrade flag
            UpgradeFlag(upgradePanels[BtnNo].flagVal);

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
            generatorPanels[i].genFlagVal = generatorSO[i].genFlag;


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
