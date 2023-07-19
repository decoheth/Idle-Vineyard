using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="Cask", menuName="Scriptable Objects/New Cask", order=3)]
public class CaskSO : ScriptableObject
{

    public string title;
    public float time;
    public Sprite image;
    // change to bool
    public int auto;
    public Button button;
    public double output;



}
