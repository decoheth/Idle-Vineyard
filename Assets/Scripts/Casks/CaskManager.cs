using System.Collections;
using System.Collections.Generic;
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
    private List<CaskTemplate> caskPanel = new List<CaskTemplate>();
    private List<GameObject> caskGO = new List<GameObject>();

    [Header("Timers")]

    public static CaskManager instance;

    // Start is called before the first frame update
    void Start()
    {
        
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
