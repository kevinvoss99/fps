using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;

public class PlayerPlaceGadget : MonoBehaviour
{
    public static GameObject claymoreGameObject;


    public static void placeGadget(Vector3 mPlayerPosition)
    {
        claymoreGameObject = Resources.Load("Prefabs/Claymore") as GameObject;
        claymoreGameObject = Instantiate(claymoreGameObject, new Vector3(mPlayerPosition.x+2, -9f, mPlayerPosition.z+2), Quaternion.identity);
        claymoreGameObject.AddComponent<Claymore>();
    }
}
