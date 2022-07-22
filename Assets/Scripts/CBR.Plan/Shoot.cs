using System.Runtime.Serialization;
namespace Assets.Scripts.CBR.Plan
{
    /**
     * Klasse zur Darstellung der konkreten Aktion, dass der Spieler schie&szlig;en
     * soll. Diese Klasse erbt somit von der abstrakten Klasse Action.
     * 
     * @author Jannis Hillmann
     *
     */
    [DataContract]
    public class Shoot : Action
    {
        /**
         * Default-Konstruktor.
         */
        public Shoot() : this(false) { }
        /**
         * Konstruktor, der einen Parameter erwartet, ob die Aktion sequentiell oder parallel ist.
         */
        public Shoot(bool sequentiel) : base("Shoot", sequentiel)
        {

        }
    }
}
