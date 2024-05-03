using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Survey
{

    class connection
    {
        private static string myconnectionstring = "Server=localhost;Database=sda_base;uid=root;pwd=25121968;";
        private MySqlConnection con;
        
        public connection()
        {
            Console.WriteLine("Here  number 1");
            con = new MySqlConnection(myconnectionstring);
            con.Open();
        }

        public bool addUser()
        {
            MySqlCommand cmd;
            cmd= con.CreateCommand();
            cmd.CommandText = "INSERT INTO `sda_base`.`user`(`Full_Names`,`email`,`date_of_birth`,`contact`)" +
                "VALUES('Test1','dsffdfdfdfdg@gmail.com','2005-04-11','1111144444')";
            cmd.ExecuteNonQuery();
            con.Close();
            Console.WriteLine("Here number 2");
            return  true;
        }
    }
}
