using System;
using MyAspWeb.DAL;
using MyAspWeb.Model;

namespace MyAspWeb
{
    public partial class serviceState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");
            }
            User user = UserDAL.findUserByUsername((string) Session["username"]);

            if (user.State.Equals("0"))
            {
                Button1.Text = "未启动";
                Button1.CssClass = "btn btn-primary";
            }
            else
            {
                Button1.Text = "已启动";
                Button1.CssClass = "btn btn-success";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            User user = UserDAL.findUserByUsername((string)Session["username"]);
            string state = user.State;
            if (state.Equals("0"))
            {
                //添加任务队列
                UserDAL.modifyUserState(user, "1");
                TaskQueue.RemoveUserAllTask(user);
                TaskQueue.PushTastToQueue(TaskQueue.GetUserNextTask(user));

                Button1.Text = "已启动";
                Button1.CssClass = "btn btn-success";
            }
            else
            {
                //删除任务队列
                UserDAL.modifyUserState(user, "0");
                TaskQueue.RemoveUserAllTask(user);

                Button1.Text = "未启动";
                Button1.CssClass = "btn btn-primary";
            }


        }
    }
}