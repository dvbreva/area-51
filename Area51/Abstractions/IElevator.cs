using Area51.Enums;

namespace Area51.Abstractions
{
    public interface IElevator
    {
        void ChooseFloor(FloorType currentFloor);

        void GoToFloor(FloorType currentFloor, FloorType desiredFloor);

        void EnterElevator(BaseAgent agent, FloorType currentFloor, FloorType desiredFloor);

        void LeaveElevator(BaseAgent agent);
    }
}
