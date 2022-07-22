using Assets.Scripts.Util;
using System.Collections.Generic;
using Boris;

namespace Assets.Scripts.CMAS
{
    /**
     * Diese Klasse stellt den AgentenController dar, welcher sich um die Strukturierung der vorhandenen Agenten kümmert.
     */
    public class AgentController
    {
        /**
         * Portal, zu welchem sich verbunden wird.
         */
        private Portal mAgentPortal;
        /**
         * Da es immer nur exakt einen Kommunikationsagenten gibt, kann dieser hier als Membervariable gespeichert werden.
         */
        private CommunicationAgent mCommunicationAgent;
        /**
         * Da es immer nur exakt einen Planungsagenten gibt, kann dieser hier als Membervariable gespeichert werden.
         */
        private PlanningAgent mPlanningAgent;

        /**
         * Liste zur Speicherung aller existierenden Agenten.
         */
        private static List<AbstractAgent> mAgents;

        /**
         * Default-Konstruktor.
         */
        public AgentController()
        {
            mAgents = new List<AbstractAgent>();
        }

        /**
         * Diese Funktion sorgt für das Instanziieren der Portale und die Verknüpfung dieser, sodass ein funktionsfähiges MAS ermöglicht wird.
         */
        public void StartAgentPortal()
        {
            mAgentPortal = new Portal(Constants.PORTAL_NAME);
            mCommunicationAgent = new CommunicationAgent(Constants.COMMUNICATION_AGENT_NAME);
            mPlanningAgent = new PlanningAgent(Constants.PLANNING_AGENT_NAME);
            AddAgent(mPlanningAgent);
            AddAgent(mCommunicationAgent);
            mAgentPortal.Connect(new Portal(Constants.MAIN_PORTAL_NAME));
        }

        /**
         * Diese Funktion fügt einen gegebenen Agenten zu der Liste und dem Agentenportal hinzu.
         */
        public void AddAgent(AbstractAgent agent)
        {
            mAgents.Add(agent);
            mAgentPortal.AddAgent(agent);
        }
    }
}
