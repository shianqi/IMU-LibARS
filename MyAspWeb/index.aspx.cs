using System;

namespace MyAspWeb
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                Label1.Text = (string)Session["username"];
            }
        }

        protected void Logout(object sender, EventArgs e)
        {
            Session["username"] = null;
            Response.Redirect("login.aspx");
        }
    }
}