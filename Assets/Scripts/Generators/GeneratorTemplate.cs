using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorTemplate : MonoBehaviour

{
    public TMP_Text titleText;
    public TMP_Text costText;
    public TMP_Text effectText;
    public TMP_Text countText;
    public double costModVal;
    public double rateModVal;
    public double costVal;
    public double rateVal;
    public Image genImage;
    public int listIndex = 0;
    public int countVal = 0;
    public double totalRateVal = 0d;
    public int genFlagVal;
}

