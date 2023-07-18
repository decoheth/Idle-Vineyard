using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{

    public double savedGold;
    public double savedRate;
    public double savedGoldPerClick;
    //public int[] genLevel;

    public SaveData (double gold, double rate, double gpc) 
    {
        savedGold = gold;
        savedRate = rate;
        savedGoldPerClick = gpc;

    }


}
