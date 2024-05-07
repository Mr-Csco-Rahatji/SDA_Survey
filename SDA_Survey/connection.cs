using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SDA_Survey
{

    class Connection
    {
        private static string myconnectionstring = "Server=localhost;Database=sda_base;uid=root;pwd=25121968;";
        private MySqlConnection con;
        public List<User> users;
        
        public Connection()
        {}

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

        public List<User> getAllUsers()
        {
            users = new List<User>();
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();

            cmd.CommandText = "SELECT * FROM sda_base.user;";
            MySqlDataReader reader= cmd.ExecuteReader();
            while(reader.Read())
            {
                User user = new User();
                user.full_names = reader.GetString("Full_Names");
                user.email = reader.GetString("email");
                user.dob = reader.GetDateTime("date_of_birth");
                user.contact = reader.GetString("contact");
                user.foodList = new List<FavFood>();
                user.likes = new List<Like>();
                users.Add(user);
            }
            reader.Close();
            CloseConnection();
            PopulatePasta(users);
            PopulatePizza(users);
            PopulateOther(users);
            PopulatePap(users);
            PopulateMovies(users);
            PopulateRadio(users);
            PopulateEat(users);
            PopulateTv(users);
            return users;
        }

        public void PopulatePasta(List<User> users)
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd= con.CreateCommand();
            for (int a=0;a<users.Count; a++)
            {
                cmd.CommandText = "SELECT * FROM sda_base.pasta where email='" + users[a].email + "' and contact='" + users[a].contact + "' ;";
                MySqlDataReader reader= cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    FavFood fav = new FavFood(Type.PASTA);
                    users[a].foodList.Add(fav);
                }
                reader.Close() ;
            }
            CloseConnection();
        }

        public void PopulatePizza(List<User> users)
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            for (int a = 0; a < users.Count; a++)
            {
                cmd.CommandText = "SELECT * FROM sda_base.pizza where email='" + users[a].email + "' and contact='" + users[a].contact + "' ;";
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    FavFood fav = new FavFood(Type.PIZZA);
                    users[a].foodList.Add(fav);
                }
                reader.Close();
            }
            CloseConnection();
        }

        public void PopulatePap(List<User> users)
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            for (int a = 0; a < users.Count; a++)
            {
                cmd.CommandText = "SELECT * FROM sda_base.pap_and_wors where email='" + users[a].email + "' and contact='" + users[a].contact + "' ;";
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    FavFood fav = new FavFood(Type.PAP_AND_WORS);
                    users[a].foodList.Add(fav);
                }
                reader.Close();
            }
            CloseConnection();
        }

        public void PopulateOther(List<User> users)
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            for (int a = 0; a < users.Count; a++)
            {
                cmd.CommandText = "SELECT * FROM sda_base.other where email='" + users[a].email + "' and contact='" + users[a].contact + "' ;";
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    FavFood fav = new FavFood(Type.OTHER);
                    users[a].foodList.Add(fav);
                }
                reader.Close();
            }
            CloseConnection();
        }

        public void PopulateTv(List<User> users)
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            for (int a = 0; a < users.Count; a++)
            {
                cmd.CommandText = "SELECT * FROM sda_base.watch_tv where email='" + users[a].email + "' and contact='" + users[a].contact + "' ;";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Like like = new Like();
                    like.type = TYPE.TV;
                    like.rating = reader.GetInt32("rating");
                    users[a].likes.Add(like);
                }
                reader.Close();
            }
            CloseConnection();
        }

        public void PopulateEat(List<User> users)
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            for (int a = 0; a < users.Count; a++)
            {
                cmd.CommandText = "SELECT * FROM sda_base.eat_out where email='" + users[a].email + "' and contact='" + users[a].contact + "' ;";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Like like = new Like();
                    like.type = TYPE.EAT;
                    like.rating = reader.GetInt32("rating");
                    users[a].likes.Add(like);
                }
                reader.Close();
            }
            CloseConnection();
        }

        public void PopulateRadio(List<User> users)
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            for (int a = 0; a < users.Count; a++)
            {
                cmd.CommandText = "SELECT * FROM sda_base.listen_to_radio where email='" + users[a].email + "' and contact='" + users[a].contact + "' ;";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Like like = new Like();
                    like.type = TYPE.RADIO;
                    like.rating = reader.GetInt32("rating");
                    users[a].likes.Add(like);
                }
                reader.Close();
            }
            CloseConnection();
        }

        public void PopulateMovies(List<User> users)
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            for (int a = 0; a < users.Count; a++)
            {
                cmd.CommandText = "SELECT * FROM sda_base.watch_movies where email='" + users[a].email + "' and contact='" + users[a].contact + "' ;";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Like like = new Like();
                    like.type = TYPE.MOVIES;
                    like.rating = reader.GetInt32("rating");
                    users[a].likes.Add(like);
                }
                reader.Close();
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

        public double getAvgAge()
        {
            double totalAge = 0;
            double avgAge = 0;
            Connection con = new Connection();
            List<User> users = new List<User>();
            users=con.getAllUsers();    
            for(int a=0;a<users.Count;a++)
            {
                totalAge += (DateTime.Now.Year - users[a].dob.Year);
            }
            avgAge = totalAge/users.Count;

            Math.Round(avgAge, 2, MidpointRounding.AwayFromZero);

            return avgAge;
        }

        public int getYoung()
        {
            Connection con = new Connection();
            List<User> users = new List<User>();
            users = con.getAllUsers();
            int young = DateTime.Now.Year- users[0].dob.Year;
            for(int a=1;a<users.Count; a++)
            {
                if(DateTime.Now.Year-users[a].dob.Year <= young)
                {
                    young = DateTime.Now.Year- users[a].dob.Year;
                }
            }
            return young;
        }

        public int getOld()
        {
            Connection con = new Connection();
            List<User> users = new List<User>();
            users = con.getAllUsers();
            int old = DateTime.Now.Year - users[0].dob.Year;
            for (int a = 1; a < users.Count; a++)
            {
                if (DateTime.Now.Year -users[a].dob.Year >= old)
                {
                    old = DateTime.Now.Year-users[a].dob.Year;
                }
            }
            return old;
        }

        public double getPizza()
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            double number=0;
                cmd.CommandText = "SELECT COUNT(email)  FROM sda_base.pizza;";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    number = reader.GetDouble("count(email)");
                }
                reader.Close();
            CloseConnection();
            Connection con1 = new Connection();
            List<User> users = new List<User>();
            users = con1.getAllUsers();
            return (number/users.Count)*100;
        }

        public double getPasta()
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            double number = 0;
            cmd.CommandText = "SELECT COUNT(email)  FROM sda_base.pasta;";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                number = reader.GetDouble("count(email)");
            }
            reader.Close();
            CloseConnection();
            Connection con1 = new Connection();
            List<User> users = new List<User>();
            users = con1.getAllUsers();
            return (number/users.Count)*100;
        }

        public double getPap()
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            double number = 0;
            cmd.CommandText = "SELECT COUNT(email)  FROM sda_base.pap_and_wors;";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                number = reader.GetDouble("count(email)");
            }
            reader.Close();
            CloseConnection();
            Connection con1 = new Connection();
            List<User> users = con1.getAllUsers();
            users = con1.getAllUsers();
            return (number/users.Count)*100;
        }

        public double getMovies()
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            double number = 0;
            cmd.CommandText = "SELECT AVG(rating)  FROM sda_base.watch_movies;";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                number = reader.GetDouble("AVG(rating)");
            }
            reader.Close();
            return number;
        }

        public double getRadio()
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            double number = 0;
            cmd.CommandText = "SELECT AVG(rating)  FROM sda_base.listen_to_radio;";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                number = reader.GetDouble("AVG(rating)");
            }
            reader.Close();
            return number;
        }

        public double getEatOut()
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            double number = 0;
            cmd.CommandText = "SELECT AVG(rating)  FROM sda_base.eat_out;";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                number = reader.GetDouble("AVG(rating)");
            }
            reader.Close();
            return number;
        }

        public double getWatchTv()
        {
            OpenConnection();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            double number = 0;
            cmd.CommandText = "SELECT AVG(rating)  FROM sda_base.watch_tv;";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                number = reader.GetDouble("AVG(rating)");
            }
            reader.Close();
            return number;
        }
    }
}
