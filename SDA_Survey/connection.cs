using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SDA_Survey
{

    class Connection
    {
        private static string myconnectionstring = "Server=localhost;Database=sda_base;uid=root;pwd=25121968;";
        private MySqlConnection con;
        
        public Connection()
        {
           
        }

        public void OpenConnection()
        {
            try
            {
                con = new MySqlConnection(myconnectionstring);
                con.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Can't Connect");
                Console.Write(ex.Message.ToString().Trim());
            }
        }

        private void CloseConnection()
        {
            con.Close();
        }


        public 

        public int checkUser(String contact,string email )
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM sda_base.user where contact='"+contact+"' and email='"+email+"';";
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                CloseConnection();
                return 1;
            }
            else
            {
                CloseConnection();
                return 0;
            }
        }
        public bool addUser(User user)
        {
            if (checkUser(user.contact, user.email) == 0)
            {
                OpenConnection();
                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO `sda_base`.`user`(`Full_Names`,`email`,`date_of_birth`,`contact`)" +
                    "VALUES('" + user.full_names + "'," +
                    "'" + user.email + "'," +
                    "'" + user.dob + "'," +
                    "'" + user.contact + "')";
                cmd.ExecuteNonQuery();
                for (int a = 0; a < user.foodList.Count; a++)
                {
                    cmd.CommandText = "INSERT INTO `sda_base`.`" + user.foodList[a].type + "` (`email`, `contact`) " +
                        "VALUES ('" + user.email + "', '" + user.contact + "');";
                    cmd.ExecuteNonQuery();
                }
                cmd.CommandText = "INSERT INTO `sda_base`.`watch_movies` (`email`, `contact`, `rating`) " +
                    "VALUES ('" + user.email + "', " +
                    "'" + user.contact + "', " +
                    "'" + user.likes[0].rating + "');";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO `sda_base`.`listen_to_radio` (`email`, `contact`, `rating`) " +
                    "VALUES ('" + user.email + "', " +
                    "'" + user.contact + "', " +
                    "'" + user.likes[1].rating + "');";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO `sda_base`.`eat_out` (`email`, `contact`, `rating`) " +
                    "VALUES ('" + user.email + "', " +
                    "'" + user.contact + "', " +
                    "'" + user.likes[2].rating + "');";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO `sda_base`.`watch_tv` (`email`, `contact`, `rating`) " +
                    "VALUES ('" + user.email + "', " +
                    "'" + user.contact + "', " +
                    "'" + user.likes[3].rating + "');";
                cmd.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public void getAllUsers()
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM sda_base.user;";
            MySqlDataReader reader= cmd.ExecuteReader();
            while(reader.Read())
            {
                User user1 = new User();
                user1.full_names = reader.GetString("Full_Names");

                Console.WriteLine("User is " + user1.full_names);
            }
            CloseConnection();
        }

        public int getNumberOfSurveys()
        {
            OpenConnection ();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COUNT(Full_Names) FROM sda_base.user;";
            MySqlDataReader reader = cmd.ExecuteReader();
            int nSurveys = -1;
            while (reader.Read())
            {
                nSurveys = reader.GetInt32("COUNT(Full_Names)");
            }
            CloseConnection();
            return nSurveys;
        }
    }
}
