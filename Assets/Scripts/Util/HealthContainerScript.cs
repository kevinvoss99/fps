using Assets.Scripts.CBR.Plan;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.Util
{
    /**
     * Dieses Skript übernimmt die Möglichkeit zum Einsammeln von Herzcontainern.
     */
    public class HealthContainerScript : MonoBehaviour
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
                        if (player.mPlayerHealth < Player.mMaxLife)
                        {
                            player.mPlayerHealth = Player.mMaxLife;
                            GameControllerScript.mHeartCollected = true;
                            Destroy(gameObject);
                        }

                        if (player.mCBR)
                        {
                            for (int i = 0; i < player.mPlan.GetActionCount(); i++)
                            {
                                if (player.mPlan.actions[i].GetType() == typeof(CollectItem))
                                {
                                    CollectItem cItem = (CollectItem)player.mPlan.actions[i];
                                    if (cItem.destination.Contains("health"))
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
            }
        }
    }
}
