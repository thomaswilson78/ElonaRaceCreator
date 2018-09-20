using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace ElonaRaceCreator
{
    public class SQLiteDB
    {
        public static SQLiteConnection Conn;

        public static bool EstablishConnection()
        {
            try
            {
                if (!File.Exists(@".\o_skill.db"))
                    throw new Exception("Cannot find \"o_skill.db\". Please make sure your are running the program in the elona/data/ folder.");
                Conn = new SQLiteConnection("Data Source=o_skill.db;Version=3;");
                Conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (Conn != null && Conn.State == System.Data.ConnectionState.Open)
                    Conn.Close();
                return false;
            }
            return true;
        }

        public static void TerminateConnection()
        {
            if (Conn != null && Conn.State == System.Data.ConnectionState.Open)
                Conn.Close();
        }

        public static SQLiteDataReader ReturnQuery(string query, string[] values, string[] paramaters)
        {
            SQLiteDataReader reader = null;
            try
            {
                if (EstablishConnection())
                {
                    SQLiteCommand cmd = new SQLiteCommand(query, Conn);
                    if(values!=null)
                        for (int i = 0; i < values.Length; i++)
                            cmd.Parameters.AddWithValue(paramaters[i], values[i]);
                    Console.WriteLine(cmd.ToString());
                    reader = cmd.ExecuteReader();
                }
                else
                    throw new Exception("Unable to establish connection to database.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                TerminateConnection();
                reader = null;
            }
            return reader;
        }
    }
}
