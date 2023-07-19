using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Upgrades")]

    public GameObject UpgradeMenu;
    public GameObject UpgradeTab1;
    public GameObject UpgradeTab2;
    public GameObject UpgradeTab3;

    [Header("Casks")]


    public GameObject CaskMenu;
    public GameObject CaskTab1;
    public GameObject CaskTab2;
    public GameObject CaskTab3;

    [Header("Buttons")]

    public GameObject SettingsButton;
    public GameObject VintageButton;

    public static UIManager instance;

    void Awake() 
    {
        instance = this;

        // Enable buttons on startup
        SettingsButton.SetActive(true);
        VintageButton.SetActive(true);
        // Disable menus on startup
        UpgradeMenu.SetActive(false);
        CaskMenu.SetActive(false);

        // Set initial selected menu tabs
        SetUpgradeTab1();
        SetCaskTab1();

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

    public void ToggleOtherUI()
    {
        SettingsButton.SetActive(!SettingsButton.activeSelf);
        VintageButton.SetActive(!VintageButton.activeSelf);
    }
}
