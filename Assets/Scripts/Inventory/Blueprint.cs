using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public string itemName;
    public string material1;
    public string material2;
    public string material3;
    public int materialAmount1;
    public int materialAmount2;
    public int materialAmount3;
    public int numOfMaterial;

    public Blueprint(string name, int reqNUM, string m1, int mA1, string m2, int mA2, string m3, int mA3)
    {
        itemName = name;
        material1 = m1;
        material2 = m2;
        numOfMaterial = reqNUM;
        materialAmount1 = mA1;
        materialAmount2 = mA2;
        material3 = m3;
        materialAmount3 = mA3;
    }
    public Blueprint(string name, int reqNUM, string m1, int mA1, string m2, int mA2)
    {
        itemName = name;
        material1 = m1;
        material2 = m2;
        numOfMaterial = reqNUM;
        materialAmount1 = mA1;
        materialAmount2 = mA2;
    }
}
