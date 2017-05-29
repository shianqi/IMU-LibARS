using System;
using System.Collections;
using MyAspWeb.Model;
using MySql.Data.MySqlClient;

namespace MyAspWeb.DAL
{
    public class UserDAL
    {
        /// <summary>
        /// 获取所有激活任务用户
        /// </summary>
        /// <returns>ArrayList 所有激活任务用户</returns>
        public static ArrayList GetAllactivateUser()
        {
            ArrayList array = new ArrayList(); 
            string sqlStr = "select * from User where state='1' and rule!=''";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sqlStr);
            while (reader.Read())
            {
                int uid = (int)reader["uid"];
                string _username = (string)reader["username"];
                string _password = (string)reader["password"];
                string state = (string)reader["state"];
                string rule = (string)reader["rule"];

                User user = new User(uid, _username, _password, state, rule);
                array.Add(user);
            }
            reader.Close();
            return array;
        }

        public static User findUserByUsername(string username)
        {
            string sqlStr = string.Format("select * from User where username={0}", username);
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sqlStr);
            if (reader.HasRows)
            {
                reader.Read();
                int uid = (int) reader["uid"];
                string _username = (string) reader["username"];
                string _password = (string) reader["password"];
                string state = (string) reader["state"];
                string rule = (string) reader["rule"];

                User user = new User(uid,_username, _password,state,rule);
                reader.Close();
                return user;
            }
            else
            {
                reader.Close();
                return null;
            }
        }

        public static bool saveUser(User user)
        {
            try
            {
                string str = string.Format("insert into User " +
                                           "values (0, '{0}', '{1}', '{2}', '{3}');",
                    user.Username, user.Password,
                    user.State, user.Rule);
                MySqlDataReader dataReader = MysqlHelper.ExecuteReader(str);
                dataReader.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool modifyUserPassword(User user, string password)
        {
            try
            {
                string str = string.Format("update User set password='{0}' where uid='{1}'", password, user.Uid);
                MySqlDataReader dataReader = MysqlHelper.ExecuteReader(str);
                dataReader.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool modifyUserState(User user, string state)
        {
            try
            {
                string str = string.Format("update User set state='{0}' where uid='{1}'", state, user.Uid);
                MySqlDataReader dataReader = MysqlHelper.ExecuteReader(str);
                dataReader.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool modifyUserRule(User user, string rule)
        {
            try
            {
                string str = string.Format("update User set rule='{0}' where uid='{1}'", rule, user.Uid);
                MySqlDataReader dataReader = MysqlHelper.ExecuteReader(str);
                dataReader.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}