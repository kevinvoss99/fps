using System;
using Assets.Scripts.Util;
using UnityEngine;
using Assets.Scripts.Model;
using Assets.Scripts.CBR.Model;

namespace Assets.Scripts.AI
{

    /**
     * 
     * Diese Klasse stellt die triviale KI für einen computergesteuerten Spieler zur Verfügung.
     * 
     */
    public class BotBehaviourScript : MonoBehaviour
    {

        /*
         * Für eine KI, die mehrere Gegner berücksichtigen kann, muss hier eine Liste mit allen Gegnern verwendet werden.
         * /
        /**
         * Der Spieler ohne CBR-System.
         */
        private Player mPlayerWithoutCBR;
        /**
         * Der Gegner des Spielers.
         */
        private Player mEnemy;

        /**
         * Variable, welche die vergangene Zeit speichert. Es sollen nur alle x Sekunden Aktionen durchgeführt werden.
         */
        private float mTimer = 0f;
        /**
         * Timer, wie lange der Bot schon in der Mitte der Karte steht.
         */
        private float mCenterStandingTimer = 0f;
        /**
         * Timer, wie lange der Bot allgemein schon steht
         */
        private float mStandingTimer = 0f;
        /**
         * Der Bot darf maximal 3 Sekunden rumstehen.
         */
        private const float mMaximumgStandingTime = 3f;
        /**
         * Da der Bot nicht am exakten Nullpunkt (0, 0, 0) stehen kann, muss da eine gewisse Karenz gewährt werden. Also (+-5, 0, +-5) zälht auch als "Mitte".
         */
        private const float mMidDistance = 5f;

        /**
         * Ziel erreicht?
         */
        bool mDestinationReached = false;


        /**
         * 
         * Unity Methode
         */
        private void Update()
        {

            mTimer += Time.deltaTime;


            if (mPlayerWithoutCBR == null || mEnemy == null)
            {
                AssignPlayers();
            }

            if (mTimer >= 0.3f)
            {
                mTimer = 0f;
                if (mEnemy != null && mPlayerWithoutCBR != null)
                {
                    // Analysiere die Umgebung und führe dementsprechend Aktionen durch.
                    mPlayerWithoutCBR.mStatus = CommonUnityFunctions.GetStatus(mPlayerWithoutCBR, mEnemy, mPlayerWithoutCBR.mStatus);

                    bool collectWeapon = mPlayerWithoutCBR.mStatus.isWeaponNeeded && mPlayerWithoutCBR.mStatus.weaponPosition.x != 10000f && mPlayerWithoutCBR.mStatus.weaponDistance <= (int)Status.WeaponDistance.far;
                    bool collectHealth = mPlayerWithoutCBR.mStatus.isHealthNeeded && mPlayerWithoutCBR.mStatus.healthPosition.x != 10000f && mPlayerWithoutCBR.mStatus.healthDistance <= (int)Status.HealthDistance.far;
                    bool collectAmmunition = mPlayerWithoutCBR.mStatus.isAmmunitionNeeded && mPlayerWithoutCBR.mStatus.ammuPosition.x != 10000f && mPlayerWithoutCBR.mStatus.ammunitionDistance <= (int)Status.AmmunitionDistance.far;
                    bool collectAmmunitionLarge =  mPlayerWithoutCBR.mStatus.ammuLargePosition.x != 10000f && mPlayerWithoutCBR.mStatus.ammunitionLargeDistance <= (int)Status.AmmunitionLargeDistance.far;
                    bool isGadgetNeeded = mPlayerWithoutCBR.mStatus.isGadgetNeeded;


                    if (mPlayerWithoutCBR.mStatus.isEnemyVisible)
                    {
                        if ((mPlayerWithoutCBR.GetWeaponCount() == 2 && !mPlayerWithoutCBR.mEquippedWeapon.mName.Equals("Machine Gun")) || (mPlayerWithoutCBR.GetWeaponCount() == 2 && mPlayerWithoutCBR.mEquippedWeapon.IsWeaponEmpty()))
                        {
                            mPlayerWithoutCBR.SwitchWeapon();
                        }

                        if (mPlayerWithoutCBR.mStatus.distanceToEnemy == (int)Status.Distance.middle && CommonUnityFunctions.EnemyInShootingLine(mPlayerWithoutCBR, mEnemy))
                        {
                            CommonUnityFunctions.LookAt(mPlayerWithoutCBR, mEnemy);

                            mPlayerWithoutCBR.Shoot();
                        }
                        else if (mPlayerWithoutCBR.mStatus.distanceToEnemy <= (int)Status.Distance.near)
                        {
                            CommonUnityFunctions.LookAt(mPlayerWithoutCBR, mEnemy);
                            mPlayerWithoutCBR.Shoot();
                        }
                        else
                        {
                            mDestinationReached = CommonUnityFunctions.DestinationReached(mPlayerWithoutCBR);

                            if (CommonUnityFunctions.EnemyInShootingLine(mPlayerWithoutCBR, mEnemy))
                            {
                                CommonUnityFunctions.mRotationFinished = false;
                                CommonUnityFunctions.RotateTowards(mPlayerWithoutCBR, mEnemy.mGameObject.transform.position);
                            }
                            else if (CommonUnityFunctions.DestinationReached(mPlayerWithoutCBR))
                            {
                                CommonUnityFunctions.mRotationFinished = true;
                            }

                            Transform enemyPosition = CommonUnityFunctions.GetEnemyPosition(mPlayerWithoutCBR);
                            CommonUnityFunctions.MoveTo(mPlayerWithoutCBR, enemyPosition == null ? mEnemy.mGameObject.transform.position : enemyPosition.position, CommonUnityFunctions.NORMAL_STOPPING_DISTANCE);

                        }
                    }
                    else if (mPlayerWithoutCBR.mStatus.enemiesLastKnownPosition.x != 10000f && !mDestinationReached)
                    {
                        CommonUnityFunctions.mRotationFinished = false;
                        CommonUnityFunctions.MoveTo(mPlayerWithoutCBR, mPlayerWithoutCBR.mStatus.enemiesLastKnownPosition, CommonUnityFunctions.NORMAL_STOPPING_DISTANCE);
                        mDestinationReached = CommonUnityFunctions.DestinationReached(mPlayerWithoutCBR);

                        CommonUnityFunctions.LookAround(mPlayerWithoutCBR);

                        if (!isGadgetNeeded)
                        {
                            mPlayerWithoutCBR.PlaceClaymore();
                        }
                    }
                    else
                    {
                        if (collectAmmunition)
                        {
                            CommonUnityFunctions.mRotationFinished = false;
                            CommonUnityFunctions.MoveTo(mPlayerWithoutCBR, mPlayerWithoutCBR.mStatus.ammuPosition, 0);
                        }
                        if (collectAmmunitionLarge)
                        {
                            CommonUnityFunctions.mRotationFinished = false;
                            CommonUnityFunctions.MoveTo(mPlayerWithoutCBR, mPlayerWithoutCBR.mStatus.ammuLargePosition, 0);
                        }
                        else if (collectHealth)
                        {
                            CommonUnityFunctions.mRotationFinished = false;
                            CommonUnityFunctions.MoveTo(mPlayerWithoutCBR, mPlayerWithoutCBR.mStatus.healthPosition, 0);
                        }
                        else if (collectWeapon)
                        {
                            CommonUnityFunctions.mRotationFinished = false;
                            CommonUnityFunctions.MoveTo(mPlayerWithoutCBR, mPlayerWithoutCBR.mStatus.weaponPosition, 0);
                        }
                        else
                        {
                            bool isInCenter = (mPlayerWithoutCBR.mGameObject.transform.position.x <= mMidDistance && mPlayerWithoutCBR.mGameObject.transform.position.x >= -mMidDistance) && (mPlayerWithoutCBR.mGameObject.transform.position.z <= mMidDistance && mPlayerWithoutCBR.mGameObject.transform.position.z >= -mMidDistance);
                            bool isInOtherPosition = (mPlayerWithoutCBR.mGameObject.transform.position.x <= 20 && mPlayerWithoutCBR.mGameObject.transform.position.x >= 10) && (mPlayerWithoutCBR.mGameObject.transform.position.z <= 20 && mPlayerWithoutCBR.mGameObject.transform.position.z >= 10);

                            if (isInCenter)
                            {
                                mCenterStandingTimer += Time.deltaTime * 15;
                            }
                            if (isInOtherPosition)
                            {
                                mStandingTimer += Time.deltaTime * 15;
                            }


                            if (mCenterStandingTimer <= mMaximumgStandingTime || mStandingTimer > mMaximumgStandingTime)
                            {
                                mStandingTimer = 0f;
                                CommonUnityFunctions.mRotationFinished = false;
                                CommonUnityFunctions.MoveTo(mPlayerWithoutCBR, new Vector3(0f, 0f, 0f), 0);
                                CommonUnityFunctions.LookAround(mPlayerWithoutCBR);
                            }
                            else if (mStandingTimer <= mMaximumgStandingTime || mCenterStandingTimer > mMaximumgStandingTime)
                            {
                                CommonUnityFunctions.mRotationFinished = false;
                                CommonUnityFunctions.MoveTo(mPlayerWithoutCBR, new Vector3(15f, 0f, 15f), 0);
                                CommonUnityFunctions.LookAround(mPlayerWithoutCBR);

                                if (CommonUnityFunctions.DestinationReached(mPlayerWithoutCBR))
                                {
                                    mCenterStandingTimer = 0f;
                                }
                            }
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
            mEnemy = playerTuple.Item1;
            mPlayerWithoutCBR = playerTuple.Item2;
        }



    }
}