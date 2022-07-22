using System.Runtime.Serialization;
namespace Assets.Scripts.CBR.Plan
{
    /**
     * Abstrakte Klasse zur Darstellung einer Aktion. Erbende Klassen stellen
     * jeweils eine konkrete Aktion dar.
     * 
     * @author Jannis Hillmann
     *
     */
    [DataContract]
    public abstract class Action
    {
        /**
	     * Attribut das beschreibt, ob die Aktion sequentiell oder parallel
	     * ausgef&uuml;hrt werden kann.
	     */
        [DataMember]
        public bool sequentiel { get; set; }
        /**
	     * Attribut das beschreibt, ob die Ausf&uuml;hrung der Aktion abgeschlossen
	     * ist.
	     */
        [DataMember]
        public bool finished { get; set; }
        /**
	     * Attribut das den Namen der Aktion darstellt.
	     */
        public string name { get; set; }

        /**
         * Default-Konstruktor.
         */
        public Action() : this("Action", true) { }
        /**
         * Konstruktor der den Namen der Aktion als Parameter erwartet.
         */
        public Action(string name) : this(name, true)
        {
        }
        /**
         * Konstruktor der den Namen der Aktion und die Information, ob die Aktion sequentiell ist als Parameter erwartet.
         */
        public Action(string name, bool sequentiel)
        {
            this.name = name;
            this.sequentiel = sequentiel;
            finished = false;
        }

    }
}
