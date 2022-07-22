using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinChanceScript : MonoBehaviour {

    public static double winChanceValue;
    public static int cbrHealth;
    public static int kiHealth;
    public static bool betterEquipped = false;
    public static int cbrWeapon = 1;
    public static int kiWeapon= 1;
    public static int cbrAmmu = 1;
    public static int kiAmmu = 1;
    public static double healthDiff;
    public static double ammuDiff;

    Text winChance;

	// Use this for initialization
	void Start () {

        winChance = GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {

        healthDiff = ((cbrHealth - kiHealth) * 0.45);
        ammuDiff = ((((kiAmmu / cbrAmmu) * 12.5) * 0.05));

        winChanceValue = (healthDiff + ammuDiff)+50.0;

        if (cbrWeapon>kiWeapon)
        {
            betterEquipped = true;
            winChanceValue += 25.0;

        } else if(kiWeapon>cbrWeapon)
        {
            winChanceValue -= 25.0;
        }


        

        winChance.text = "WinChance: " + winChanceValue.ToString("0.00") + "%";
        
		
	}


}
