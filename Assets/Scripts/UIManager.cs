using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

    [Header("Focus")]
    public GameObject CropsFocusButton;
    public GameObject BuildingFocusButton;
    public GameObject ShopFocusButton;
    public GameObject CropsFocus;
    public GameObject BuildingFocus;
    public GameObject ShopFocus;

    [Header("Buttons")]

    public GameObject SettingsButton;
    public GameObject VintageButton;

    public static UIManager instance;

    void Awake() 
    {
        instance = this;

        // Enable buttons on startup
        UpgradeButton.SetActive(true);
        CaskButton.SetActive(true);
        SettingsButton.SetActive(true);
        VintageButton.SetActive(true);
        CropsFocusButton.SetActive(true);
        BuildingFocusButton.SetActive(true);
        ShopFocusButton.SetActive(true);

        // Disable menus on startup
        UpgradeMenu.SetActive(false);
        CaskMenu.SetActive(false);

        // Set initial selected
        SetUpgradeTab1();
        SetCaskTab1();
        SetFocus2();

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



    public void SetFocus1()
    {
        CropsFocus.SetActive(true);
        BuildingFocus.SetActive(false);
        ShopFocus.SetActive(false);
    }

    public void SetFocus2()
    {
        CropsFocus.SetActive(false);
        BuildingFocus.SetActive(true);
        ShopFocus.SetActive(false);
    }

    public void SetFocus3()
    {
        CropsFocus.SetActive(false);
        BuildingFocus.SetActive(false);
        ShopFocus.SetActive(true);
    }
}
