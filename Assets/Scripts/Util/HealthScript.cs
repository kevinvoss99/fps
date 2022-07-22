using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    public static int healthValue;
    public static int enemyHealthValue;

    Text health;


	// Use this for initialization
	void Start () {

        health = GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	public void Update () {

        health.text = "Health: " + healthValue;

	}
}
