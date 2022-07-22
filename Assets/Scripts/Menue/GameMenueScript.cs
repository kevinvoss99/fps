using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/**
 * Skript, welches das Spielmenü darstellt, falls der Benutzer während des laufenden Spiels 'ESC' drückt.
 */
public class GameMenueScript : MonoBehaviour
{
    /**
     * Das Bild, welches über die Szene gelegt wird, wenn der Spieler 'ESC' drückt.
     */
    public Image pauseImage;

    /**
     * Button zum Fortführen des Spiels.
     */
    public Button continueButton;
    /**
     * Button zum Betreten des Zuschauermodus.
     */
    public Button spectatorModeButton;
    /**
     * Button um zum Hauptmenü zu kommen.
     */
    public Button mainMenueButton;
    /**
     * Button zum Beenden des Spiels.
     */
    public Button exitButton;

    /**
     * Spielerkamera.
     */
    public Camera playerCamera;

    /**
     * Referenz auf das GameControllerScript, um Zugriff auf den GameStatus (PAUSED/RUNNING) zu erhalten.
     */
    public GameControllerScript gameControllerScript;

    /**
     * Liste, welche die Buttons enthält.
     */
    private List<Button> buttonList;
    /**
     * Default-Color Wert.
     */
    private Color defaultColor;
    /**
     * Referenz auf ein Unity-Script, welches einige statische Funktionen für das Menü enthält.
     */
    private StaticMenueFunctions staticMenueFunctions;

    /**
     * Unity-Methode.
     */
    private void OnEnable()
    {
        defaultColor = pauseImage.color;

        buttonList = new List<Button>();
        buttonList.Add(continueButton);
        buttonList.Add(spectatorModeButton);
        buttonList.Add(mainMenueButton);
        buttonList.Add(exitButton);

        staticMenueFunctions = StaticMenueFunctions.GetInstance();
        staticMenueFunctions.camera = playerCamera;
    }

    /**
     * Diese Methode führt das Spiel fort, sollte es pausiert sein und pausiert es, sollte es laufen.
     */
    public void ToggleGameMenue()
    {
        Color tempColor = defaultColor;
        tempColor.a = 0.2f;

        bool isPaused = gameControllerScript.mState == GameControllerScript.GameState.PAUSED;

        pauseImage.color = isPaused ? tempColor : defaultColor;

        foreach (Button btn in buttonList)
        {
            btn.gameObject.SetActive(isPaused);
        }
    }

    /**
     * Schalte das Menü auf den Ausgangsstatus.
     */
    public void SetGameMenueToDefaultSettings()
    {
        gameControllerScript.ContinueGame(false);
        pauseImage.color = defaultColor;
        foreach (Button btn in buttonList)
        {
            btn.gameObject.SetActive(false);
        }
    }



    public void StartSpectatorMode()
    {
        // TBD
    }

    /**
     * Methode um die Szene zu ändern - Not needed.
     */
    public void ChangeScene(string name)
    {
        staticMenueFunctions.ChangeScene(name);
    }
    /*
     * Methode um das Spiel zu beenden.
     */
    public void QuitApplication()
    {
        staticMenueFunctions.QuitApplication();
    }
}