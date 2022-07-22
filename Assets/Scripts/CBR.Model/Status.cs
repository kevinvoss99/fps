using System.Runtime.Serialization;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.CBR.Model
{
    [DataContract]
    public class Status
    {

        /**
	     * enum zur Darstellung der aktuellen Munition.
	     */
        public enum CurrentAmmunition
        {
            full = (1 << 0), much = (1 << 1), middle = (1 << 2), few = (1 << 3), empty = (1 << 4)
        }
        /**
	     * enum zur Darstellung der Reservemunition.
	     */
        public enum CurrentOverallAmmunition
        {
            full = (1 << 0), much = (1 << 1), middle = (1 << 2), few = (1 << 3), empty = (1 << 4)
        }
        /**
	     * enum zur Darstellung der Distanz zum Gegner.
	     */
        public enum Distance
        {
            near = (1 << 0), middle = (1 << 1), far = (1 << 2), unknown = (1 << 3)
        }
        /**
         * enum zur Darstellung der eigenen Gesundheit.
	     */
        public enum OwnHealth
        {
            full = (1 << 0), much = (1 << 1), middle = (1 << 2), few = (1 << 3), critical = (1 << 4)
        }
        /**
	     * enum zur Darstellung der Distanz zur letzten bekannten Position des
	     * Gegners.
	     */
        public enum LastPosition
        {
            near = (1 << 0), middle = (1 << 1), far = (1 << 2), unknown = (1 << 3)
        }
        /**
	     * enum zur Darstellung der Waffendistanz.
	     */
        public enum WeaponDistance
        {
            near = (1 << 0), middle = (1 << 1), far = (1 << 2), unknown = (1 << 3)
        }
        /**
	     * enum zur Darstellung der Munitionsdistanz.
	     */
        public enum AmmunitionDistance
        {
            near = (1 << 0), middle = (1 << 1), far = (1 << 2), unknown = (1 << 3)
        }
        /**
	     * enum zur Darstellung der Gesundheitsdistanz.
	     */
        public enum HealthDistance
        {
            near = (1 << 0), middle = (1 << 1), far = (1 << 2), unknown = (1 << 3)
        }
        /**
	     * enum zur Darstellung der Distanz zur n&auml;chsten Deckung.
	     */
        public enum CoverDistance
        {
            near = (1 << 0), middle = (1 << 1), far = (1 << 2), unknown = (1 << 3)
        }

        public enum CurrentWinChance
        {
            winning = (1 << 0), equal = (1 << 1), loosing = (1 << 2)
        }

        public enum KillDeathRatio
        {
            positive = (1 << 0), equal = (1 << 1), negative = (1 << 2)
        }

        public enum UpTime
        {
            longer = (1 << 0), equal = (1 << 1), shorter = (1 << 2)
        }
        /**
      * enum zur Darstellung der Distanz zur n&auml;chsten bewegenden Deckung.
      */
        public enum MovingCoverDistance
        {
            near = (1 << 0), middle = (1 << 1), far = (1 << 2), unknown = (1 << 3)
        }
        /**
      * enum zur Darstellung der großen Munitionsdistanz.
      */
        public enum AmmunitionLargeDistance
        {
            near = (1 << 0), middle = (1 << 1), far = (1 << 2), unknown = (1 << 3)
        }

        /**
         * Vektor zur Speicherung der letzten bekannten Position des Gegners.
         */
        public Vector3 enemiesLastKnownPosition { get; set; }
        /**
         * Vektor zur Speicherung der Position der Munitionskiste.
         */
        public Vector3 ammuPosition { get; set; }
        /**
         * Vektor zur Speicherung der Position des Herzcontainers.
         */
        public Vector3 healthPosition { get; set; }
        /**
         * Vektor zur Speicherung der Position der Waffe.
         */
        public Vector3 weaponPosition { get; set; }
        /**
       * Vektor zur Speicherung der Position der großen Munitionskiste.
       */
        public Vector3 ammuLargePosition { get; set; }
    

        /**
	     * Attribut das speichert, ob der Gegner sichtbar ist.
	     */
        [DataMember]
        public bool isEnemyVisible { get; set; }
        /**
	     * Attribut, das die Distanz zum Gegner speichert.
	     */
        [DataMember]
        public int distanceToEnemy { get; set; }
        /**
	     * Attribut, das die Entfernung zur letzten bekannten Position des Gegners
	     * darstellt.
	     */
        [DataMember]
        public int lastPosition { get; set; }
        /**
	     * Attribut das speichert, ob der Gegner am Leben ist.
	     */
        [DataMember]
        public bool isEnemyAlive { get; set; }
        /**
	     * Attribut, das die eigene Gesundheit speichert.
	     */
        [DataMember]
        public int ownHealth { get; set; }
        /**
	     * Attribut, das die aktuell ausger&uuml;stete Waffe speichert.
	     */
        [DataMember]
        public string equippedWeapon { get; set; }
        /**
	     * Attribut, das den Munitionsstand des aktuellen Magazins speichert.
	     */
        [DataMember]
        public int currentAmmu { get; set; }
        /**
	     * Attribut, das die Reservemunition speichert.
	     */
        [DataMember]
        public int currentOverallAmmu { get; set; }
        /**
	     * Attribut das speichert, ob eine weitere Waffe ben&ouml;tigt wird.
	     */
        [DataMember]
        public bool isWeaponNeeded { get; set; }
        /**
	     * Attribut das speichert, ob Munition ben&ouml;tigt wird.
	     */
        [DataMember]
        public bool isAmmunitionNeeded { get; set; }
        /**
	     * Attribut das speichert, ob Gesundheit ben&ouml;tigt wird.
	     */
        [DataMember]
        public bool isHealthNeeded { get; set; }
        /**
	     * Attribut das speichert, ob Deckung ben&ouml;tigt wird.
	     */
        [DataMember]
        public bool isCoverNeeded { get; set; }
        /**
	     * Attribut das speichert, ob sich der Spieler in Deckung befindet.
	     */
        [DataMember]
        public bool isCovered { get; set; }
        /**
	     * Attribut das die Distanz zur aufnehmbaren Waffe speichert.
	     */
        [DataMember]
        public int weaponDistance { get; set; }
        /**
	     * Attribut das die Distanz zur aufnehmbaren Munition speichert.
	     */
        [DataMember]
        public int ammunitionDistance { get; set; }
        /**
	     * Attribut das die Distanz zur aufnehmbaren Gesundheit speichert.
	     */
        [DataMember]
        public int healthDistance { get; set; }
        /**
	     * Attribut das die Distanz zur n&auml;chsten Deckung speichert.
	     */
        [DataMember]
        public int coverDistance { get; set; }

        [DataMember]
        public double winChance { get; set; }

        [DataMember]
        public double killDeathRatio { get; set; }

        [DataMember]
        public double upTime { get; set; }

        /**
        * Attribut das die Distanz zur bewegenden Deckung.
        */
        [DataMember]
        public int movingCoverDistance { get; set; }
        /**
        * Attribut das die Distanz zur aufnehmbaren großen Munition speichert.
        */
        [DataMember]
        public int ammunitionLargeDistance { get; set; }
        /**
       * Attribut das speichert, ob das Gadget gebraucht wird.
       */
        [DataMember]
        public bool isGadgetNeeded { get; set; }
      
        /**
	     * Default-Konstruktor f&uuml;r den Status.
	     */
        public Status() : this(false, (int)Distance.unknown, (int)LastPosition.unknown, true, 1000000, "Pistol", 12, 48,
            true, false, false, false, false, (int)WeaponDistance.unknown, (int)AmmunitionDistance.unknown,
            (int)HealthDistance.unknown, (int)CoverDistance.unknown, (double)CurrentWinChance.equal, (double)KillDeathRatio.negative,
            (double)UpTime.shorter, (int)MovingCoverDistance.unknown, (int)AmmunitionLargeDistance.unknown, true)
        {
            ammuPosition = new Vector3(10000f, 10000f, 10000f);
            healthPosition = new Vector3(10000f, 10000f, 10000f);
            weaponPosition = new Vector3(10000f, 10000f, 10000f);
            enemiesLastKnownPosition = new Vector3(10000f, 10000f, 10000f);
            //coverDistance = new Vector3(10000f, 10000f, 10000f);
            ammuLargePosition = new Vector3(10000f, 10000f, 10000f);
        }

        /**
	     * Konstruktor mit allen Parametern zur Erzeugung eines Status-Objekts, alle
	     * Parameter vor allem f&uuml;r die (De-)Serialisierung durch JSON.
	     * 
	     * @param isEnemyVisible
	     *            Ist der Gegner sichtbar.
	     * @param distanceToEnemy
	     *            Distanz zum Gegner.
	     * @param lastPosition
	     *            Distanz zur letzten bekannten Position des Gegners.
	     * @param isEnemyAlive
	     *            Ist der Gegner am Leben.
	     * @param ownHealth
	     *            Eigene Gesundheit.
	     * @param equippedWeapon
	     *            Ausger&uuml;stete Waffe-
	     * @param currentAmmu
	     *            Aktuelle Munition im Magazin.
	     * @param currentOverallAmmu
	     *            Aktuelle Reservemunition.
	     * @param isWeaponNeeded
	     *            Wird eine weitere Waffe ben&ouml;tigt.
	     * @param isAmmunitionNeeded
	     *            Wird Munition gebraucht.
	     * @param isHealthNeeded
	     *            Wird Gesundheit gebraucht.
	     * @param isCoverNeeded
	     *            Wird Deckung gebraucht.
	     * @param isCovered
	     *            Befindet sich der Spieler aktuell in Deckung.
	     * @param weaponDistance
	     *            Distanz zur Waffe.
	     * @param ammunitionDistance
	     *            Distanz zur Munition.
	     * @param healthDistance
	     *            Distanz zur Gesundheit.
	     * @param coverDistance
	     *            Distanz zur Deckung.
         * @param winChance
         *            Mögliche Gewinnchance der CBR Ki
         * @param killDeathRatio
         *            Verhältnis von Kills zu toden der CBR Ki
         * @param upTime 
         *            Misst die Zeit, die die CBR Ki in einer Situation ueberlebt  
         *            
	     */
        public Status(bool isEnemyVisible, int distanceToEnemy, int lastPosition, bool isEnemyAlive, int ownHealth,
            string equippedWeapon, int currentAmmu, int currentOverallAmmu, bool isWeaponNeeded,
            bool isAmmunitionNeeded, bool isHealthNeeded, bool isCoverNeeded, bool isCovered,
            int weaponDistance, int ammunitionDistance, int healthDistance, int coverDistance, double winChance, double killDeathRatio,
            double upTime, int movingCoverDistance, int ammunitionLargeDistance, bool isGadgetNeeded)
        {
            this.isEnemyVisible = isEnemyVisible;
            this.distanceToEnemy = distanceToEnemy;
            this.lastPosition = lastPosition;
            this.isEnemyAlive = isEnemyAlive;
            this.ownHealth = ownHealth;
            this.equippedWeapon = equippedWeapon;
            this.currentAmmu = currentAmmu;
            this.currentOverallAmmu = currentOverallAmmu;
            this.isWeaponNeeded = isWeaponNeeded;
            this.isAmmunitionNeeded = isAmmunitionNeeded;
            this.isHealthNeeded = isHealthNeeded;
            this.isCoverNeeded = isCoverNeeded;
            this.isCovered = isCovered;
            this.weaponDistance = weaponDistance;
            this.ammunitionDistance = ammunitionDistance;
            this.healthDistance = healthDistance;
            this.coverDistance = coverDistance;
            this.winChance = winChance;
            this.killDeathRatio = killDeathRatio;
            this.upTime = upTime;

            this.movingCoverDistance = movingCoverDistance;
            this.ammunitionLargeDistance = ammunitionLargeDistance;
            this.isGadgetNeeded = isGadgetNeeded;

            enemiesLastKnownPosition = new Vector3(10000f, 10000f, 10000f);

            
        }
 
        public static CurrentWinChance GetCurrentWinChance(double value)
        {
            CurrentWinChance winChance;

            if (value < 40)
            {
                winChance = CurrentWinChance.loosing;
            }
            else if (value > 60)
            {
                winChance = CurrentWinChance.winning;
            }
            else
            {
                winChance = CurrentWinChance.equal;
            }

            return winChance;
        }

        public static KillDeathRatio GetKillDeathRatio(double value)
        {
            KillDeathRatio killDeathRatio;

            killDeathRatio = KillDeathRatio.negative;

            if(value > 1)
            {
                killDeathRatio = KillDeathRatio.positive;
            }
            else if (value < 1)
            {
                killDeathRatio = KillDeathRatio.negative;
            }
            else if (value == 1)
            {
                killDeathRatio = KillDeathRatio.equal;
            }

            return killDeathRatio;
        }

        public static UpTime GetUpTime(double value)
        {
            UpTime upTime = UpTime.shorter;

            double timecheck = 0.00;
            

            if(value > timecheck)
            {
                upTime = UpTime.longer;
            } else if (value == timecheck)
            {
                upTime = UpTime.equal;
            } else if (value < timecheck)
            {
                upTime = UpTime.shorter;
            }

            return upTime;
        }

        /**
         * Funktion, um einen Float-Value in den zugehörigen enum zu überführen.
         */
        public static Distance GetEnemyDistanceFromFloatValue(float value)
        {
            Distance enemyDistance;

            if (value < 15)
            {
                enemyDistance = Distance.near;
            }
            else if (value >= 15 && value < 30)
            {
                enemyDistance = Distance.middle;
            }
            else if (value >= 30 && value <= 50)
            {
                enemyDistance = Distance.far;
            }
            else
            {
                enemyDistance = Distance.unknown;
            }

            return enemyDistance;
        }

        /**
         * Funktion, um einen Float-Value in den zugehörigen enum zu überführen.
         */
        public static LastPosition GetLastPositionFromFloatValue(float value)
        {
            LastPosition position;

            if (value < 15)
            {
                position = LastPosition.near;
            }
            else if (value >= 15 && value < 30)
            {
                position = LastPosition.middle;
            }
            else if (value >= 30 && value <= 50)
            {
                position = LastPosition.far;
            }
            else
            {
                position = LastPosition.unknown;
            }

            return position;
        }

        /**
         * Funktion, um einen Integer-Value in den zugehörigen enum zu überführen.
         */
        public static OwnHealth GetOwnHealthFromIntValue(int value)
        {
            OwnHealth health;

            if (value == Player.mMaxLife)
            {
                health = OwnHealth.full;
            }
            else if (value >= 70 && value < Player.mMaxLife)
            {
                health = OwnHealth.much;
            }
            else if (value >= 40 && value < 70)
            {
                health = OwnHealth.middle;
            }
            else if (value >= 20 && value < 40)
            {
                health = OwnHealth.few;
            }
            else
            {
                health = OwnHealth.critical;
            }
            
            return health;
           
        }

        /**
         * Funktion, um der Waffe die aktuelle Munition zu ermitteln.
         */
        public static CurrentAmmunition GetCurrentAmmunitionFromWeapon(Weapon weapon)
        {
            int value = weapon.mCurrentMagazineAmmu;
            int magazineSize = weapon.mMagazineSize;
            CurrentAmmunition ammu;

            if (value == magazineSize)
            {
                ammu = CurrentAmmunition.full;
            }
            else if (value >= magazineSize * 0.7 && value < magazineSize)
            {
                ammu = CurrentAmmunition.much;
            }
            else if (value >= magazineSize * 0.35 && value < magazineSize * 0.7)
            {
                ammu = CurrentAmmunition.middle;
            }
            else if (value > 0 && value < magazineSize * 0.35)
            {
                ammu = CurrentAmmunition.few;
            }
            else
            {
                ammu = CurrentAmmunition.empty;
            }
            return ammu;
        }

        /**
         * Funktion, um der Waffe die aktuelle Munitionsreserve zu ermitteln.
         */
        public static CurrentOverallAmmunition GetCurrentOverallAmmunitionFromWeapon(Weapon weapon)
        {
            int value = weapon.mCurrentOverallAmmu;
            int maxMagazineSize = weapon.mMagazineSize * Weapon.AMMU_FACTOR;
            CurrentOverallAmmunition ammu;

            if (value == maxMagazineSize)
            {
                ammu = CurrentOverallAmmunition.full;
            }
            else if (value >= maxMagazineSize * 0.7 && value < maxMagazineSize)
            {
                ammu = CurrentOverallAmmunition.much;
            }
            else if (value >= maxMagazineSize * 0.35 && value < maxMagazineSize * 0.7)
            {
                ammu = CurrentOverallAmmunition.middle;
            }
            else if (value > 0 && value < maxMagazineSize * 0.35)
            {
                ammu = CurrentOverallAmmunition.few;
            }
            else
            {
                ammu = CurrentOverallAmmunition.empty;
            }

            return ammu;
        }

        /**
         * Funktion, um anhand eines Float-Values den zugehörigen enum Wert zu erhalten.
         */
        public static WeaponDistance GetWeaponDistance(float value)
        {
            WeaponDistance distance;

            if (value < 15)
            {
                distance = WeaponDistance.near;
            }
            else if (value >= 15 && value < 30)
            {
                distance = WeaponDistance.middle;
            }
            else if (value >= 30 && value <= 50)
            {
                distance = WeaponDistance.far;
            }
            else
            {
                distance = WeaponDistance.unknown;
            }

            return distance;
        }

        /**
         * Funktion, um anhand eines Float-Values den zugehörigen enum Wert zu erhalten.
         */
        public static AmmunitionDistance GetAmmunitionDistance(float value)
        {
            AmmunitionDistance distance;

            if (value < 15)
            {
                distance = AmmunitionDistance.near;
            }
            else if (value >= 15 && value < 30)
            {
                distance = AmmunitionDistance.middle;
            }
            else if (value >= 30 && value <= 50)
            {
                distance = AmmunitionDistance.far;
            }
            else
            {
                distance = AmmunitionDistance.unknown;
            }

            return distance;
        }

        /**
       * Funktion, um anhand eines Float-Values den zugehörigen enum Wert zu erhalten.
       */
        public static AmmunitionLargeDistance GetAmmunitionLargeDistance(float value)
        {
            AmmunitionLargeDistance distance;

            if (value < 15)
            {
                distance = AmmunitionLargeDistance.near;
            }
            else if (value >= 15 && value < 30)
            {
                distance = AmmunitionLargeDistance.middle;
            }
            else if (value >= 30 && value <= 50)
            {
                distance = AmmunitionLargeDistance.far;
            }
            else
            {
                distance = AmmunitionLargeDistance.unknown;
            }

            return distance;
        }

        /**
         * Funktion, um anhand eines Float-Values den zugehörigen enum Wert zu erhalten.
         */
        public static HealthDistance GetHealthDistance(float value)
        {
            HealthDistance distance;

            if (value < 15)
            {
                distance = HealthDistance.near;
            }
            else if (value >= 15 && value < 30)
            {
                distance = HealthDistance.middle;
            }
            else if (value >= 30 && value <= 50)
            {
                distance = HealthDistance.far;
            }
            else
            {
                distance = HealthDistance.unknown;
            }

            return distance;
        }

        /**
         * Funktion, um anhand eines Float-Values den zugehörigen enum Wert zu erhalten.
         */
        public static CoverDistance GetCoverDistance(float value)
        {
            CoverDistance distance;

            if (value < 15)
            {
                distance = CoverDistance.near;
            }
            else if (value >= 15 && value < 30)
            {
                distance = CoverDistance.middle;
            }
            else if (value >= 30 && value <= 50)
            {
                distance = CoverDistance.far;
            }
            else
            {
                distance = CoverDistance.unknown;
            }

            return distance;
        }

        /**
       * Funktion, um anhand eines Float-Values den zugehörigen enum Wert zu erhalten.
       */
        public static MovingCoverDistance GetMovingCoverDistance(float value)
        {
            MovingCoverDistance distance;

            if (value < 15)
            {
                distance = MovingCoverDistance.near;
            }
            else if (value >= 15 && value < 30)
            {
                distance = MovingCoverDistance.middle;
            }
            else if (value >= 30 && value <= 50)
            {
                distance = MovingCoverDistance.far;
            }
            else
            {
                distance = MovingCoverDistance.unknown;
            }

            return distance;
        }

        public override string ToString()
        {
            return "Status [isEnemyVisible=" + isEnemyVisible + ", distanceToEnemy=" + (Distance)distanceToEnemy + ", lastPosition="
                + (LastPosition)lastPosition + ", isEnemyAlive=" + isEnemyAlive + ", ownHealth=" + (OwnHealth)ownHealth + ", equippedWeapon="
                + equippedWeapon + ", currentAmmu=" + (CurrentAmmunition)currentAmmu + ", currentOverallAmmu=" + (CurrentOverallAmmunition)currentOverallAmmu
                + ", isWeaponNeeded=" + isWeaponNeeded + ", isAmmunitionNeeded=" + isAmmunitionNeeded
                + ", isHealthNeeded=" + isHealthNeeded + ", isCoverNeeded=" + isCoverNeeded + ", isCovered=" + isCovered
                + ", weaponDistance=" + (WeaponDistance)weaponDistance + ", ammunitionDistance=" + (AmmunitionDistance)ammunitionDistance
                + ", healthDistance=" + (HealthDistance)healthDistance + ", coverDistance=" + (CoverDistance)coverDistance + ", winChance=" + (CurrentWinChance)winChance 
                + ", killDeathRatio=" + (KillDeathRatio)killDeathRatio + ", upTime=" + (UpTime)upTime + ", movingCoverDistance=" + (MovingCoverDistance)movingCoverDistance
                + ", ammunitionLargeDistance=" + (AmmunitionLargeDistance)ammunitionLargeDistance + ", isGadgetNeeded=" + isGadgetNeeded + "]";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!obj.GetType().Name.Equals(GetType().Name))
            {
                return false;
            }

            Status compare = (Status)obj;

            if (compare.isEnemyVisible != isEnemyVisible)
            {
                return false;
            }
            if (compare.distanceToEnemy != distanceToEnemy)
            {
                return false;
            }
            if (compare.lastPosition != lastPosition)
            {
                return false;
            }
            if (compare.isEnemyAlive != isEnemyAlive)
            {
                return false;
            }
            if (compare.ownHealth != ownHealth)
            {
                return false;
            }
            if (compare.equippedWeapon != equippedWeapon)
            {
                return false;
            }
            if (compare.currentAmmu != currentAmmu)
            {
                return false;
            }
            if (compare.currentOverallAmmu != currentOverallAmmu)
            {
                return false;
            }
            if (compare.isWeaponNeeded != isWeaponNeeded)
            {
                return false;
            }
            if (compare.isAmmunitionNeeded != isAmmunitionNeeded)
            {
                return false;
            }
            if (compare.isGadgetNeeded != isGadgetNeeded)
            {
                return false;
            }
            if (compare.isHealthNeeded != isHealthNeeded)
            {
                return false;
            }
            if (compare.isCoverNeeded != isCoverNeeded)
            {
                return false;
            }
            if (compare.isCovered != isCovered)
            {
                return false;
            }
            if (compare.weaponDistance != weaponDistance)
            {
                return false;
            }
            if (compare.ammunitionDistance != ammunitionDistance)
            {
                return false;
            }
            if (compare.ammunitionLargeDistance != ammunitionLargeDistance)
            {
                return false;
            }
            if (compare.healthDistance != healthDistance)
            {
                return false;
            }
            if (compare.coverDistance != coverDistance)
            {
                return false;
            }
            if (compare.movingCoverDistance != movingCoverDistance)
            {
                return false;
            }
            if (compare.winChance != winChance)
            {
                return false;
            }
            if(compare.killDeathRatio != killDeathRatio)
            {
                return false;
            }
            if (compare.upTime != upTime)
            {
                return false;
            }

            return true;
           
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
