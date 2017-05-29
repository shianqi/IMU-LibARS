using System;
using Newtonsoft.Json.Linq;

namespace MyAspWeb
{
    public class IMULabHelper
    {
        private string loginUrl = "http://202.207.7.180:8081/ClientWeb/pro/ajax/login.aspx?";

        private string resvUrl = "http://202.207.7.180:8081/ClientWeb/pro/ajax/reserve.aspx?";

        private string deviceUrl = "http://202.207.7.180:8081/ClientWeb/pro/ajax/device.aspx?";

        private HttpRequestHelper http = null;


        public IMULabHelper()
        {
            http = new HttpRequestHelper();
        }

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns>时间戳字符串</returns>
        public string getTime()
        {
            //_nocache time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 图书馆模拟登陆操作
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>登陆是否成功</returns>
        public bool LoginRequest(string username, string password)
        {
            
            string data = "act=login" + "&id=" + username + "&pwd=" + password + "&role=512&_nocache=" + getTime();

            JObject obj = http.SendGetRequest(loginUrl + data);
            string res = (string)obj["msg"];
            if (res.Equals("ok"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 图书馆模拟注销操作
        /// </summary>
        /// <returns></returns>
        public bool LogoutRequest()
        {
            string data = "act=logout" + "&_nocache=" + getTime();

            JObject obj = http.SendGetRequest(loginUrl + data);
            string res = (string)obj["msg"];
            if (res.Equals("操作成功！"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查看我的已预定信息
        /// </summary>
        /// <returns></returns>
        public JObject CheckMyResv()
        {
            string data = "stat_flag=9&act=get_my_resv&_nocache="+getTime();

            JObject obj = http.SendGetRequest(resvUrl + data);
            string res = (string)obj["msg"];
            if (res.Equals("ok"))
            {
                return obj;
            }
            return null;
        }

        /// <summary>
        /// 删除没有进行的预约
        /// </summary>
        /// <param name="resv_id">预约ID</param>
        /// <returns></returns>
        public bool DelMyResv(string resv_id)
        {
            string data = String.Format("act=del_resv&id={0}&_nocache={1}", resv_id, getTime());
            JObject obj = http.SendGetRequest(resvUrl + data);
            string res = (string)obj["msg"];
            if (res.Equals("ok"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 签到操作，谨慎使用
        /// </summary>
        /// <param name="dev_id">座位ID</param>
        /// <param name="lab_id">图书馆位置ID（楼层）</param>
        /// <param name="resv_id">预约ID</param>
        /// <returns>返回是否签到完成</returns>
        public bool SignInResv(string dev_id, string lab_id, string resv_id)
        {
            string data = String.Format("act=resv_checkin&dev_id={0}&lab_id={1}&resv_id={2}",dev_id,lab_id,resv_id);

            JObject obj = http.SendGetRequest(resvUrl + data);
            string res = (string)obj["ret"];
            System.Diagnostics.Debug.WriteLine((string)obj["msg"]);
            if (res.Equals("1"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 签退操作，谨慎使用
        /// </summary>
        /// <param name="resv_id"></param>
        /// <returns></returns>
        public bool SignOutResv(string resv_id)
        {
            string data = String.Format("act=resv_leave&type=2&resv_id={0}&_nocache={1}", resv_id, getTime());

            JObject obj = http.SendGetRequest(resvUrl + data);
            string res = (string)obj["ret"];
            System.Diagnostics.Debug.WriteLine((string)obj["msg"]);
            if (res.Equals("1"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查找可预约座位
        /// </summary>
        /// <param name="room_id">座位ID</param>
        /// <param name="date">预约日期 例如：2017-05-03</param>
        /// <param name="start">查找开始时间 例如：14:10</param>
        /// <param name="end">查找结束时间 例如：17:10</param>
        /// <param name="fr_start">预约开始时间 例如：15:10</param>
        /// <param name="fr_end">预约结束时间 例如：18:10</param>
        /// <returns></returns>
        public JObject FindSeat(string room_id, string date, string start, string end, string fr_start, string fr_end)
        {
            string data = String.Format("right=detail&classkind=8&room_id={0}&name=1B%E5%8C%BA&" +
                                        "open_start=730&open_end=2200classkind&date={1}&start={2}&end={3}&" +
                                        "fr_start={4}&fr_end={5}&act=get_rsv_sta&_nocache={6}",
                                        room_id, date, start, end, fr_start, fr_end, getTime());
            JObject obj = http.SendGetRequest(deviceUrl + data);
            string res = (string)obj["ret"];
            System.Diagnostics.Debug.WriteLine((string)obj["msg"]);
            if (res.Equals("1"))
            {
                return obj;
            }
            return null;
        }

        /// <summary>
        /// 预约座位
        /// </summary>
        /// <param name="dev_id">座位ID 例如：100485934</param>
        /// <param name="lab_id">图书馆位置ID（楼层） 例如：100482081</param>
        /// <param name="kind_id">不知道什么ID 例如：100485569</param>
        /// <param name="start">开始时间 例如：2017-05-03%2018:55</param>
        /// <param name="end">结束时间 例如：2017-05-03%2019:55</param>
        /// <returns></returns>
        public bool SetResv(string dev_id, string lab_id, string kind_id, string start, string end)
        {
            string data = String.Format("dev_id={0}&lab_id={1}&kind_id={2}&type=dev&prop=&test_id=&" +
                                        "resv_id=&term=&min_user=&max_user=&mb_list=&test_name=&" +
                                        "start={3}&end={4}&memo=&act=set_resv&_nocache={5}",
                                        dev_id, lab_id, kind_id, start, end, getTime());
            JObject obj = http.SendGetRequest(resvUrl + data);
            string res = (string)obj["ret"];
            System.Diagnostics.Debug.WriteLine((string)obj["msg"]);
            if (res.Equals("1"))
            {
                return true;
            }
            return false;
        }
    }
}