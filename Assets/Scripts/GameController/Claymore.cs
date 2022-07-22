using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.CBR.Model;
using Assets.Scripts.CBR.Plan;
using Assets.Scripts.Model;
using Assets.Scripts.Util;


//player.mPlayerShooting = player.mGameObject.AddComponent<PlayerShooting>();
//player.mPlayerShooting.mShootingPlayer = player;


public class Claymore : MonoBehaviour
{

    private GameObject mSpectatorCameraGameObject;

    public Player mPlaceGadgetPlayer { get; set; }

    public Player mCbrPlayer { get; set; }

    private PlayerHealth mPlayerHealthScript;

    public static Player mHumanPlayer;

    // Platzhalter für die Visuelle Schadensnahme
    private GameObject mBloodParticles;
    // Platzhalter für die Visuelle Schadensnahme Claymore
    private GameObject mClaymoreParticles;


    /**
     * Diese Methode wird aufgerufen, wenn ein anderes Objekt, was über einen Collider verfügt, mit diesem Collider kollidiert. Dann wird geprüft, ob der andere Collider zum Spieler gehört.
     * Gehört er zum Spieler, so wird dem Spieler den Schaden der Claymore zugefügt.
     */
    private void OnTriggerEnter(Collider source)
    {
        // ersetzen wenn funktionalität implementiert wird
       // mPlaceGadgetPlayer = GameControllerScript.mPlayers[0];

        mCbrPlayer = GameControllerScript.mPlayers[0];

        if (mBloodParticles == null)
        {
            mBloodParticles = Resources.Load("Prefabs/BloodParticle") as GameObject;
        }
        if (mClaymoreParticles == null)
        {
            mClaymoreParticles = Resources.Load("Prefabs/ClaymoreExplosion") as GameObject;
        }


        foreach (Player hitPlayer in GameControllerScript.mPlayers)
        {
            if (source.name.Equals(hitPlayer.mName))
            {
                Debug.Log(hitPlayer.mName + " was hit by Claymore");

                //hitPlayer.TakeDamage(110);
                hitPlayer.mPlayerHealth = 5;

                Instantiate(mClaymoreParticles, hitPlayer.mGameObject.transform.position, Quaternion.identity);
                Instantiate(mBloodParticles, hitPlayer.mGameObject.transform.position, Quaternion.identity);
                

                Debug.Log(hitPlayer.mName + " has left " + hitPlayer.mPlayerHealth + "/" + Player.mMaxLife + " life points");



               /// hitPlayer.mIsAlive = hitPlayer.mPlayerHealth > 0;

                if (hitPlayer.mIsHumanControlled && GameControllerScript.hudCanvas.activeSelf)
                {
                    mPlayerHealthScript = hitPlayer.mGameObject.GetComponent<PlayerHealth>();

                    if (mPlayerHealthScript != null)
                    {
                        mPlayerHealthScript.damaged = true;
                        mPlayerHealthScript.TakeDamage(hitPlayer);
                    }

                }

              
            }
        }
        Destroy(transform.parent.gameObject);
    }

}