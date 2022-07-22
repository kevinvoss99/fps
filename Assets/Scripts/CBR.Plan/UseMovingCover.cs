using System.Runtime.Serialization;
namespace Assets.Scripts.CBR.Plan
{
    /**
     * Klasse zur Darstellung der konkreten Aktion, dass der Spieler in mobiler Deckung
     * gehen soll. Diese Klasse erbt somit von der abstrakten Klasse Action.
     * 
     * @author Jannis 
     */
    [DataContract]
    public class UseMovingCover : Action
    {
        /**
         * Default-Konstruktor.
         */
        public UseMovingCover() : this(true) { }
        /**
         * Konstruktor, der einen Parameter erwartet, ob die Aktion sequentiell oder parallel ist.
         */
        public UseMovingCover(bool sequentiel) : base("UseMovingCover", sequentiel)
        {

        }
    }
}
