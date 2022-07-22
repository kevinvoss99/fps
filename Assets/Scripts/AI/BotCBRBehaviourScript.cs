using System;
using Assets.Scripts.Model;
using Assets.Scripts.CBR.Model;
using Assets.Scripts.CBR.Plan;
using Assets.Scripts.Util;

using UnityEngine;

namespace Assets.Scripts.AI
{
    /**
     * 
     * Diese Klasse stellt die KI für einen computergesteuerten Spieler, der auf das CBR-System zugreift, zur Verfügung.
     * 
     */
    public class BotCBRBehaviourScript : MonoBehaviour
    {
        /*
         * Für eine KI, die mehrere Gegner berücksichtigen kann, muss hier eine Liste mit allen Gegnern verwendet werden.
         * /
        /**
         * Der Spieler mit CBR-System.
         */
        private Player mPlayerWithCBR;
        /**
         * Der Gegner des Spielers.
         */
        private Player mEnemy;

        /**
         * Variable, welche die vergangene Zeit speichert. Es sollen nur alle x Sekunden Aktionen durchgeführt werden.
         */
        private float mTimer = 0f;

        /**
         * Variable, die benötigt wird, um die Anzahl an Anfragen zu zählen.
         */
        private int mCounter = 0;

        /**
         * Stellt der Agent zum ersten Mal eine Anfrage? Dies ist für den Programmfluss entscheident.
         */
        public static bool mFirstTime = true;

        /**
         * Variable, die angibt, nach welcher Zeitspanne frühestens eine neue Anfrage gestellt werden kann.
         */
        private float mCbrInterval = 0.3f;

        /**
         * Variable, die angibt, nach welcher Anzahl frühestens eine neue Anfrage gestellt werden kann.
         */
        private int mCurrentPlanCounter = 0;

        /**
         * Wird aktuelle eine Anfrage verarbeitet?
         */
        public static bool mIsRequesting = false;

        /**
         * Unity Methode
         */
        private void Update()
        {
            mTimer += Time.deltaTime;
            if (mEnemy == null || mPlayerWithCBR == null)
            {
                AssignPlayers();
            }

            if (mPlayerWithCBR != null && mTimer >= mCbrInterval && Time.timeScale != 0)
            {
                if (mPlayerWithCBR.mEquippedWeapon.mName.Equals("Pistol") && mPlayerWithCBR.GetWeaponCount() == 2)
                {
                    mPlayerWithCBR.SwitchWeapon();
                }

                mTimer = 0f;
                if (mFirstTime && mCounter++ == 0)
                {
                    mPlayerWithCBR.mStatus = CommonUnityFunctions.GetStatus(mPlayerWithCBR, mEnemy, mPlayerWithCBR.mStatus);
                    mPlayerWithCBR.mPlayerAgent.SendStringMessage(Constants.COMMUNICATION_AGENT_NAME, JsonParser<Request>.SerializeObject(new Request(new Situation(mPlayerWithCBR.mName, mPlayerWithCBR.mStatus))));
                    mIsRequesting = true;
                }

                if (mPlayerWithCBR.mPlan != null)
                {
                    Status stat = new Status();
                    Debug.Log("BotCBRBehaviourScript#Update#Hole neuen Status");
                    stat = CommonUnityFunctions.GetStatus(mPlayerWithCBR, mEnemy, stat);
                 
                    Debug.Log("BotCBRBehaviourScript#Update#Status geholt!");

                    if ((mPlayerWithCBR.mPlan.progress == (int)Plan.Progress.DONE || mPlayerWithCBR.mPlan.progress == (int)Plan.Progress.NOT_STARTED))
                    {
                        mCurrentPlanCounter = 0;
                        Debug.Log("if ((mPlayerWithCBR.mPlan.progress == (int)Plan.Progress.DONE || mPlayerWithCBR.mPlan.progress == (int)Plan.Progress.NOT_STARTED))");
                        if (!mFirstTime)
                        {
                            Debug.Log("BotCBRBehaviourScript#Update#Sende Nachricht an Kommunikationsagenten!");
                            mPlayerWithCBR.mPlayerAgent.SendStringMessage(Constants.COMMUNICATION_AGENT_NAME, JsonParser<Request>.SerializeObject(new Request(new Situation(mPlayerWithCBR.mName, mPlayerWithCBR.mStatus))));
                            mIsRequesting = true;
                            Debug.Log("BotCBRBehaviourScript#Update#Nachricht ist gesendet!");
                        }

                        while (mIsRequesting) ;

                        Debug.Log("BotCBRBehaviourScript#Update#Führe Plan aus!");
                        CommonUnityFunctions.ExecutePlan(mPlayerWithCBR, mEnemy, mPlayerWithCBR.mStatus);

                    }
                    else if (mPlayerWithCBR.mPlan.progress == (int)Plan.Progress.IN_PROGRESS)
                    {

                        
                        Debug.Log("else if (mPlayerWithCBR.mPlan.progress == (int)Plan.Progress.IN_PROGRESS)");
                        if (mPlayerWithCBR.mStatus.Equals(stat) && mPlayerWithCBR.mStatus.isEnemyAlive) // Situation hat sich nicht geändert - führe Plan weiterhin aus                     
                        {
                            
                            if (mCurrentPlanCounter >= 10)
                            {
                                // Plan wurde zu oft ausgeführt - gehe zur Mitte und schaue dich um
                                CommonUnityFunctions.MoveTo(mPlayerWithCBR, new Vector3(0f, 0f, 0f), 0);
                                CommonUnityFunctions.LookAround(mPlayerWithCBR);
                            } else
                            {
                                // Plan wurde nicht oft genug ausgeführt - führe Plan weiterhin aus
                                mCurrentPlanCounter++;
                                CommonUnityFunctions.ExecutePlan(mPlayerWithCBR, mEnemy, mPlayerWithCBR.mStatus);
                                mPlayerWithCBR.mStatus.upTime = 0;
                            }
                            
                        }
                        else
                        {
                            // Situation hat sich geändert - hole neuen Plan
                            mPlayerWithCBR.mStatus = stat;
                            mPlayerWithCBR.mPlan.progress = (int)Plan.Progress.DONE;
                        }
                    }
                }
            }

        }

        /**
         * Diese Methode ordnet die vorhandenen Spieler korrekt zu.
         */
        private void AssignPlayers()
        {
            Tuple<Player, Player> playerTuple = CommonUnityFunctions.GetBotPlayersCorrectly();
            mPlayerWithCBR = playerTuple.Item1;
            mEnemy = playerTuple.Item2;
        }
    }
}
