using Area51.Enums;
using System;

namespace Area51
{
    public class Agent
    {
        private bool _inElevator = true;
        private bool _decidesToLeaveFloor = false;
        private FloorType _currentFloor = FloorType.GroundFloor;
        private static Random random = new Random();

        public string Name { get; private set; }

        public SecurityLevel SecurityLevel { get; private set; }

        public Elevator Elevator { get; private set; }

        public bool DecidesToLeave = false;

        public Agent(string name,
            SecurityLevel securityLevel,
            Elevator elevator)
        {
            Name = name;
            SecurityLevel = securityLevel;
            Elevator = elevator;
        }


        public void DoWork()
        {
            while (_inElevator)
            {
                var elevatorAction = random.Next(0, 3);

                switch (elevatorAction)
                {
                    case 0:
                        Console.WriteLine($"{Name} is doing something on the floor...");
                        DoSomethingOnTheFloor(_currentFloor);
                        break;

                    case 1:
                        Console.WriteLine($"{Name} decided to call the elevator.");
                        CallTheElevator();
                        break;

                    case 2:
                        DecidesToLeave = true;
                        GoHome();
                        break;

                    default:
                        throw new NotSupportedException("Unsupported Area51 action.");
                }
            }
        }

        public void GoHome()
        {
            _inElevator = false;
            Console.WriteLine($"x Agent {Name} then decided to go home.\n");
        }

        public void CallTheElevator()
        {
            Console.WriteLine($"\nAgent {Name} is waiting for the elevator.");
            if (Elevator.CurrentFloor != FloorType.GroundFloor)
            {
                Elevator.GoToFloor(Elevator.CurrentFloor, _currentFloor);

                EnterElevator();
            }
            else
            {
                EnterElevator();
            }
        }

        public void DoSomethingOnTheFloor(FloorType newFloor)
        {
            _currentFloor = newFloor;
            Console.WriteLine($"Agent {Name} has moved to {newFloor} floor.");

            _decidesToLeaveFloor = true;
            if (_decidesToLeaveFloor)
            {
                GoHome();
            }
        }

        private void EnterElevator()
        {
            var randomFloor = GetRandomFloor();

            Elevator.EnterElevator(this, FloorType.GroundFloor, randomFloor);
        }

        private FloorType GetRandomFloor()
            => (FloorType)random.Next(0, 4);

    }
}
