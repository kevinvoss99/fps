using Assets.Scripts.CBR.Plan;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.Util
{
    /**
     * Dieses Skript übernimmt die Möglichkeit zum Einsammeln von Waffen.
     */
    public class M4A1Script : MonoBehaviour
    {
        /**
         * Diese Methode wird aufgerufen, wenn ein anderes Objekt, was über einen Collider verfügt, mit diesem Collider kollidiert. Dann wird geprüft, ob der andere Collider zum Spieler gehört.
         * Gehört er zum Spieler, so wird überprüft, ob der Spieler berechtigt ist, den Gegenstand aufzunehmen. Ist er es, so verschwindet das Gameobject und erscheint erst nach x Sekunden erneut.
         */
        private void OnTriggerEnter(Collider other)
        {

            if (other.tag.Equals("Player"))
            {
                foreach (Player player in GameControllerScript.mPlayers)
                {
                    if (other.name.Equals(player.mName))
                    {
                        foreach (Weapon weapon in player.GetWeapons())
                        {
                            if (weapon.mName == "Machine Gun" && weapon.mInPossess)
                            {
                                return;
                            }
                        }
                        Debug.Log(player.mName + " has collected the M4A1");
                        player.AddWeapon(new MachineGun(player.mGameObject));

                        if (player.mCBR)
                        {
                            for (int i = 0; i < player.mPlan.GetActionCount(); i++)
                            {
                                if (player.mPlan.actions[i].GetType() == typeof(CollectItem))
                                {
                                    CollectItem cItem = (CollectItem)player.mPlan.actions[i];
                                    if (cItem.destination.Contains("weapon"))
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

                GameControllerScript.mM4a1Collected = true;

                Destroy(gameObject);
            }
        }
    }
}
