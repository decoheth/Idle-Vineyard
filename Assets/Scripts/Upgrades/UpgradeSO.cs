using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="UpgradeMenu", menuName="Scriptable Objects/New Upgrade", order=2)]
public class UpgradeSO : ScriptableObject
{

    public string title;
    public string description;
    public double cost;
    public Sprite image;
    public int flag;
    public bool isPurchased;


}
