using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[System.Serializable]
public class SaveData
{

    public double savedGold;
    public double savedRate;
    public double savedGoldPerClick;

    public double savedStock;
    public int[] savedGenLevel;

    public string savedExitTime;


    public SaveData (double gold, double rate, double gpc, int[] genLevel, double stock, string exitTime) 
    {
        savedGold = gold;
        savedRate = rate;
        savedGoldPerClick = gpc;
        savedGenLevel = genLevel;
        savedStock = stock;
        savedExitTime = exitTime;

    }


}
