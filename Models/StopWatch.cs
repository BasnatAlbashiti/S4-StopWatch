using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4.Models
{
    internal class StopWatch
    {
        public int ElapsedSeconds { get; private set; }
        public bool IsRunning { get; private set; }
        
        //private set means only the class itself (StopWatch) can change the value of this property
        //get : any one can read the value 

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Reset()
        {
            ElapsedSeconds = 0;
        }

        public void Tick()
        {
            if (IsRunning)
            {
                ElapsedSeconds++;
            }
        }
    }
}
