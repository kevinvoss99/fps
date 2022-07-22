using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Scripts.Model;
using Assets.Scripts.Util;
using Assets.Scripts.CMAS;
using System.Collections;
using Assets.Scripts.AI;



/**
 * Dieses Skript stellt den zentralen Bezugspunkt des Programmes dar, an dem alle relevanten Daten gespeichert sind.
 */
public class GameControllerScript : MonoBehaviour
{

    /**
     * AgentController, der das Multiagentensystem startet und verwaltet.
     */
    public static AgentController mAgentController;

    /**
     * Liste, in der alle verfügbaren Namen für die Spieler gespeichert werden.
     */
    private List<string> mPlayerNames;

    /**
     * Referenz auf das GameMenueScript, welches sich um den pausierten Zustand des Spiels kümmert.
     */
    public GameMenueScript mGameMenueScript;
    /**
     * Referenz auf alle verfügbaren PlayerSpawnpoints.
     */
    public GameObject mSpawnPointObject;
    /**
     * Referenz auf alle verfügbaren WeaponSpawnpoints.
     */
    public GameObject mWeaponSpawnPoint;
    /**
     * Referenz auf alle verfügbaren HealthSpawnpoints.
     */
    public GameObject mHealthSpawnPoint;
    /**
     * Referenz auf alle verfügbaren AmmunitionSpawnpoints.
     */
    public GameObject mPickUpsSpawnPoints;

    /**
     * Boolean, ob gerade die Waffe gewechselt wird.
     */
    private bool mIsSwitching = false;

    /**
     * Variable, die den Timestamp beim Spielstart darstellt.
     */
    public static DateTime mGameStart { get; private set; }

    /**
     * Wird zur Identifizierung des CBR-Players verwendet.
     */
    private static int mSpawnCounter = 0;

    /**
     * Dieser Wert gibt an, wie viele Spieler am Spiel teilnehmen sollen. Standardwert: 2.
     * Theoretisch auch 1000 oder mehr Spieler möglich, allerdings ist der Prototyp dafür nicht ausgelegt und es gibt nicht genug Namen für 1000 Spieler.
     */
    public int NumberOfPlayers = 2;

    /**
     * Enum der den Status des Spiels darstellt (Laufend, Pausiert).
     */
    public enum GameState
    {
        PAUSED, RUNNING
    }

    /**
     * Dieser Wert gibt an, wie viele Sekunden vergehen, bis ein toter Spieler reanimiert wird (Standard: 3).
     */
    public static float mRespawnTime { get; private set; }

    /**
     * Status des Spiels.
     */
    public GameState mState;
    /**
     * GameObject des CBR Spielers (Prefab).
     */
    private GameObject mCBRPlayerGameObject;

    /**
     * GameObject des Trivial Spielers (Prefab).
    */
    private GameObject mTrivialPlayerGameObject;
    /**
     * GameObject der Spielerkamera (Prefab).
     */
    private GameObject mCameraObject;

    /**
     * Liste, die alle Spawnpoints der Munitionskiste beinhaltet.
     */
    public static List<Transform> mWeaponsCrateSpawnPoints { get; private set; }
    /**
     * Liste, die alle Spawnpoints des Herzcontainers beinhaltet.
     */
    public static List<Transform> mHealthSpawnPoints { get; private set; }
    /**
     * Liste, die alle Spawnpoints der Spieler beinhaltet.
     */
    public static List<Transform> mSpawnPoints { get; private set; }

    /**
     * Liste, die alle Spawnpoints der zweiten Waffe beinhaltet.
     */
    public static List<Transform> mM4a1SpawnPoints { get; private set; }

    /**
     * Liste, die alle Spieler beinhaltet.
     */
    public static List<Player> mPlayers { get; private set; }

    /**
     * Variable, welche die Information hält, wie viele Spieler am Spiel teilnehmen.
     */
    private int mPlayerCounter = 0;
    /**
     * Die Zuschauerkamera als Variable.
     */
    private Camera mSpectatorCamera;
    /**
     * GameObject der Zuschauerkamera (Prefab)
     */
    private GameObject mSpectatorCameraGameObject;
    /**
     * GameObject der Munitionskiste (Prefab)
     */
    private GameObject mWeaponCrateGameObject;
    /**
     * Der vom Menschen gesteuerte Spieler.
     */
    private Player mHumanControlled;
    /**
     * Die Reichweite der Waffen.
     */
    public float mRange { get; set; }
    /**
     * Instanziierung des Prefabs der Munitionskiste.
     */
    private GameObject mWeaponsCrateObject;
    /**
     * Prefab Objekt der zweiten Waffe.
     */
    private GameObject mM4a1GameObject;
    /**
     * Prefab Objekt der zweiten Waffe.
     */
    private GameObject mHeartGameObject;

    /**
   * Instanziierung des Prefabs der großen Munitionskiste.
   */
    private GameObject mAmmuLargeCrateObject;

    /**
     * Gibt an, ob die M4A1 eingesammelt ist (wichtig für Respawn).
     */
    public static bool mM4a1Collected = true;
    /**
     * Gibt an, ob die Munitionskiste eingesammelt ist (wichtig für Respawn).
     */
    public static bool mWeaponCrateCollected = true;
    /**
     * Gibt an, ob der Herzcontainer eingesammelt ist (wichtig für Respawn).
     */
    public static bool mHeartCollected = true;

    /**
    * Gibt an, ob die große Munitionskiste eingesammelt ist (wichtig für Respawn).
    */
    public static bool mAmmuLargeCrateCollected = true;

    /**
     * Der menschliche Spieler verfügt über ein HUD.
     */
    public static GameObject hudCanvas { get; private set; }

    /**
     * Zeit in Sekunden, bis eine neue Waffe spawnt.
     */
    private float mWeaponSpawnTimer = 60f;
    /**
     * Zeit in Sekunden, bis eine neue Munitionskiste spawnt.
     */
    private float mWeaponCrateTimer = 20f;
    /**
 * Zeit in Sekunden, bis eine neue große Munitionskiste spawnt.
 */
    private float mAmmuLargeCrateTimer = 20f;
    /**
     * Zeit in Sekunden, bis ein neuer Herzcontainer spawnt.
     */
    private float mHeartTimer  = 30f;

    /**
     * Dauer in Sekunden, die für den Nachladevorgang benötigt werden.
     */
    private float mReloadTimer = 1f;

    /**
     * Methode, die zu Beginn des Skripts ausgeführt wird und das Java-Programm, sowie den Agentencontroller inklusive der benötigten Agenten startet.
     */
    private void InitAgentsAndCBR()
    {
        bool withWindow = true;
        Constants.StartServer(withWindow);
        mAgentController = new AgentController();
        mAgentController.StartAgentPortal();
    }
    /**
     * Unity Methode, die beim Aufruf des Skripts *einmalig* ausgeführt wird.
     */
    private void Awake()
    {
        Constants.CreateFolderIfDoesNotExistYet(Constants.SAVES_PATH);
        InitAgentsAndCBR();
        MainMenueScript.OnlyBots = true;

        hudCanvas = GameObject.FindGameObjectWithTag("HUD");

        mGameStart = DateTime.Now;

        

        Debug.Log("Game start at: " + StaticMenueFunctions.GetTimeStampString(mGameStart));

        mRespawnTime = 7f;

        // Vergabe der Namen
        mPlayerNames = new List<string>();
        mPlayerNames.Add("CBR Player");
        mPlayerNames.Add("Trivial Player");


        // Zuordnung der Spielermodells
        mCBRPlayerGameObject = Resources.Load("Prefabs/PlayerCBR") as GameObject;
        mTrivialPlayerGameObject = Resources.Load("Prefabs/PlayerTrivial") as GameObject;

        // Spieler werden der Liste hinzugefügt
        mPlayers = new List<Player>();
        mPlayers.Add(new Player(mPlayerNames[0], mCBRPlayerGameObject));
        mPlayers.Add(new Player(mPlayerNames[1], mTrivialPlayerGameObject));

        mSpectatorCameraGameObject = Resources.Load("Prefabs/SpectatorCamera") as GameObject;
        mWeaponsCrateObject = Resources.Load("Prefabs/WeaponsCrate/WeaponsCrate") as GameObject;
        mAmmuLargeCrateObject = Resources.Load("Prefabs/AmmunitionLarge") as GameObject;
        
        mM4a1GameObject = Resources.Load("Prefabs/M4A1_Collectable") as GameObject;

        mM4a1SpawnPoints = new List<Transform>();

        for (int i = 0; i < mWeaponSpawnPoint.transform.childCount; i++)
        {
            mM4a1SpawnPoints.Add(mWeaponSpawnPoint.transform.GetChild(i));
        }

        mSpectatorCamera = mSpectatorCameraGameObject.GetComponent<Camera>();

        mSpawnPoints = new List<Transform>();

        for (int i = 0; i < mSpawnPointObject.transform.childCount; ++i)
        {
            mSpawnPoints.Add(mSpawnPointObject.transform.GetChild(i));
        }

        mHealthSpawnPoints = new List<Transform>();

        for (int i = 0; i < mHealthSpawnPoint.transform.childCount; i++)
        {
            mHealthSpawnPoints.Add(mHealthSpawnPoint.transform.GetChild(i));
        }


        mWeaponsCrateSpawnPoints = new List<Transform>();

        for (int i = 0; i < mPickUpsSpawnPoints.transform.childCount; ++i)
        {
            mWeaponsCrateSpawnPoints.Add(mPickUpsSpawnPoints.transform.GetChild(i));
        }

        Cursor.lockState = CursorLockMode.Locked;
        mState = GameState.RUNNING;
        mGameMenueScript.SetGameMenueToDefaultSettings();

        if (mSpectatorCamera != null && MainMenueScript.OnlyBots)
        {
            EnableSpectatorCamera();
        }

        foreach (Player player in mPlayers)
        {
            SpawnPlayer(player);
            player.OnPlayerClick += Clicked;
            mAgentController.AddAgent(player.mPlayerAgent);
            Constants.InitSaveFolderAndPlayerSaveFile(player, mGameStart);
        }
        mRange = 100f;
    }

    /**
     * Methode, die einen übergebenen Spieler in der Spielwelt starten lässt.
     */
    private void SpawnPlayer(Player player)
    {
        Transform spawnPoint = null;

        bool free = false;

        float timer = 0f;

        while (!free)
        {
            timer += Time.deltaTime;
            spawnPoint = mSpawnPoints[UnityEngine.Random.Range(0, mSpawnPoints.Count)];
            Vector3 spawnVector = spawnPoint.position;
            

            var hitColliders = Physics.OverlapSphere(spawnVector, 2);

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
            }
        }

        if (MainMenueScript.OnlyBots || !MainMenueScript.OnlyBots && mPlayerCounter == 1)
        {
            player.mIsHumanControlled = false;
        }
        else
        {
            player.mIsHumanControlled = true;
            mPlayerCounter++;
        }

        mCameraObject = Instantiate(player.mCamera.gameObject, spawnPoint.position, Quaternion.identity);
        player.mGameObject = Instantiate(player.mGameObject, spawnPoint.position, Quaternion.identity);
        player.mIdentifier = mSpawnCounter++;
        player.mBotBehaviour = player.mIdentifier >= 1 ? player.mGameObject.AddComponent<BotBehaviourScript>() : null;
        player.mCbrBehaviour = player.mIdentifier < 1 ? player.mGameObject.AddComponent<BotCBRBehaviourScript>() : null;
        player.InitGameObjectAndCamera();
        player.mGameObject.name = player.mName;

        mCameraObject.transform.parent = player.mGameObject.transform;
        mCameraObject.transform.position = mCameraObject.transform.position + new Vector3(0, 1, 0.5f);

        player.mPlayerShooting = player.mGameObject.AddComponent<PlayerShooting>();
        player.mPlayerShooting.mShootingPlayer = player;



        if (player.mIsHumanControlled)
        {
            mHumanControlled = player;
        }
    }


    /**
     * Delegate-Methode, um zu ermitteln, welcher Spieler beim entsprechenden Mausklick schießen soll.
     */
    public void Clicked(object sender, EventArgs e)
    {
        Player player = sender as Player;

        if (player.Equals(mHumanControlled) && !mIsSwitching)
        {
            player.Shoot();
        }
    }

    /**
     * Unity Methode
     */
    private void FixedUpdate()
    {
        if (MainMenueScript.OnlyBots && mState == GameState.RUNNING)
        {

            Player tmp = null;
            if (Input.GetKeyDown(KeyCode.F1))
            {
                tmp = mPlayers[0];
                DisableSpectatorCamera();
                tmp.ActivatePlayer();
                mHumanControlled = tmp;
                PlayerShooting.mHumanPlayer = mHumanControlled;

            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                tmp = mPlayers[1];
                DisableSpectatorCamera();
                tmp.ActivatePlayer();
                mHumanControlled = tmp;
                PlayerShooting.mHumanPlayer = mHumanControlled;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F12))
            {
                foreach (Player player in mPlayers)
                {
                    if (player.mIsHumanControlled)
                    {
                        player.DeactivatePlayer();
                        mHumanControlled = null;
                        PlayerShooting.mHumanPlayer = null;
                        break;
                    }
                }

                EnableSpectatorCamera();

            }
        }
    }

    /**
     * Methode, um die Zuschauerkamera auszuschalten.
     */
    private void DisableSpectatorCamera()
    {
        MainMenueScript.OnlyBots = false;

        mSpectatorCameraGameObject.GetComponent<SpectatorCameraScript>().enabled = false;
        mSpectatorCameraGameObject.GetComponent<PlayerPerspective>().enabled = false;
        mSpectatorCameraGameObject.SetActive(false);
        mSpectatorCameraGameObject.GetComponent<Camera>().enabled = false;

        Destroy(mSpectatorCameraGameObject);
        mSpectatorCameraGameObject = null;

        hudCanvas.SetActive(true);
    }
    /**
     * Methode, um die Zuschauerkamera einzuschalten.
     */
    private void EnableSpectatorCamera()
    {

        MainMenueScript.OnlyBots = true;

        mSpectatorCameraGameObject = Instantiate(mSpectatorCameraGameObject, new Vector3(0f, 15f, 0f), Quaternion.identity);
        mSpectatorCameraGameObject.name = "SpectatorCamera";

        mSpectatorCameraGameObject.GetComponent<SpectatorCameraScript>().enabled = true;
        mSpectatorCameraGameObject.GetComponent<PlayerPerspective>().enabled = true;
        mSpectatorCameraGameObject.SetActive(true);
        mSpectatorCameraGameObject.GetComponent<Camera>().enabled = true;
        hudCanvas.SetActive(false);
    }

    /**
     * Unity Methode.
     */
    private void Update()
    {

        if (mSpectatorCameraGameObject == null)
        {
            mSpectatorCameraGameObject = Resources.Load("Prefabs/SpectatorCamera") as GameObject;
        }

        if (Input.GetButton("Fire1"))
        {
            foreach (Player player in mPlayers)
            {
                player.OnClick();
            }
        }

        if (mWeaponCrateCollected)
        {
            StartCoroutine(SpawnWeaponsCrate());
        }

        if (mHeartCollected)
        {
            StartCoroutine(SpawnHealthContainer());
        }

        if (mM4a1Collected)
        {
            StartCoroutine(SpawnM4A1());
        }

        if (mAmmuLargeCrateCollected)
        {
            StartCoroutine(SpawnLargeAmmuCrate());
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (mHumanControlled != null)
            {
                Debug.Log(mHumanControlled.mName + " tries to switch weapon!");
                StartCoroutine(SwitchWeapon());
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mState == GameState.PAUSED)
            {
                ContinueGame(false);
                
            }
            else
            {
                PauseGame();                
            }

            mGameMenueScript.ToggleGameMenue();
        }

        if (Input.GetButtonDown("Tab")) // GetButton(...)
        {
            Debug.Log("SHOW STATISTICS");
            foreach (Player player in mPlayers)
            {
                Debug.Log(player.ToString());
            }
        }
    }
    
    /**
     * Subroutine, um die Waffe zu wechseln.
     */
    public IEnumerator SwitchWeapon()
    {
        mIsSwitching = true;
        yield return new WaitForSeconds(mReloadTimer);
        mHumanControlled.SwitchWeapon();
        mIsSwitching = false;
    }

    /**
     * Methode, um das Spiel und den Zeitfluss fortzusetzen.
     */
    public void ContinueGame(bool fromScene)
    {
        Time.timeScale = 1;
        mState = GameState.RUNNING;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (fromScene)
        {
            mGameMenueScript.ToggleGameMenue();
            UpTimeScript.pauseHit = false;


        }
    }
    /**
     * Methode, um das Spiel und den Zeitfluss zu stoppen.
     */
    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        mState = GameState.PAUSED;
        UpTimeScript.pauseHit = true;
    }

    /**
     * Diese Unity-Methode wird beim Verlassen des Programms ausgeführt. Hier wird die Java-Applikation beendet.
     */
    private void OnApplicationQuit()
    {
        Constants.proc.Kill();
    }

    /**
     * Methode, um einen Herzcontainer zu spawnen.
     */
    private IEnumerator SpawnHealthContainer()
    {
        mHeartCollected = false;
        Transform spawnPoint = null;

        bool free = false;

        while (!free)
        {
            spawnPoint = mHealthSpawnPoints[UnityEngine.Random.Range(0, mHealthSpawnPoints.Count)];
            Vector3 spawnVector = spawnPoint.position;

            var hitColliders = Physics.OverlapSphere(spawnVector, 2);

            int pCounter = 0;

            foreach (Collider collider in hitColliders)
            {
                if (collider.tag == "Health")
                {
                    pCounter++;
                }
            }

            if (pCounter <= 0)
            {
                free = true;
            }
        }

        yield return new WaitForSeconds(mHeartTimer);

        if (free)
        {
            Debug.Log("spawn health container");
            if (mHeartGameObject == null)
            {
                mHeartGameObject = Resources.Load("Prefabs/Health") as GameObject;
            }
            mHeartGameObject = Instantiate(mHeartGameObject, spawnPoint.position, Quaternion.identity);
            mHeartGameObject.AddComponent<HealthContainerScript>();
        }
    }

    /**
     * Methode, um eine Waffe zu spawnen.
     */
    private IEnumerator SpawnM4A1()
    {

        mM4a1Collected = false;
        Transform spawnPoint = null;

        bool free = false;

        while (!free)
        {
            spawnPoint = mM4a1SpawnPoints[UnityEngine.Random.Range(0, mM4a1SpawnPoints.Count)];
            Vector3 spawnVector = spawnPoint.position;

            var hitColliders = Physics.OverlapSphere(spawnVector, 2);

            int pCounter = 0;

            foreach (Collider collider in hitColliders)
            {
                if (collider.tag == "Machine Gun Collectable")
                {
                    pCounter++;
                }
            }

            if (pCounter <= 0)
            {
                free = true;
            }
        }

        yield return new WaitForSeconds(mWeaponSpawnTimer);

        if (free)
        {
            Debug.Log("spawn weapon");
            if (mM4a1GameObject == null)
            {
                mM4a1GameObject = Resources.Load("Prefabs/M4A1_Collectable") as GameObject;
            }
            mM4a1GameObject = Instantiate(mM4a1GameObject, spawnPoint.position, Quaternion.identity);
            mM4a1GameObject.AddComponent<M4A1Script>();
        }
    }

    /**
     * Methode, um eine Munitionskiste zu spawnen.
     */
    private IEnumerator SpawnWeaponsCrate()
    {
        mWeaponCrateCollected = false;
        Transform spawnPoint = null;

        bool free = false;

        while (!free)
        {
            spawnPoint = mWeaponsCrateSpawnPoints[UnityEngine.Random.Range(0, mWeaponsCrateSpawnPoints.Count)];
            Vector3 spawnVector = spawnPoint.position;

            var hitColliders = Physics.OverlapSphere(spawnVector, 2);

            int pCounter = 0;

            foreach (Collider collider in hitColliders)
            {
                if (collider.tag == "Ammunition")
                {
                    pCounter++;
                }
            }

            if (pCounter <= 0)
            {
                free = true;
            }
        }


        yield return new WaitForSeconds(mWeaponCrateTimer);

        if (free)
        {
            Debug.Log("create crate");
            mWeaponCrateGameObject = Instantiate(mWeaponsCrateObject, spawnPoint.position, Quaternion.identity);
            mWeaponCrateGameObject.AddComponent<AmmunitionScript>();
        }


    }

    /**
 * Methode, um eine große Munitionskiste zu spawnen.
 */
    private IEnumerator SpawnLargeAmmuCrate()
    {
        mAmmuLargeCrateCollected = false;
        Transform spawnPoint = null;

        bool free = false;

        while (!free)
        {
            spawnPoint = mWeaponsCrateSpawnPoints[UnityEngine.Random.Range(0, mWeaponsCrateSpawnPoints.Count)];
            Vector3 spawnVector = spawnPoint.position;

            var hitColliders = Physics.OverlapSphere(spawnVector, 2);

            int pCounter = 0;

            foreach (Collider collider in hitColliders)
            {
                if (collider.tag == "AmmunitionLarge")
                {
                    pCounter++;
                }
            }

            if (pCounter <= 0)
            {
                free = true;
            }
        }


        yield return new WaitForSeconds(mAmmuLargeCrateTimer);

        if (free)
        {
            Debug.Log("create Large Ammunition crate");
            if(mAmmuLargeCrateObject != null) {
                mAmmuLargeCrateObject = Instantiate(mAmmuLargeCrateObject, spawnPoint.position, Quaternion.identity);
                mAmmuLargeCrateObject.AddComponent<AmmunitionLargeScript>();
            }
           
        }


    }


}
