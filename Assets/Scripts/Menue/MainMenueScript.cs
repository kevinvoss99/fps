using UnityEngine;
using UnityEngine.UI;

/**
 * NOT NEEDED IN PROJECT
 */
public class MainMenueScript : MonoBehaviour {

    private static StaticMenueFunctions staticMenueFunctions;

    public static bool OnlyBots { get; set; }
    public Button onlyBotsButton;

    private static string[] onlyBotsStrings = { "1vs1 (Bots)", "1vs1" };

    private void Start()
    {
        staticMenueFunctions = StaticMenueFunctions.GetInstance();
        OnlyBots = true;
    }


    public void ChangeScene(string name)
    {
        staticMenueFunctions.ChangeScene(name);
    }

    public void QuitApplication()
    {
        staticMenueFunctions.QuitApplication();
    }

    public void ChangeBotButton()
    {
        OnlyBots = !OnlyBots;
        onlyBotsButton.GetComponentInChildren<Text>().text = OnlyBots ? onlyBotsStrings[0] : onlyBotsStrings[1];
    }
	

}
