using System.Threading;

namespace STelecom.Classes.Cheack
{
    class InstanceChecker
    {
        static readonly Mutex mutex = new Mutex(false, "STelecom");
        public static bool TakeMemory()
        {
            return mutex.WaitOne();
        }
    }
}
