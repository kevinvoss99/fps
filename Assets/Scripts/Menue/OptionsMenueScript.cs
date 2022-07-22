using UnityEngine;

/**
 * NOT NEEDED IN PROJECT
 */
public class OptionsMenueScript : MonoBehaviour {

    private StaticMenueFunctions staticMenueFunctions;

    private void Start()
    {
        staticMenueFunctions = StaticMenueFunctions.GetInstance();
    }
    public void ChangeScene(string name)
    {
        staticMenueFunctions.ChangeScene(name);
    }

}
