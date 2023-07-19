using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class CaskTemplate : MonoBehaviour
{
    public TMP_Text titleText;
    public int index;
    public float rate;
    public Slider progressBar;
    public float progress;
    public int auto;
    public Image caskIcon;
    public DateTime startTime;
    public Button caskButton;

   void Update()
   {
        // Update the slider to reflect the value of the progress variable
        progressBar.value = progress;
   }


}

