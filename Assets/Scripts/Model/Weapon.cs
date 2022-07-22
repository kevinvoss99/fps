using UnityEngine;

namespace Assets.Scripts.Model
{
    /**
     * Diese abstrakte Klasse stellt eine Datenstruktur für Waffen zur Verfügung.
     */
    public abstract class Weapon
    {
        /**
         * Die aktuelle Munition im Magazin.
         */
        public int mCurrentMagazineAmmu { get; set; }
        /**
         * Reservemunition.
         */
        public int mCurrentOverallAmmu { get; set; }
        /**
         * Die Magazingröße.
         */
        public int mMagazineSize { get; set; }
        /**
         * Maximale Anzahl an Munition.
         */
        public int mMaxAmmu { get; set; }
        /**
         * Der Schaden, den die Waffe verursacht.
         */
        public int mDamage { get; set; }
        /**
         * Der Name der Waffe.
         */
        public string mName { get; set; }
        /**
         * Die Feuerrate der Waffe.
         */
        public float mFireRate { get; set; }
        /**
         * Angabe, ob sich die Waffe im Besitz des Spielers befindet.
         */
        public bool mInPossess { get; set; }
        /**
         * Das 3D-Modell (Prefab) zur Waffe.
         */
        public GameObject mWeaponModel { get; set; }
        
        /**
         * Der Spieler, für den die Waffen verwaltet werden.
         */
        protected GameObject mPlayer;

        /**
         * MaxAmmu wird so berechnet: Magazingröße * diesen Faktor.
         */
        public static readonly int AMMU_FACTOR = 4;

        /**
         * Einziger Konstruktor der Klasse, der alle Daten für eine Waffe erwartet.
         */
        public Weapon(GameObject player, string name, int damage, float fireRate, GameObject weaponModel, int magazineSize)
        {
            mPlayer = player;
            mName = name;
            mDamage = damage;
            mFireRate = fireRate;

            mWeaponModel = weaponModel;

            mMagazineSize = magazineSize;

            mMaxAmmu = mMagazineSize * AMMU_FACTOR;
            mCurrentOverallAmmu = mMaxAmmu;
            mCurrentMagazineAmmu = mMagazineSize;



        }
        /**
         * Abstrakte Methode zur Aktivierung der Waffe.
         */
        public abstract void Activate();
        /**
         * Abstrakte Methode zur Deaktivierung der Waffe.
         */
        public abstract void Deactivate();

        /**
         * Methode zum Nachladen der Waffe. Hier werden alle Berechnungen vorgenommen, die beim Nachladen notwendig sind.
         */
        public void Reload()
        {
            if (mCurrentOverallAmmu <= 0 || mCurrentMagazineAmmu == mMagazineSize)
            {
                return;
            }

            int tmpOverall = mCurrentOverallAmmu;
            int tmpMagazine = mCurrentMagazineAmmu;

            mCurrentOverallAmmu -= mMagazineSize - mCurrentMagazineAmmu;
            if (mCurrentOverallAmmu <= 0)
            {
                mCurrentOverallAmmu = 0;
                mCurrentMagazineAmmu = tmpOverall + tmpMagazine;
            }
            else
            {
                mCurrentMagazineAmmu = mMagazineSize;
            }
        }

        /**
         * Methode die zurückgibt, ob die Waffe leer ist.
         */
        public bool IsWeaponEmpty()
        {
            return mCurrentMagazineAmmu == 0 && mCurrentOverallAmmu == 0;
        }



    }
}
