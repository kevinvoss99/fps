using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using UnityEngine;
using UnityEngine.UI;

public class UpTimeScript : MonoBehaviour {

    public static double upTimeValue = 0.0;
    public static bool pauseHit = false;

    Text upTime;

    //System.Diagnostics.Stopwatch upTimer = new System.Diagnostics.Stopwatch();


	// Use this for initialization
	void Start () {

        upTime = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {

        if (!pauseHit) {

            upTimeValue += 0.05;

        } else
        {
            upTimeValue += 0.00;
        }

        upTime.text = "UpTime: " + upTimeValue.ToString("00.00");
    }
}
