using Area51.Enums;

namespace Area51.Abstractions
{
    public interface IAgent
    {
        void DoWork();

        void GoHome();

        void CallTheElevator();

        void DoSomethingOnTheFloor(FloorType newFloor);
    }
}
