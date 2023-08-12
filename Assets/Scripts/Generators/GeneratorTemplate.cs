using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorTemplate : MonoBehaviour

{
    // References
    public TMP_Text titleText;
    public TMP_Text costText;
    public TMP_Text effectText;
    public TMP_Text countText;
    public Image genImage;
    // Backend
    public int listIndex;
    public int countVal = 0;
    public int genFlagVal;
    // Values
    public double costModVal;
    public double totalRate;
    public double genCost;
    public double genRate;

}

