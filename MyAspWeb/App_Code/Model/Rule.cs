using System;

namespace MyAspWeb.Model
{
    public class Rule
    {
        private string startTime;
        private string loopTime;
        private string orderTime;

        public Rule(string startTime, string orderTime, string loopTime)
        {
            this.orderTime = orderTime;
            this.startTime = startTime;
            this.loopTime = loopTime;
        }

        public string OrderTime
        {
            get { return orderTime; }
            set { orderTime = value; }
        }

        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public string LoopTime
        {
            get { return loopTime; }
            set { loopTime = value; }
        }

        public DateTime GetStartTime()
        {
            return Convert.ToDateTime(startTime);
        }
    }
}