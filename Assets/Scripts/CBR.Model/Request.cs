using System.Runtime.Serialization;

namespace Assets.Scripts.CBR.Model
{
    /**
     * Diese Klasse bietet eine Datenstruktur, um eine Anfrage (Request) darstellen zu können.
     */
    [DataContract]
    public class Request
    {
        /**
         * Eine Anfrage besteht immer auch aus einer vorliegenden Situation.
         */
        [DataMember]
        public Situation situation { get; set; }

        /**
         * Default-Konstruktor
         */
        public Request() : this(new Situation())
        {

        }

        /**
         * Konstruktor, der eine Situation als Parameter erwartet.
         */
        public Request(Situation situation)
        {
            this.situation = situation;
        }

        public override string ToString()
        {
            return "Request: " + situation.ToString();
        }
    }
}
