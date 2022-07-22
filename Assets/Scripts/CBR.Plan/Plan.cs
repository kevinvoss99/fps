using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Assets.Scripts.CBR.Plan
{
    /**
     * Klasse zur Darstellung eines Plans, der von dem Spieler ausgef&uuml;hrt
     * werden soll.
     * 
     * @author Jannis Hillmann
     *
     */
    [DataContract]
    public class Plan
    {
        /**
         * Liste zur Speicherung der Aktionen.
         */
        public List<Action> actions { get; set; }

        /**
	     * Der Plan als String mit klarer Formatierung zur simplen und
	     * automatisierten Verarbeitung.
	     */
        [DataMember]
        public string actionsAsString { get; set; }

        /**
         * Enum der aussagt, in welchem Stadium des Fortschritts sich der Plan befindet.
         */
        public enum Progress
        {
            NOT_STARTED = (1 << 0), IN_PROGRESS = (1 << 1), DONE = (1 << 2)
        }

        /**
	     * Attribut zur Speicherung des Fortschritts der Ausf&uuml;hrung des Plans.
	     */
        [DataMember]
        public int progress { get; set; }

        /**
         * Default-Konstruktor.
         */
        public Plan()
        {
            actions = new List<Action>();
            progress = (int)Progress.NOT_STARTED;
        }
        /**
	     * Methode, die ein gegebenes Array von Aktionen zur Membervariable
	     * hinzuf&uuml;gt, die den Plan als String speichert.
	     */
        public void AddActions(Action[] actions)
        {
            foreach (Action action in actions)
            {
                AddAction(action);
            }
        }

        /**
         * Methode, die eine gegebene Aktion der Liste hinzufügt.
         */
        public void AddAction(Action action)
        {
            actions.Add(action);
        }

        /**
         * Methode, die eine gegebene Aktion aus der Liste entfernt.
         */
        public void RemoveAction(Action action)
        {
            actions.Remove(action);
        }

        /**
         * Methode, welche die Anzahl der sich in der Liste befindenen Aktionen zurückgibt.
         */
        public int GetActionCount()
        {
            return actions.Count;
        }

        /**
         * Methode, welche eine Aktion aus der Liste anhand des Index zurückgibt.
         */
        public Action GetActionByIndex(int index)
        {
            return actions[index];
        }
    }
}
