using System;

namespace MyAspWeb
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text;
            string password = TextBox2.Text;
            
            UserService u = new UserService();
            if (ProcessSqlStr.check(username) && ProcessSqlStr.check(password) && u.UserLogin(username, password))
            {
                //登陆成功
                Session["username"] = username;
                Label1.Text = "SUCCESS！";
                Response.Redirect("index.aspx");
            }
            else
            {
                Label1.Text = "用户名或密码错误！";
            }
        }
        
    }
}