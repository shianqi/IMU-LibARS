using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace MyAspWeb
{
    public class Global : System.Web.HttpApplication
    {
        private Thread schedulerThread;

        protected void Application_Start(object sender, EventArgs e)
        {
            TaskQueue.InitQueue();

            schedulerThread = new Thread(TaskQueue.ExecTask);
            schedulerThread.Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            schedulerThread.Abort();
        }
    }
}