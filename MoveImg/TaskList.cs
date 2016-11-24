using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace MoveImg
{
    class TaskList
    {
        public static string DB = "Server=123.232.102.241;Port=28112;Database=ent;Uid=yunrunmysql; Password=Yunrunmysql2016!@#;CharSet=utf8";
        public static void AddUrlList(UrlList urlList)
        {

            try
            {
                string sql = "INSERT INTO urllist (URL,ImgName,Type,Status,DownLoad,TaskID) SELECT @URL, @ImgName,@Type,@Status,@DownLoad,@TaskID FROM DUAL WHERE NOT EXISTS (SELECT URL FROM urllist WHERE Type=@Type And TaskID=@TaskID And ImgName=@ImgName)";
                MySqlParameter[] Parameter = new MySqlParameter[6];
                Parameter[0] = new MySqlParameter("@URL", urlList.URL);
                Parameter[1] = new MySqlParameter("@ImgName", urlList.ImgName);
                Parameter[2] = new MySqlParameter("@Type", urlList.Type);
                Parameter[3] = new MySqlParameter("@Status", urlList.Status);
                Parameter[4] = new MySqlParameter("@DownLoad", urlList.DownLoad);
                Parameter[5] = new MySqlParameter("@TaskID", urlList.TaskID);

                bool res = Insert(sql, Parameter);
                if (res)
                {
                    Console.WriteLine("成功添加到urllist表：" + urlList.TaskID);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("false.txt", ex.ToString() + "\r\n");
                Console.WriteLine(ex);

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool Insert(string sql, MySqlParameter[] parameter)
        {
            bool res = false;
            MySqlConnection sqlcon = new MySqlConnection(DB);
            // MySqlConnection sqlcon = new MySqlConnection(DB);
            try
            {
                sqlcon.Open();
                MySqlCommand sqlcom = new MySqlCommand(sql, sqlcon);
                sqlcom.Parameters.AddRange(parameter);
                int a = sqlcom.ExecuteNonQuery();
                if (a > 0)
                {
                    res = true;
                }
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                sqlcon.Close();
            }
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GatDate(string sql)
        {
            DataTable dt = new DataTable();
            MySqlConnection sqlcon = new MySqlConnection(DB);

            try
            {
                sqlcon.Open();
                MySqlDataAdapter sqlda = new MySqlDataAdapter(sql, sqlcon);
                sqlda.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                    sqlcon = null;
                }
            }
            return dt;
        }
        public static List<Img> GetSmallImg()
        {
            using (MySqlConnection conn = new MySqlConnection(DB))
            {
                conn.Open();//打开数据库  
                List<Img> Luser = new List<Img>();


                //创建数据库命令  
                MySqlCommand cmd = conn.CreateCommand();
                //创建查询语句  
                cmd.CommandText = "SELECT id, SmallPicture,SmallPictureUrl  FROM ent.star  WHERE  ENABLE =1";
                //从数据库中读取数据流存入reader中  
                MySqlDataReader reader = cmd.ExecuteReader();

                //从reader中读取下一行数据,如果没有数据,reader.Read()返回flase  
                while (reader.Read())
                {
                    Img user = new Img();
                    //reader.GetOrdinal("id")是得到ID所在列的index,  
                    //reader.GetInt32(int n)这是将第n列的数据以Int32的格式返回  
                    //reader.GetString(int n)这是将第n列的数据以string 格式返回  
                    user.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    user.ImgName = reader["SmallPicture"] is DBNull ? "" : (string)reader["SmallPicture"];
                    user.URL = reader["SmallPictureUrl"] is DBNull ? "" : (string)reader["SmallPictureUrl"];
                    Luser.Add(user);
                    //格式输出数据  
                    //Console.Write("ID:{0},Type:{1},Name:{2},Img:{3},Imagelist:{4}\n", user.ID, user.Type, user.Name, user.Img, user.Imagelist);
                }
                return Luser;
            }
        }
        public static List<Img> GetBigImg()
        {
            using (MySqlConnection conn = new MySqlConnection(DB))
            {
                conn.Open();
                List<Img> Luser = new List<Img>();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT id, BigPicture,BigPictureUrl  FROM ent.star  WHERE  ENABLE =1";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Img user = new Img();
                    user.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    user.ImgName = reader["BigPicture"] is DBNull ? "" : (string)reader["BigPicture"];
                    user.URL = reader["BigPictureUrl"] is DBNull ? "" : (string)reader["BigPictureUrl"];
                    Luser.Add(user);
                }
                return Luser;
            }
        }

        public static List<Img> GetHaiBaoImg()
        {
            using (MySqlConnection conn = new MySqlConnection(DB))
            {
                conn.Open();
                List<Img> Luser = new List<Img>();
                MySqlCommand cmd = conn.CreateCommand();
                // cmd.CommandText = "SELECT id, ImgUrl,PicImg  FROM ent.work  WHERE  type in (2,3)";
                cmd.CommandText = "SELECT id, ImgUrl,PicImg  FROM ent.work WHERE id IN(SELECT workid FROM `star_work` WHERE starid =89)  AND TYPE IN (2,3)";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Img user = new Img();
                    user.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    user.ImgName = reader["PicImg"] is DBNull ? "" : (string)reader["PicImg"];
                    user.URL = reader["ImgUrl"] is DBNull ? "" : (string)reader["ImgUrl"];
                    Luser.Add(user);
                }
                return Luser;
            }
        }

        public static List<Img> GetZhuanJi()
        {
            using (MySqlConnection conn = new MySqlConnection(DB))
            {
                conn.Open();
                List<Img> Luser = new List<Img>();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT id, ImgUrl,PicImg  FROM ent.work  WHERE  type in (5)";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Img user = new Img();
                    user.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    user.ImgName = reader["PicImg"] is DBNull ? "" : (string)reader["PicImg"];
                    user.URL = reader["ImgUrl"] is DBNull ? "" : (string)reader["ImgUrl"];
                    Luser.Add(user);
                }
                return Luser;
            }
        }

        public static List<Img> GetZongYi()
        {
            using (MySqlConnection conn = new MySqlConnection(DB))
            {
                conn.Open();
                List<Img> Luser = new List<Img>();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT id, Img,PicImg  FROM ent.variety  WHERE  type >0";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Img user = new Img();
                    user.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    user.ImgName = reader["PicImg"] is DBNull ? "" : (string)reader["PicImg"];
                    user.URL = reader["Img"] is DBNull ? "" : (string)reader["Img"];
                    Luser.Add(user);
                }
                return Luser;
            }
        }

        public static List<Img> GetJuZhao()
        {
            using (MySqlConnection conn = new MySqlConnection(DB))
            {
                conn.Open();
                List<Img> Luser = new List<Img>();
                MySqlCommand cmd = conn.CreateCommand();
                //cmd.CommandText = "SELECT id, Imagelist  FROM ent.work  WHERE  type in (2,3)";
                cmd.CommandText = "SELECT id, Imagelist   FROM ent.work WHERE id IN(SELECT workid FROM `star_work` WHERE starid =89)  AND TYPE IN (2,3)";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Img user = new Img();
                    user.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    user.ImgName = reader["Imagelist"] is DBNull ? "" : (string)reader["Imagelist"];
                    //user.URL = reader["Img"] is DBNull ? "" : (string)reader["Img"];
                    Luser.Add(user);
                }
                return Luser;
            }
        }


        public static List<Img> GetXieZhen()
        {
            using (MySqlConnection conn = new MySqlConnection(DB))
            {
                conn.Open();
                List<Img> Luser = new List<Img>();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT id, Imagelist  FROM ent.star  WHERE  ENABLE =1";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Img user = new Img();
                    user.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    user.ImgName = reader["Imagelist"] is DBNull ? "" : (string)reader["Imagelist"];
                    //user.URL = reader["Img"] is DBNull ? "" : (string)reader["Img"];
                    Luser.Add(user);
                }
                return Luser;
            }
        }


        /// <summary>
        /// 从star表数据库拿名字
        /// </summary>
        /// <returns></returns>
        public static string GetStarName(int id)
        {
            string name = "";
            using (MySqlConnection conn = new MySqlConnection(DB))
            {
                conn.Open();//打开数据库  
                // List<User> Luser = new List<User>();


                //创建数据库命令  
                MySqlCommand cmd = conn.CreateCommand();
                //创建查询语句  
                string sql = string.Format("SELECT NAME FROM `star` WHERE id IN(SELECT starid FROM `star_work` WHERE workid ='{0}')", id);
                cmd.CommandText = sql;
                //cmd.CommandText = "SELECT id, TYPE,NAME,Img,Imagelist  FROM ent.work  WHERE  TYPE ='5'";
                //从数据库中读取数据流存入reader中  
                MySqlDataReader reader = cmd.ExecuteReader();

                //从reader中读取下一行数据,如果没有数据,reader.Read()返回flase  
                while (reader.Read())
                {

                    //reader.GetOrdinal("id")是得到ID所在列的index,  
                    //reader.GetInt32(int n)这是将第n列的数据以Int32的格式返回  
                    //reader.GetString(int n)这是将第n列的数据以string 格式返回  
                    name = reader["Name"] is DBNull ? "" : (string)reader["Name"];
                    //格式输出数据  
                    //Console.Write("ID:{0},Type:{1},Name:{2},Img:{3},Imagelist:{4}\n", user.ID, user.Type, user.Name, user.Img, user.Imagelist);
                }
                return name;
            }
        }
    }
}
