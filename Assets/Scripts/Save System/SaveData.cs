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

    // DateTime not serializable
    public DateTime savedExitTime;


    public SaveData (double gold, double rate, double gpc, int[] genLevel, double stock, DateTime exitTime) 
    {
        savedGold = gold;
        savedRate = rate;
        savedGoldPerClick = gpc;
        savedGenLevel = genLevel;
        savedStock = stock;
        savedExitTime = exitTime;

    }


}
