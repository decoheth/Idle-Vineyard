using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeTemplate : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text costText;
    public double clickModVal = 1.0d;
    public double clickBonusVal = 0.0d;
    public double autoModVal = 1.0d;
    public double autoBonusVal = 0.0d;
    public Image upgradeImage;
    public int listIndex = 0;
    public int flagVal = 0;
}

