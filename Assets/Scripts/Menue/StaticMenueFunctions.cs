using System;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * Klasse, die einige statische Methoden für die Menüführung beinhaltet.
 */
public class StaticMenueFunctions {

    /**
     * Kameraobjekt.
     */
    public Camera camera { get; set; }
    /**
     * Singleton Desing Pattern.
     */
    private static StaticMenueFunctions staticMenueFunctions;

    /**
     * Default-Konstruktor
     */
    private StaticMenueFunctions()
    {
        
    }
    
    /**
     * Singleton Desing Pattern Methode.
     * 
     * ANMERKUNG: Singleton Design Pattern ist hier absolut überflüssig. Zu Beginn war es sinnvoll, weil noch einige Einstellungen im Konstruktor vorgenommen wurden, inzwischen überflüssig.
     */
    public static StaticMenueFunctions GetInstance()
    {
        if (staticMenueFunctions == null)
        {
            staticMenueFunctions = new StaticMenueFunctions();
        }

        return staticMenueFunctions;
    }

    /**
     * Methode, um die Unity-Scene zu wechseln.
     */
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /**
     * Methode um die Applikation komplett zu beenden.
     */
    public void QuitApplication()
    {
        Application.Quit();
    }

    /**
     * Methode um ein DateTime Objekt (timestamp) in einen String zu konvertierne.
     */
    public static string GetTimeStampString(DateTime timestamp)
    {
        return timestamp.ToString("dd.MM.yyyy HH:mm:ss.ffff");
    }


    /**
     * Diese generische Methode findet Komponenten in den Kindern eines GameObjekts anhand des tags.
     */
    public static T FindComponentInChildWithTag<T>(GameObject parent, string tag) where T : Component
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.GetComponent<T>();
            }
        }

        return null;
    }

    /**
     * Diese generische Methode findet eine Komponente des Typs <T> innerhalb der Kinder des übergebenen GameObjekts.
     */
    public static T FindComponentInChildren<T>(GameObject parent) where T : Component
    {
        T[] values = parent.GetComponentsInChildren<T>();
        T value = null;
        foreach (T val in values)
        {
            if (val.gameObject.transform.parent != null)
            {
                value = val;
                break;
            }
        }

        return value;
    }


}
