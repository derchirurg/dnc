using System;
using System.Threading.Tasks;

namespace EmptyApp
{
    public class WaitService
    {
        public Task Wait()
        {
            return Task.Delay(TimeSpan.FromSeconds(4));
        }
    }
}