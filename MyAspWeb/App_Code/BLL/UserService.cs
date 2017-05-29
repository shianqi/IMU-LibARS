using MyAspWeb.DAL;
using MyAspWeb.Model;
using MySql.Data.MySqlClient;

namespace MyAspWeb
{
    public class UserService
    {

        public UserService()
        {

        }

        /// <summary>
        /// 用户登陆操作
        /// </summary>
        /// ①用用户名和密码去登陆图书馆系统，如果登陆成功，进行②，否则返回失败
        /// ②检查数据库中是否存在该用户，存在则进行③，否则将该用户存入数据库
        /// ③将该用户的用户名和密码和数据库中的对比，不同则更新
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserLogin(string username, string password)
        {
            IMULabHelper helper = new IMULabHelper();
            if (helper.LoginRequest(username, password))
            {
                User user = UserDAL.findUserByUsername(username);
                if (user!=null)
                {
                    //如果密码不同，则更新密码
                    if (user.Password != password)
                    {
                        if (!UserDAL.modifyUserPassword(user, password))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //将该用户存入数据库
                    User tempUser = new User(username, password);
                    if (!UserDAL.saveUser(tempUser))
                    {
                        return false;
                    }
                }

                
                return true;
            }
            return false;
        }
    }
}