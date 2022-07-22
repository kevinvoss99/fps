using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Erstellt durch das Tutorial: https://www.youtube.com/watch?v=fAUmoa1a0-U
 */


/*
 * Diese Klasse ist für die Perspektive des Spielers verantwortlich
 */
[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class PlayerPerspective : MonoBehaviour
{

    /*
     * Objekt des Game Controllers
     */
    private GameObject gameController;
    /*
     * Objekt des Game Controller Script
     */
    private GameControllerScript gameControllerScript;
    /*
     * float Variable für die Maus-Sensivität
     */
    float lookSensitivity = 4;
    /*
     * float Variable für die y-Rotation
     */
    float yRotation;
    /*
     * float Variable für die x-Rotation
     */
    float xRotation;
    /*
     * float Variable für die Maus-Smoothnes
     */
    float lookSmoothnes = 0.1f;
    /*
     * float Variable für die aktuelle x-Rotation
     */
    float currentXRotation;
    /*
     * float Variable für die aktuelle y-Rotation
     */
    float currentYRotation;
    /*
     * float Variable für die aktuelle y-Rotation Geschwindigkeit
     */
    float yRotationV;
    /*
     * float Variable für die aktuelle x-Rotation Geschwindigkeit
     */
    float xRotationV;

    /*
     * Unity-Methode
     */
    void Update()
    {
        // Solange das Spiel läuft werden die Maus Eingaben abgefangen und zur Bewegung transformiert
        if (gameControllerScript.mState == GameControllerScript.GameState.RUNNING)
        {
            yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
            xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;

            xRotation = Mathf.Clamp(xRotation, -90, 90);

            currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothnes);
            currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothnes);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }

    /*
     * Unity-Methode
     */
    void Start()
    {
        //Initalisiert das Game Controller Script solange der Game Controller nicht null ist
        gameController = GameObject.Find("GameController");
        if (gameController != null)
        {
            gameControllerScript = gameController.GetComponent("GameControllerScript") as GameControllerScript;
        }

    }

}