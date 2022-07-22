using Boris;

namespace Assets.Scripts.CMAS
{
    /**
     * Abstrakte Klasse, die einen Boris.NET-Agenten zur Verfügung stellt.
     */
    public abstract class AbstractAgent : MetaAgent
    {

        /**
         * Der Name des Agenten.
         */
        public string agentName { get; private set; }

        /**
         * Einziger Konstruktor der Klasse, der zur Erzeugung eines Objekts den Namen des Agenten erwartet.
         */
        public AbstractAgent(string agentName) : base(agentName)
        {
            this.agentName = agentName;
        }

        /**
         * Abstrakte Methode, die jede erbende Klasse implementieren muss und die sich um das Versenden von Nachrichten kümmert.
         */
        public abstract void SendStringMessage(string to, string content);
    }
}
