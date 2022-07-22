using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;
using Assets.Scripts.CBR.Plan;

public class AmmunitionLarge : MonoBehaviour
{
    /**
      * Diese Methode wird aufgerufen, wenn ein anderes Objekt, was �ber einen Collider verf�gt, mit diesem Collider kollidiert. Dann wird gepr�ft, ob der andere Collider zum Spieler geh�rt.
      * Geh�rt er zum Spieler, so wird �berpr�ft, ob der Spieler berechtigt ist, den Gegenstand aufzunehmen. Ist er es, so verschwindet das Gameobject und erscheint erst nach x Sekunden erneut.
      */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            foreach (Player player in GameControllerScript.mPlayers)
            {
                if (other.name.Equals(player.mName))
                {
                    int newAmmu = player.mEquippedWeapon.mCurrentOverallAmmu + (player.mEquippedWeapon.mMagazineSize * 3);
                    player.mEquippedWeapon.mCurrentOverallAmmu = newAmmu > player.mEquippedWeapon.mMaxAmmu ? player.mEquippedWeapon.mMaxAmmu : newAmmu;
                    if (player.mCBR)
                    {
                        for (int i = 0; i < player.mPlan.GetActionCount(); i++)
                        {
                            if (player.mPlan.actions[i].GetType() == typeof(CollectItem))
                            {
                                CollectItem cItem = (CollectItem)player.mPlan.actions[i];
                                if (cItem.destination.Contains("ammuLarge"))
                                {
                                    player.mPlan.actions[i].finished = true;
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
            }

            GameControllerScript.mAmmuLargeCrateCollected = true;

            Destroy(gameObject);
        }
    }
}
