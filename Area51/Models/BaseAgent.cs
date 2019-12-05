using Area51.Abstractions;
using Area51.Enums;
using System;

namespace Area51
{
    public class BaseAgent : IAgent
    {
        #region Private Fields

        private bool _inElevator = true;
        private bool _decidesToLeaveFloor = false;
        private bool _decidesToLeave = false;
        private FloorType _currentFloor = FloorType.GroundFloor;
        private static Random _random = new Random();

        #endregion

        #region Public Properties

        public string Name { get; private set; }

        public SecurityLevel SecurityLevel { get; private set; }

        public Elevator Elevator { get; private set; }

        #endregion

        #region Public Constructors

        public BaseAgent(string name,
            SecurityLevel securityLevel,
            Elevator elevator)
        {
            Name = name;
            SecurityLevel = securityLevel;
            Elevator = elevator;
        }

        #endregion

        #region Public Methods

        public void DoWork()
        {
            while (_inElevator)
            {
                var elevatorAction = _random.Next(0, 3);

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
                        _decidesToLeave = true;
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

        #endregion

        #region Private Methods

        private void EnterElevator()
        {
            var randomFloor = GetRandomFloor();

            Elevator.EnterElevator(this, FloorType.GroundFloor, randomFloor);
        }

        private FloorType GetRandomFloor()
            => (FloorType)_random.Next(0, 4);

        #endregion
    }
}
