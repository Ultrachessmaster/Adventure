using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public class Timer
    {
        Action<int> action;
        float time;
        float timepassed;
        public Timer(Action<int> act, float time)
        {
            action = act;
            this.time = time;
            Adventure.Timers.Add(this);
        }

        public void AddTime(float timepass)
        {
            timepassed += timepass;
            if(timepassed > time)
            {
                action.Invoke(0);
                Adventure.Timers.Remove(this);
            }
            
        }

    }
}
