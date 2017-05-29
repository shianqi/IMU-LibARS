namespace MyAspWeb.Model
{
    public class User
    {
        private int uid;
        private string username;
        private string password;
        private string state;
        private string rule;


        public User(int uid, string username, string password, string state, string rule)
        {
            this.uid = uid;
            this.username = username;
            this.password = password;
            this.state = state;
            this.rule = rule;
        }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.state = "0";
            this.rule = "";
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

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string Rule
        {
            get { return rule; }
            set { rule = value; }
        }
    }
}