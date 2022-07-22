using System.Runtime.Serialization;

namespace Assets.Scripts.CBR.Model
{
    /**
     * Diese Klasse bietet eine Datenstruktur, um eine Situation darstellen zu können.
     */
    [DataContract]
    public class Situation
    {
        /**
         * Die Situation muss beinhalten, welcher Spieler sich in dieser befindet.
         */
        [DataMember]
        public string player { get; set; }

        /**
         * Die Situation muss auch den Status des Spielers beinhalten.
         */
        [DataMember]
        public Status playerStatus { get; set; }

        /**
         * Default-Konstruktor
         */
        public Situation() : this("", new Status())
        {

        }

        /**
         * Konstruktor, der den Namen des Spielers, sowie seinen Status erwartet.
         */
        public Situation(string player, Status playerStatus)
        {
            this.player = player;
            this.playerStatus = playerStatus;
        }

        public override string ToString()
        {
            return "Situation [player=" + player + ", playerStatus=" + playerStatus.ToString() + "]";
        }

    }
}
