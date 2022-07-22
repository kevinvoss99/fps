using Boris;
using Assets.Scripts.Util;
using Assets.Scripts.CBR.Model;
using UnityEngine;

namespace Assets.Scripts.CMAS
{
    /**
     * Klasse, die vom Abstrakten Agenten erbt und den Kommunikationsagenten darstellt.
     */
    public class CommunicationAgent : AbstractAgent
    {
        /**
         * Konstruktor der den Namen des Agenten erwartet.
         */
        public CommunicationAgent(string agentName) : base(agentName)
        {
            // Delegate-Methode zuweisen
            MessageReceived += ReceiveMessage;
        }

        public override void SendStringMessage(string to, string content)
        {
            SendMessage(to, content);
        }

        /**
         * Methode, welche die Nachrichten an diesen Agenten annimmt und weiterverarbeitet.
         */
        private void ReceiveMessage(Communication message)
        {
            Debug.Log("Request: " + message.Body);

            var response = JsonParser<Response>.SerializeObject(
                new Connection.Connection().Send(
                    JsonParser<Request>.DeserializeObject(message.Body)));
            Debug.Log("res: " + response);
            SendStringMessage(Constants.PLANNING_AGENT_NAME, response);
        }
    }
}
