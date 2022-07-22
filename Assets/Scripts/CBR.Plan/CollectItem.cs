using System.Runtime.Serialization;
namespace Assets.Scripts.CBR.Plan
{
    /**
     * Klasse zur Darstellung der konkreten Aktion, dass ein Gegenstand eingesammelt
     * werden soll. Diese Klasse erbt somit von der abstrakten Klasse Action.
     * 
     * @author Jannis Hillmann
     *
     */
    [DataContract]
    public class CollectItem : Action
    {
        /**
	     * Attribut zur Speicherung, welches Pick-Up aufgesammelt werden soll.
	     */
        [DataMember]
        public string destination { get; set; }

        /**
	     * Default Konstruktor.
	     */
        public CollectItem() : this("ammu") { }

        /**
	     * Konstruktor mit Angabe, welches Item eingesammelt werden soll.
	     */
        public CollectItem(string destination) : this(false, destination)
        {
        }
        /**
	     * Konstruktor mit Angabe, ob die Aktion sequentiell oder parallel ist und welches Item eingesammelt werden soll.
	     */
        public CollectItem(bool sequentiel, string destination) : base("CollectItem", sequentiel)
        {
            this.destination = destination;
        }
    }
}
