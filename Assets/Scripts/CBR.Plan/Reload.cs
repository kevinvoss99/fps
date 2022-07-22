using System.Runtime.Serialization;
namespace Assets.Scripts.CBR.Plan
{
    /**
     * Klasse zur Darstellung der konkreten Aktion, dass der Spieler nachladen soll.
     * Diese Klasse erbt somit von der abstrakten Klasse Action.
     * 
     * @author Jannis Hillmann
     *
     */
    [DataContract]
    public class Reload : Action
    {
        /**
         * Default-Konstruktor.
         */
        public Reload() : this(false) { }
        /**
         * Konstruktor, der einen Parameter erwartet, ob die Aktion sequentiell oder parallel ist.
         */
        public Reload(bool sequentiel) : base("Reload", sequentiel)
        {

        }
    }
}
