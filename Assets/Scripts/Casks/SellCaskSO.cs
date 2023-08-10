using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="SellCask", menuName="Scriptable Objects/New SellCask", order=3)]
public class SellCaskSO : ScriptableObject
{
    public double multiplier;
    public double amount;
    public Button button;

}