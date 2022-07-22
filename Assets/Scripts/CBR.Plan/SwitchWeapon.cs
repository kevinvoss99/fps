using System.Runtime.Serialization;
namespace Assets.Scripts.CBR.Plan
{
    /**
     * Klasse zur Darstellung der konkreten Aktion, dass der Spieler die Waffe
     * wechseln soll. Diese Klasse erbt somit von der abstrakten Klasse Action.
     * 
     * @author Jannis Hillmann
     *
     */
    [DataContract]
    public class SwitchWeapon : Action
    {
        /**
         * Default-Konstruktor.
         */
        public SwitchWeapon() : this(false) { }
        /**
         * Konstruktor, der einen Parameter erwartet, ob die Aktion sequentiell oder parallel ist.
         */
        public SwitchWeapon(bool sequentiel) : base("SwitchWeapon", sequentiel)
        {

        }
    }
}
