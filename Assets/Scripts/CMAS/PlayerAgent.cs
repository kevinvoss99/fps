using Boris;
using Assets.Scripts.Util;
using Assets.Scripts.CBR.Plan;
using UnityEngine;
using Assets.Scripts.AI;

namespace Assets.Scripts.CMAS
{
    /**
     * Klasse, die vom Abstrakten Agenten erbt und den Spieleragenten darstellt.
     */
    public class PlayerAgent : AbstractAgent
    {
        /**
         * Konstruktor der den Namen des Agenten erwartet.
         */
        public PlayerAgent(string agentName) : base(agentName)
        {
            // Delegate-Methode zuweisen
            MessageReceived += ReceiveMessage;
        }

        public override void SendStringMessage(string to, string content)
        {
            // Log
            SendMessage(to, content);
        }

        /**
         * Methode, welche die Nachrichten an diesen Agenten annimmt und weiterverarbeitet.
         */
        private void ReceiveMessage(Communication message)
        {
            if (message.Sender.Equals(Constants.PLANNING_AGENT_NAME))
            {
                Debug.Log(message.Recipient + " received: " + message.Body + " from " + message.Sender);

                Plan response = JsonParser<Plan>.DeserializeObject(message.Body);

                Plan plan = new Plan();
                plan.actionsAsString = response.actionsAsString;
                plan.AddActions(PlanningAgent.ExtractActionsFromString(plan.actionsAsString));

                Debug.Log("Do:" + plan.actionsAsString);


                CommonUnityFunctions.GetPlayerByName(agentName).mPlan = plan;

                BotCBRBehaviourScript.mIsRequesting = false;
            }
        }
    }
}
