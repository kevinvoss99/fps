using System;
using System.Collections.Generic;


namespace Assets.Scripts.Model
{
    /**
     * Klasse, die eine Datenstruktur für die Speicherung der Statistiken zur Verfügung stellt.
     */
    public class Statistics
    {
        /**
         * Liste, die alle Abschüsse eines Spielers beinhaltet.
         */
        private List<Frag> mFrags;
        /**
         * Liste, die alle Tode eines Spielers beinhaltet.
         */
        private List<Death> mDeaths;


        /**
         * Default-Konstruktor.
         */
        public Statistics()
        {
            mFrags = new List<Frag>();
            mDeaths = new List<Death>();
        }

        /**
         * Methode zum Hinzufügen eines Abschusses.
         */
        public void AddFrag(Frag frag)
        {
            mFrags.Add(frag);
           
            
        }
        /**
         * Methode zum Hinzufügen eines Todes.
         */
        public void AddDeath(Death death)
        {
            mDeaths.Add(death);
      
        }
        /**
         * Methode, welche die Anzahl der Tode zurückgibt.
         */
        public int DeathCount()
        {
            return mDeaths.Count;
        }
        /**
         * Methode, welche die Anzahl der Abschüsse zurückgibt.
         */
        public int FragCount()
        {
            return mFrags.Count;
        }
        /**
         * Methode, welche den letzten Abschuss zurückgibt.
         */
        public Frag GetLatestFrag()
        {
            return mFrags[mFrags.Count - 1];
        }
        /**
         * Methode, welche den letzten Tod zurückgibt.
         */
        public Death GetLatestDeath()
        {
            return mDeaths[mDeaths.Count - 1];
        }

        public override string ToString()
        {
            return "frags: " + FragCount() + ", deaths: " + DeathCount();
        }

    }

    /**
     * Klasse, welche eine Datenstruktur für einen Abschuss zur Verfügung stellt.
     */
    public class Frag
    {
        /**
         * Timestamp des Abschusses.
         */
        public DateTime mTimestamp { get; private set; }

        /**
         * Default-Konstruktor.
         */
        public Frag()
        {
            mTimestamp = DateTime.Now;
        }
    }
    /**
     * Klasse, welche eine Datenstruktur für einen Tod zur Verfügung stellt.
     */
    public class Death
    {
        /**
         * Timestamp des Abschusses.
         */
        public DateTime mTimestamp { get; private set; }

        /**
         * Default-Konstruktor.
         */
        public Death()
        {
            mTimestamp = DateTime.Now;
        }
    }
}
