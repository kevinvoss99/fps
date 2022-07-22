using System.Runtime.Serialization;
namespace Assets.Scripts.CBR.Plan
{
    /**
     * Klasse zur Darstellung der konkreten Aktion, dass der Spieler sich bewegen
     * soll. Diese Klasse erbt somit von der abstrakten Klasse Action.
     * 
     * @author Jannis Hillmann
     *
     */
    [DataContract]
    public class MoveTo : Action
    {
        /**
         * Default-Konstruktor.
         */
        public MoveTo() : this(false) { }

        /**
         * Konstruktor, der die Angabe erwartet, ob die Aktion sequentiell oder parallel durchgeführt werden kann.
         */
        public MoveTo(bool sequentiel) : base("MoveTo", sequentiel)
        {
        }
    }
}
