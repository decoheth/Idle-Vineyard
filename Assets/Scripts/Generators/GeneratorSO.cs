using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="UpgradeMenu", menuName="Scriptable Objects/New Generator", order=1)]
public class GeneratorSO : ScriptableObject
{

    public string title;
    public double baseCost;
    public double baseRate;
    public double costMod;
    public Sprite image;
    public int genFlag;
}
