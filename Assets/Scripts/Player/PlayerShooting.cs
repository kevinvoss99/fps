using UnityEngine;
using Assets.Scripts.Model;
using Assets.Scripts.Util;
using System.Collections;
using Assets.Scripts.CBR.Model;
using Assets.Scripts.CBR.Plan;

/**
 * Dieses Skript übernimmt den Vorgang des Schießens eines Spielers.
 */
public class PlayerShooting : MonoBehaviour
{
    public float mRange = 100f;

    private float mTimer;
    private Ray mShootRay = new Ray();
    private RaycastHit mShootHit;
    private int mShootableMask;
    private ParticleSystem mGunParticles;
    private LineRenderer mGunLine;
    private AudioSource mGunAudio;
    private Light mGunLight;
    private float mEffectsDisplayTime = 0.2f;
    private int mReloadSeconds = 2;
    private bool mIsReloading = false;

    private GameObject mSpectatorCameraGameObject;
    // Platzhalter für die Visuelle Schadensnahme
    private GameObject mBloodParticles;
    // Platzhalter für die akustische Schadensnahme
    private AudioSource mPlayerHurt;
    private AudioSource mZombHurt;

    public Player mShootingPlayer { get; set; }

    private PlayerHealth mPlayerHealthScript;

    public static Player mHumanPlayer;


    private void Awake()
    {
        mShootableMask = LayerMask.GetMask("Shootable");
    }

    private void Update()
    {

        if (mShootingPlayer != null)
        {
            InitComponents();
        }
        mTimer += Time.deltaTime;

        /*
         * 
        if (mHumanPlayer == null)
        {
            foreach (Player player in GameControllerScript.mPlayers)
            {
                if (player.mIsHumanControlled)
                {
                    mHumanPlayer = player;
                    break;
                }
            }
        }
        */


        if (mHumanPlayer != null && mPlayerHealthScript == null)
        {
            mPlayerHealthScript = mHumanPlayer.mGameObject.GetComponent<PlayerHealth>();
        }

        if (mHumanPlayer != null && mPlayerHealthScript != null)
        {
            mPlayerHealthScript.UpdateAmmunition(mHumanPlayer);
        }

        if (mShootingPlayer != null && mTimer >= mShootingPlayer.mEquippedWeapon.mFireRate * mEffectsDisplayTime)
        {
            DisableEffects();
        }

        if (mShootingPlayer != null && mShootingPlayer.mIsHumanControlled && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log(mShootingPlayer.mName + " reloads!");
            mIsReloading = true;
            StartCoroutine(ReloadRoutine(mShootingPlayer));

        }
    }

    private void InitComponents()
    {
        if (mGunLight == null)
        {
            mGunLight = StaticMenueFunctions.FindComponentInChildren<Light>(mShootingPlayer.mGameObject);
        }
        if (mGunParticles == null)
        {
            mGunParticles = StaticMenueFunctions.FindComponentInChildren<ParticleSystem>(mShootingPlayer.mGameObject);
        }
        if (mGunLine == null)
        {
            mGunLine = StaticMenueFunctions.FindComponentInChildren<LineRenderer>(mShootingPlayer.mGameObject);
        }
        if (mGunAudio == null)
        {
            mGunAudio = StaticMenueFunctions.FindComponentInChildren<AudioSource>(mShootingPlayer.mGameObject);
        }
        if (mBloodParticles == null)
        {
            mBloodParticles = Resources.Load("Prefabs/BloodParticle") as GameObject;
        }
        if (mZombHurt == null)
        {
            mZombHurt = (AudioSource)Resources.Load("Prefabs/PlayerTrivial/AudioSource");
        }
        if (mPlayerHurt == null)
        {
            mPlayerHurt = (AudioSource)Resources.Load("Prefabs/PlayerCBR/AudioSource");
        }

    }

    public void DisableEffects()
    {
        mGunLine.enabled = false;
        mGunLight.enabled = false;
    }

    public void Shoot()
    {

        if (mTimer >= mShootingPlayer.mEquippedWeapon.mFireRate && Time.timeScale != 0 && !mIsReloading)
        {
            if (DoShoot())
            {
                if (mHumanPlayer != null && mPlayerHealthScript != null)
                {
                    mPlayerHealthScript.UpdateAmmunition(mHumanPlayer);
                }
            }
            else
            {
                if (!mShootingPlayer.mIsHumanControlled)
                {
                    StartCoroutine(ReloadRoutine(mShootingPlayer));
                }
            }
        }

    }

    // Das Nachladen soll nicht sofort geschehen, sondern x Sekunden dauern, derzeit 2 Sekunden
    private IEnumerator ReloadRoutine(Player player)
    {
        yield return new WaitForSeconds(mReloadSeconds);
        Debug.Log(player.mName + " finished reloading!");
        mIsReloading = false;
        player.Reload();
    }

    public bool DoShoot()
    {

        if (mShootingPlayer.mEquippedWeapon.mCurrentMagazineAmmu <= 0)
        {
            return false;
        }

        mShootingPlayer.mEquippedWeapon.mCurrentMagazineAmmu -= 1;

        mTimer = 0f;

        mGunAudio.Play();

        mGunLight.enabled = true;

        mGunParticles.Stop();
        mGunParticles.Play();

        LineRenderer line = StaticMenueFunctions.FindComponentInChildren<LineRenderer>(mShootingPlayer.mGameObject);
        mGunLine.enabled = true;
        mGunLine.SetPosition(0, line.transform.position);



        mShootRay.origin = transform.position;
        mShootRay.direction = transform.forward;

        if (Physics.Raycast(mShootRay, out mShootHit, mRange, mShootableMask))
        {
            HitByRaycast(mShootHit.collider.gameObject);
            mGunLine.SetPosition(1, mShootHit.point);
        }
        else
        {
            mGunLine.SetPosition(1, mShootRay.origin + mShootRay.direction * mRange);
        }

        return true;
    }

    private void HitByRaycast(GameObject source)
    {
        foreach (Player hitPlayer in GameControllerScript.mPlayers)
        {
            if (source.name.Equals(hitPlayer.mName))
            {
                Debug.Log(hitPlayer.mName + " was hit by " + mShootingPlayer.mName);

                hitPlayer.TakeDamage(mShootingPlayer.mEquippedWeapon.mDamage);
                Instantiate(mBloodParticles, hitPlayer.mGameObject.transform.position, Quaternion.identity);
                if (hitPlayer.mName == "Trivial Player")
                {
                    mZombHurt = hitPlayer.mGameObject.GetComponent<AudioSource>();
                    mZombHurt.Play();

                }

                if (hitPlayer.mName == "CBR Player")
                {
                    mPlayerHurt = hitPlayer.mGameObject.GetComponent<AudioSource>();
                    mPlayerHurt.Play();
                }


                if (!hitPlayer.mIsHumanControlled)
                {
                    if (!hitPlayer.mStatus.isEnemyVisible)
                    {
                        CommonUnityFunctions.mRotationFinished = false;
                        CommonUnityFunctions.LookAround(hitPlayer);
                    }
                    else
                    {
                        CommonUnityFunctions.mRotationFinished = false;
                        CommonUnityFunctions.LookAt(hitPlayer, mShootingPlayer);
                    }

                }


                Debug.Log(hitPlayer.mName + " has left " + hitPlayer.mPlayerHealth + "/" + Player.mMaxLife + " life points");

                hitPlayer.mIsAlive = hitPlayer.mPlayerHealth > 0;

                if (hitPlayer.mIsHumanControlled && GameControllerScript.hudCanvas.activeSelf)
                {
                    mPlayerHealthScript = hitPlayer.mGameObject.GetComponent<PlayerHealth>();

                    if (mPlayerHealthScript != null)
                    {
                        mPlayerHealthScript.damaged = true;
                        mPlayerHealthScript.TakeDamage(hitPlayer);
                    }

                }

                if (!hitPlayer.mIsAlive)
                {
                    hitPlayer.mStatus = new Status();
                    hitPlayer.mStatistics.AddDeath(new Death());
                    mShootingPlayer.mStatistics.AddFrag(new Frag());

                    mShootingPlayer.mStatus.enemiesLastKnownPosition = new Vector3(10000f, 10000f, 10000f);

                    Constants.SaveDeath(hitPlayer, GameControllerScript.mGameStart);
                    Constants.SaveFrag(mShootingPlayer, GameControllerScript.mGameStart);

                    if (mShootingPlayer.mCBR)
                    {
                        mShootingPlayer.mPlan.progress = (int)Plan.Progress.DONE;
                        mShootingPlayer.mStatus.isEnemyVisible = false;
                        mShootingPlayer.mStatus.isEnemyAlive = false;
                        mShootingPlayer.mStatus.lastPosition = (int)Status.LastPosition.unknown;
                        mShootingPlayer.mStatus.distanceToEnemy = (int)Status.Distance.unknown;

                        for (int i = 0; i < mShootingPlayer.mPlan.GetActionCount(); i++)
                        {
                            mShootingPlayer.mPlan.GetActionByIndex(i).finished = true;
                        }

                    }
                    Debug.Log(hitPlayer.mName + " was killed by " + mShootingPlayer.mName + " after " + (hitPlayer.mStatistics.GetLatestDeath().mTimestamp - GameControllerScript.mGameStart));
                    hitPlayer.mGameObject.SetActive(false);

                    if (!MainMenueScript.OnlyBots && hitPlayer.mIsHumanControlled)
                    {
                        EnableSpectatorCamera();
                    }

                    StartCoroutine(DelayedRespawn(hitPlayer));
                }
                break;
            }
        }
    }

    private void DeactivateShooting(Player player)
    {
        for (int i = 0; i < player.mPlan.GetActionCount(); i++)
        {
            Debug.Log("PlayerShooting#DeactivateShooting#for (int i = " + i + "; " + i + " < " + player.mPlan.GetActionCount() + "; " + i + "++)");
            player.mPlan.actions[i].finished = true;
        }
    }

    private void EnableSpectatorCamera()
    {
        mSpectatorCameraGameObject = Resources.Load("Prefabs/SpectatorCamera") as GameObject;
        mSpectatorCameraGameObject = Instantiate(mSpectatorCameraGameObject, new Vector3(0f, 15f, 0f), Quaternion.identity);
        mSpectatorCameraGameObject.name = "SpectatorCamera";

        mSpectatorCameraGameObject.GetComponent<SpectatorCameraScript>().enabled = true;
        mSpectatorCameraGameObject.GetComponent<PlayerPerspective>().enabled = true;
        mSpectatorCameraGameObject.SetActive(true);
        mSpectatorCameraGameObject.GetComponent<Camera>().enabled = true;
        GameControllerScript.hudCanvas.SetActive(false);
    }

    private void DisableSpectatorCamera()
    {
        if (mSpectatorCameraGameObject != null)
        {
            mSpectatorCameraGameObject.GetComponent<SpectatorCameraScript>().enabled = false;
            mSpectatorCameraGameObject.GetComponent<PlayerPerspective>().enabled = false;
            mSpectatorCameraGameObject.SetActive(false);
            mSpectatorCameraGameObject.GetComponent<Camera>().enabled = false;

            GameControllerScript.hudCanvas.SetActive(true);

            Destroy(mSpectatorCameraGameObject);
            mSpectatorCameraGameObject = null;

        }

    }

    private IEnumerator DelayedRespawn(Player player)
    {
        yield return new WaitForSeconds(GameControllerScript.mRespawnTime);

        bool free = false;

        while (!free)
        {
            Transform spawnPoint = GameControllerScript.mSpawnPoints[Random.Range(0, GameControllerScript.mSpawnPoints.Count)];
            Vector3 spawnVector = spawnPoint.position;

            if(player.mCBR)
            {
                UpTimeScript.pauseHit = false;
                WinChanceScript.cbrWeapon = 1;
            }

            if(!(player.mCBR || player.mIsHumanControlled))
            {
                WinChanceScript.kiWeapon = 1;
            }

            var hitColliders = Physics.OverlapSphere(spawnVector, 2);

            if (hitColliders.Length > 0)
            {

                int pCounter = 0;

                foreach (Collider collider in hitColliders)
                {
                    if (collider.tag == "Player")
                    {
                        pCounter++;
                    }
                }

                if (pCounter <= 0)
                {
                    free = true;
                    player.mGameObject.transform.position = spawnPoint.position;
                    player.mGameObject.transform.rotation = Quaternion.identity;

                    if (mPlayerHealthScript != null)
                    {
                        mPlayerHealthScript.ResetValues();
                    }
                }

            }
        }





        if (!MainMenueScript.OnlyBots)
        {
            DisableSpectatorCamera();
        }

        player.Init();
        player.mGameObject.SetActive(true);

    }
}