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
    public class PlaceGadget : Action
    {
        /**
         * Default-Konstruktor.
         */
        public PlaceGadget() : this(true) { }
        /**
         * Konstruktor, der einen Parameter erwartet, ob die Aktion sequentiell oder parallel ist.
         */
        public PlaceGadget(bool sequentiel) : base("PlaceGadget", sequentiel)
        {

        }
    }
}
