using System.Runtime.Serialization;
namespace Assets.Scripts.CBR.Plan
{
    /**
     * Klasse zur Darstellung der konkreten Aktion, dass der Spieler in Deckung
     * gehen soll. Diese Klasse erbt somit von der abstrakten Klasse Action.
     * 
     * @author Jannis Hillmann
     *
     */
    [DataContract]
    public class UseCover : Action
    {
        /**
         * Default-Konstruktor.
         */
        public UseCover() : this(true) { }
        /**
         * Konstruktor, der einen Parameter erwartet, ob die Aktion sequentiell oder parallel ist.
         */
        public UseCover(bool sequentiel) : base("UseCover", sequentiel)
        {

        }
    }
}
