using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="UpgradeMenu", menuName="Scriptable Objects/New Generator", order=1)]
public class GeneratorSO : ScriptableObject
{

    public string title;
    public double cost;
    public double rate;
    public double costMod;
    public double rateMod;
    public Sprite image;
    public int count;
    public double totalRate;
    public int genFlag;
}
