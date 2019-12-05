using Area51.Abstractions;
using Area51.Enums;
using System;
using System.Threading;

namespace Area51
{
    public class Elevator : IElevator
    {
        private Semaphore _semaphore;
        private static Random _random = new Random();

        public BaseAgent Agent { get; set; }

        public FloorType CurrentFloor { get; set; }

        public Elevator(int capacity)
        {
            _semaphore = new Semaphore(capacity, capacity);
            CurrentFloor = FloorType.GroundFloor;
        }

        public void ChooseFloor(FloorType currentFloor)
        {
            var desiredFloor = GetRandomFloor();

            if (currentFloor == desiredFloor)
            {
                ChooseFloor(currentFloor);
            }
            else
            {
                Console.WriteLine($"Moving to floor {desiredFloor}");

                GoToFloor(currentFloor, desiredFloor);
            }
        }

        public void GoToFloor(FloorType currentFloor, FloorType desiredFloor)
        {
            Thread.Sleep(1000);
            CurrentFloor = desiredFloor;

            if (CanEnter(Agent, desiredFloor))
            {
                Console.WriteLine($"\n{Agent.Name} was granted access to this floor.");

                LeaveElevator(Agent);
            }
            else
            {
                Console.WriteLine($"\n{Agent.Name} was not granted access to this floor.");

                ChooseFloor(currentFloor);
            }
        }

        public void EnterElevator(BaseAgent agent, FloorType currentFloor, FloorType desiredFloor)
        {
            _semaphore.WaitOne();
            Agent = agent;

            Console.WriteLine($"{agent.Name} entered the elevator.");

            ChooseFloor(currentFloor);
        }

        public void LeaveElevator(BaseAgent agent)
        {
            _semaphore.Release();

            Console.WriteLine($"{agent.Name} has left the elevator.");

            agent.DoSomethingOnTheFloor(CurrentFloor);
        }

        private FloorType GetRandomFloor()
            => (FloorType)_random.Next(0, 4);

        private bool CanEnter(BaseAgent agent, FloorType floor)
        {
            switch (agent.SecurityLevel)
            {
                case SecurityLevel.Confidential:
                    if (floor == FloorType.GroundFloor)
                        return true;
                    else
                        return false;

                case SecurityLevel.Secret:
                    if (floor == FloorType.GroundFloor || floor == FloorType.SecretExperimentalFloor)
                        return true;
                    else
                        return false;

                case SecurityLevel.TopSecret:
                    return true;

                default:
                    Console.WriteLine("Something went wrong.");
                    return false;
            }
        }
    }
}
