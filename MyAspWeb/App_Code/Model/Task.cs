using System.Configuration;

namespace MyAspWeb.Model
{
    public class Task
    {
        public static string YUYUE = "1";
        public static string QUXIAOYUYUE = "2";
        public static string QIANDAO = "3";
        public static string QIANTUI = "4";

        private int uid;
        private string username;
        private string password;
        private Rule rule;
        private string type;
        private double time;

        public Task(int uid, string username, string password, Rule rule, string type, double time)
        {
            this.uid = uid;
            this.username = username;
            this.password = password;
            this.rule = rule;
            this.type = type;
            this.time = time;
        }

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public Rule Rule
        {
            get { return rule; }
            set { rule = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public double Time
        {
            get { return time; }
            set { time = value; }
        }
    }
}