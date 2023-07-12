using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="UpgradeMenu", menuName="Scriptable Objects/New Upgrade Item", order=2)]
public class UpgradeItemsSO : ScriptableObject
{

    public string title;
    public string description;
    public double cost;
    public double clickMod;
    public double clickBonus;
    public double autoMod;
    public double autoBonus;
    public Sprite image;
    public int flag;


}
