using System.Threading;

namespace Area51
{
    public class Elevator
    {
        private Semaphore _semaphore;

        public Elevator(int capacity = 1)
        {
            _semaphore = new Semaphore(capacity, capacity);
        }

        public void EnterElevator()
        {
            _semaphore.WaitOne();
        }

        public void LeaveElevator()
        {
            _semaphore.Release();
        }
    }
}
