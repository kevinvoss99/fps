using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KDScript : MonoBehaviour
{

    public static double cbrBotKD = 0.0;
    public static double death = 0.0;
    public static double frag = 0.0;

    Text kdRatio;


    // Use this for initialization
    void Start()
    {

        kdRatio = GetComponent<Text> ();

    }

    // Update is called once per frame
    void Update()
    {
        if(death == 0 ) {

            cbrBotKD = frag;

        } else {

            cbrBotKD = frag / death;
            kdRatio.text = "K/D: " + frag + "/" + death +"="+ cbrBotKD.ToString("0.00");

        }

    }

}
