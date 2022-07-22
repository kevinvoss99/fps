using Boris;
using Assets.Scripts.Util;
using Assets.Scripts.CBR.Model;
using Assets.Scripts.CBR.Plan;
using UnityEngine;

namespace Assets.Scripts.CMAS
{
    /**
     * Klasse, die vom Abstrakten Agenten erbt und den Planungsagenten darstellt.
     */
    public class PlanningAgent : AbstractAgent
    {
        /**
         * Konstruktor der den Namen des Agenten erwartet.
         */
        public PlanningAgent(string agentName) : base(agentName)
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
            if (message.Sender.Equals(Constants.COMMUNICATION_AGENT_NAME))
            {
                Debug.Log(message.Recipient + " received: " + message.Body + " from " + message.Sender);

                Response response = JsonParser<Response>.DeserializeObject(message.Body);

                Plan plan = new Plan();
                plan.actionsAsString = response.plan.actionsAsString;

                string json = JsonParser<Plan>.SerializeObject(plan);

                Debug.Log("Try to send to " + response.situation.player + ": " + json);
                SendStringMessage(response.situation.player, json);
            }
        }

        /**
         * Methode, welche die erhaltenen Aktionen in String-Format in ein Array aus Aktionen überführt und zurückgibt.
         */
        public static Action[] ExtractActionsFromString(string str)
        {
            string[] strings = str.Split(';');

            Action[] actions = new Action[strings.Length];

            int counter = 0;

            foreach (string s in strings)
            {
                if (s != "")
                {

                    if (s.Contains("Shoot"))
                    {
                        actions[counter++] = new Shoot();
                    }
                    else if (s.Contains("MoveTo"))
                    {
                        actions[counter++] = new MoveTo();
                    }
                    else if (s.Contains("CollectItem"))
                    {
                        CollectItem cItem = new CollectItem();

                        int posOne = s.IndexOf('<');
                        int posTwo = s.IndexOf('>');

                        cItem.destination = s.Substring(posOne + 1, posTwo - posOne - 1);

                        actions[counter++] = cItem;
                    }
                    else if (s.Contains("Reload"))
                    {
                        actions[counter++] = new Reload();
                    }
                    else if (s.Contains("SwitchWeapon"))
                    {
                        actions[counter++] = new SwitchWeapon();
                    }
                    else if (s.Contains("UseCover"))
                    {
                        actions[counter++] = new UseCover();
                    }
                    else if (s.Contains("PlaceGadget"))
                    {
                        actions[counter++] = new PlaceGadget();
                    }
                    else if (s.Contains("UseMovingCover"))
                    {
                        actions[counter++] = new UseMovingCover();
                    }
                }
            }
            return actions;
        }
    }
}
