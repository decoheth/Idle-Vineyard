using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



//[CreateAssetMenu(fileName="CaskMenu", menuName="Scriptable Objects/New Cask", order=2)]
public class CaskTemplate : MonoBehaviour
{
    public TMP_Text titleText;
    public int index;
    public float rate;
    public Slider progressBar;
    //public bool auto;
    public Image caskIcon;


    void Update()
    {

        // Fetch progress value from CaskTimer, then update the progress bar to that value
        //progressBar.value = 1 - progress;
    }


}

