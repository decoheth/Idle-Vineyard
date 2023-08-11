using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject uiCanvas;
    public GameObject popupCanvas;
    public GameObject darkenPanel;

    [Header("Upgrades")]
    public GameObject UpgradeButton;
    public GameObject UpgradeMenu;
    public GameObject UpgradeTab1;
    public GameObject UpgradeTab2;
    public GameObject UpgradeTab3;

    [Header("Casks")]

    public GameObject CaskButton;
    public GameObject CaskMenu;
    public GameObject CaskTab1;
    public GameObject CaskTab2;
    public GameObject CaskTab3;

    [Header("Settings")]

    public GameObject SettingsButton;
    public GameObject SettingsMenu;
    public GameObject SettingsTab1;
    public GameObject SettingsTab2;
    public GameObject SettingsTab3;

    [Header("PopUps")]
    public GameObject afkPopup;
    public TMP_Text afkText;
    public TMP_Text afkTimeText;

    [Header("Vintage")]
    public GameObject VintageButton;

    public static UIManager instance;

    void Awake() 
    {
        instance = this;

        // Enable buttons on startup
        UpgradeButton.SetActive(true);
        SettingsButton.SetActive(true);
        // If casks have been unlocked
        CaskButton.SetActive(true);
        // If Vintages have been unlocked
        VintageButton.SetActive(true);

        // Disable menus on startup
        UpgradeMenu.SetActive(false);
        CaskMenu.SetActive(false);
        SettingsMenu.SetActive(false);

        darkenPanel.SetActive(false);
        popupCanvas.SetActive(false);
        afkPopup.SetActive(false);

        // Set initial selected
        SetUpgradeTab1();
        SetCaskTab1();
        SetSettingsTab1();

    }

    public void ToggleOtherUI()
    {
        SettingsButton.SetActive(!SettingsButton.activeSelf);
        VintageButton.SetActive(!VintageButton.activeSelf);
    }

    public void ToggleUpgradeMenu()
    {
        UpgradeMenu.SetActive(!UpgradeMenu.activeSelf);
        ToggleOtherUI();
    }

    public void SetUpgradeTab1()
    {
        UpgradeTab1.SetActive(true);
        UpgradeTab2.SetActive(false);
        UpgradeTab3.SetActive(false);
    }

    public void SetUpgradeTab2()
    {
        UpgradeTab1.SetActive(false);
        UpgradeTab2.SetActive(true);
        UpgradeTab3.SetActive(false);
    }

    public void SetUpgradeTab3()
    {
        UpgradeTab1.SetActive(false);
        UpgradeTab2.SetActive(false);
        UpgradeTab3.SetActive(true);
    }



    public void ToggleCaskMenu()
    {
        CaskMenu.SetActive(!CaskMenu.activeSelf);
        ToggleOtherUI();
    }

    public void SetCaskTab1()
    {
        CaskTab1.SetActive(true);
        CaskTab2.SetActive(false);
        CaskTab3.SetActive(false);
    }

    public void SetCaskTab2()
    {
        CaskTab1.SetActive(false);
        CaskTab2.SetActive(true);
        CaskTab3.SetActive(false);
    }

    public void SetCaskTab3()
    {
        CaskTab1.SetActive(false);
        CaskTab2.SetActive(false);
        CaskTab3.SetActive(true);
    }

   public void ToggleSettingsMenu()
    {
        SettingsMenu.SetActive(!SettingsMenu.activeSelf);
        ToggleOtherUI();
    }

    public void SetSettingsTab1()
    {
        SettingsTab1.SetActive(true);
        SettingsTab2.SetActive(false);
        SettingsTab3.SetActive(false);
    }

    public void SetSettingsTab2()
    {
        SettingsTab1.SetActive(false);
        SettingsTab2.SetActive(true);
        SettingsTab3.SetActive(false);
    }

    public void SetSettingsTab3()
    {
        SettingsTab1.SetActive(false);
        SettingsTab2.SetActive(false);
        SettingsTab3.SetActive(true);
    }


    public void AfkPopup(double income, double time)
    {
        afkPopup.SetActive(true);
        darkenPanel.SetActive(true);
        popupCanvas.SetActive(true);
        uiCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        afkText.text = "You have earned " + GameManager.instance.ConvertNum(income) + " gold while you were away!";
        afkTimeText.text = "Away for " + time.ToString("F0") + " minutes";
    }

    public void ClosePopup()
    {
        afkPopup.SetActive(false);
        darkenPanel.SetActive(false);
        popupCanvas.SetActive(false);
        uiCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;       
    }
}
