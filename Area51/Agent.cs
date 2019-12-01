using Area51.Enums;

namespace Area51
{
    public class Agent
    {
        public string Name { get; private set; }

        public SecurityLevel SecurityLevel { get; private set; }

        public AgentPosition AgentPosition { get; private set; }

        public Elevator Elevator { get; private set; }


        public Agent(string name, 
            SecurityLevel securityLevel, 
            Elevator elevator)
        {
            Name = name;
            SecurityLevel = securityLevel;
            Elevator = elevator;
        }
    }
}
