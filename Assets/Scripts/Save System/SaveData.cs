using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[System.Serializable]
public class SaveData
{

    public double savedGold;
    public double savedRate;
    public double savedValueBottle;
    public double savedStock;
    public int[] savedGenLevel;
    public string savedExitTime;
    public double savedAfkModifier;


    public SaveData (double gold, double rate, int[] genLevel, double stock, string exitTime, double valueBottle, double afkModifier) 
    {
        savedGold = gold;
        savedRate = rate;
        savedGenLevel = genLevel;
        savedStock = stock;
        savedExitTime = exitTime;
        savedValueBottle = valueBottle;
        savedAfkModifier = afkModifier;

    }


}
