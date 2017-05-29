using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Web.UI.WebControls;
using MyAspWeb.DAL;
using MyAspWeb.Model;
using Newtonsoft.Json.Linq;

namespace MyAspWeb
{
    public class TaskQueue
    {
        private static ArrayList userList;
        private static ArrayList taskList = new ArrayList();
        private static DateTime javaScriptTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));

        public static void ExecTask()
        {
            while (true)
            {
                Thread.Sleep(100);
                 
                if (taskList.Count == 0) continue;
                Task task = (Task)taskList[0];
                DateTime nowTime = DateTime.Now;
                double startTimeLong = task.Time;
                

                double nowTimeLong = (nowTime - javaScriptTime).TotalMilliseconds;

                if (task.Type.Equals(Task.YUYUE) || startTimeLong - nowTimeLong < 0)
                {
                    Task next = Exec(task);
                    if (next != null)
                    {
                        taskList.Add(next);
                    }
                    else
                    {
                        PushTastToQueue(GetUserNextTask(UserDAL.findUserByUsername(task.Username)));
                    }
                }
                System.Diagnostics.Debug.WriteLine(
                    "当前任务：" + javaScriptTime.AddMilliseconds(startTimeLong)+" "+
                    "下一次任务：" + (startTimeLong - nowTimeLong)+
                    " 总任务数："+ taskList.Count);
            }
        }

        public static Task Exec(Task oldTask)
        {
            string[] dataArr = DateHelper.ToDateTime(oldTask.Time).ToString().Split(' ');
            string date;
            if (dataArr[0].Split('/')[1].Length > 1)
            {
                date = dataArr[0].Split('/')[0] + "-" +
                       dataArr[0].Split('/')[1] + "-" +
                       dataArr[0].Split('/')[2];
            }
            else
            {
                date = dataArr[0].Split('/')[0] + "-0" +
                       dataArr[0].Split('/')[1] + "-" +
                       dataArr[0].Split('/')[2];
            }

            string fr_start = dataArr[1].Split(':')[0]+":" +dataArr[1].Split(':')[1];
            int fr_start_hour = int.Parse(fr_start.Split(':')[0]);
            int fr_start_minute = int.Parse(fr_start.Split(':')[1]);
            string fr_end = (fr_start_hour + int.Parse(oldTask.Rule.LoopTime)) > 24
                ? 24 + ":" + fr_start_minute
                : (fr_start_hour + int.Parse(oldTask.Rule.LoopTime)) + ":" + fr_start_minute;
            string start = (fr_start_hour - 1) + ":" + fr_start_minute;
            string end = (fr_start_hour + int.Parse(oldTask.Rule.LoopTime) - 1) > 24
                ? 24 + ":" + fr_start_minute
                : (fr_start_hour + int.Parse(oldTask.Rule.LoopTime) - 1) + ":" + fr_start_minute;

            string resv_start = date + "%20" + fr_start;
            string resv_end = date + "%20" + fr_end;

            IMULabHelper imuLabHelper = new IMULabHelper();
            imuLabHelper.LogoutRequest();
            imuLabHelper.LoginRequest(oldTask.Username, oldTask.Password);
            switch (oldTask.Type) 
            {
                case "1":
                    taskList.RemoveAt(0);
                    JObject obj = imuLabHelper.FindSeat("100485887", date, start, end, fr_start, fr_end);
                    if (obj != null)
                    {
                        JToken o = obj["data"];
                        for (int i = 0; i < o.Count(); i++)
                        {
                            if (!o[i]["ts"].Any())
                            {
                                imuLabHelper.SetResv(
                                    (string) o[i]["devId"],
                                    (string) o[i]["labId"],
                                    (string) o[i]["kindId"],
                                    resv_start,
                                    resv_end
                                );
                                break;
                            }
                        }
                        DateTime qtDate = DateHelper.ToDateTime(oldTask.Time).AddMinutes(8);
                        return new Task(
                            oldTask.Uid, 
                            oldTask.Username, 
                            oldTask.Password,
                            oldTask.Rule,
                            Task.QUXIAOYUYUE,
                            DateHelper.ToLong(qtDate)
                            );
                    }
                    return null;
                    
                case "2":
                    taskList.RemoveAt(0);
                    JObject obj2 = imuLabHelper.CheckMyResv();
                    if (obj2 != null)
                    {
                        JToken o = obj2["data"];
                        if (o[0].Any())
                        {
                            if (((string) o[0]["status"]).Length<=5)
                            {
                                imuLabHelper.SignInResv((string)o[0]["devId"], "100482081", (string)o[0]["id"]);
                                imuLabHelper.SignOutResv((string)o[0]["id"]);
                                System.Diagnostics.Debug.WriteLine("用户未签到，已成功签退！");
                            }
                            else 
                            {
                                DateTime qtDate = DateHelper.ToDateTime(oldTask.Time)
                                    .AddHours(int.Parse(oldTask.Rule.LoopTime))
                                    .AddMinutes(-1);
                                return new Task(
                                    oldTask.Uid,
                                    oldTask.Username,
                                    oldTask.Password,
                                    oldTask.Rule,
                                    Task.QIANTUI,
                                    DateHelper.ToLong(qtDate)
                                );
                            }
                        }
                    }
                    return null;
                case "3":
                    taskList.RemoveAt(0);
                    JObject obj3 = imuLabHelper.CheckMyResv();
                    if (obj3 != null)
                    {
                        JToken o = obj3["data"];
                        if (!o[0].Any())
                        {
                            imuLabHelper.SignInResv((string) o[0]["dev"], "100482081", (string) o[0]["id"]);
                        }
                    }
                    return null;
                case "4":
                    taskList.RemoveAt(0);
                    JObject obj4 = imuLabHelper.CheckMyResv();
                    if (obj4 != null)
                    {
                        JToken o = obj4["data"];
                        if (!o[0].Any())
                        {
                            imuLabHelper.SignOutResv((string)o[0]["id"]);
                        }
                    }
                    return null;
                default: return null;
            }
        }

        public static void LogTaskList()
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                Task task = (Task)taskList[i];
                System.Diagnostics.Debug.WriteLine(i+" "+ javaScriptTime.AddMilliseconds(task.Time)+" "+task.Username);
            }
            
        }

        /// <summary>
        /// 初始化队列，从数据库中将每个用户的任务读取出来
        /// </summary>
        public static void InitQueue()
        {
            userList = UserDAL.GetAllactivateUser();
            for (int i = 0; i < userList.Count; i++)
            {
                User user = (User)userList[i];
                Task task = GetUserNextTask(user);
                PushTastToQueue(task);
            }
            LogTaskList();
        }

        /// <summary>
        /// 获取用户下一个任务
        /// </summary>
        public static Task GetUserNextTask(User user)
        {
            string[] rules = user.Rule.Split('?');
            double minTime = long.MaxValue;
            Task nextTask = null;

            for (int i = 0; i < rules.Length; i++)
            {
                string[] rule = rules[i].Split(',');
                double time  = GetNextTaskTime(new Rule(rule[0], rule[1], rule[2]));
                if (time < minTime)
                {
                    minTime = time;
                    nextTask = new Task(user.Uid, user.Username, user.Password, new Rule(rule[0], rule[1], rule[2]), Task.YUYUE, time);
                }
            }
            return nextTask;
        }

        public static void RemoveUserAllTask(User user)
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                if (((Task) taskList[i]).Uid.Equals(user.Uid))
                {
                    taskList.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// 获取下次任务的时间
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static double GetNextTaskTime(Rule rule)
        {
            double nextTimeLong = 0;

            DateTime startTime = rule.GetStartTime();
            DateTime nowTime = DateTime.Now;
            double startTimeLong = (startTime - javaScriptTime).TotalMilliseconds;
            double nowTimeLong = (nowTime - javaScriptTime).TotalMilliseconds;

            if (nowTimeLong < startTimeLong)
            {
                nextTimeLong = startTimeLong;
            }
            else
            {
                double loopValue = int.Parse(rule.LoopTime)*24*60*60*1000;
                double d_value = (nowTimeLong - startTimeLong) % loopValue;
                nextTimeLong = (loopValue - d_value) + nowTimeLong;
            }
            return nextTimeLong;
        }

        /// <summary>
        /// 将任务插入任务队列
        /// </summary>
        public static void PushTastToQueue(Task newTask)
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                Task ts = (Task) taskList[i];
                if (newTask.Time < ts.Time)
                {
                    taskList.Insert(i,newTask);
                    return;
                }
            }
            taskList.Add(newTask);
        }
    }
}