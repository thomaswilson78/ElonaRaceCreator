using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ElonaRaceCreator
{
    public class Skill
    {
        public string ID { get; set; }
        public string Name { get; set; }
        private int level = 0;
        public int Level
        {
            get {
                return level;
            }
            set {
                if (value < 0)
                    level = 0;
                else if (value > 999)
                    level = 999;
                else
                    level = value;
            }
        }
        public bool Enabled { get; set; }

        public Skill(string skl)
        {
            ID = skl.Substring(0, 3);
            Level = int.Parse(skl.Substring(3, 4));
            SQLiteDataReader rdr = SQLiteDB.ReturnQuery("SELECT `name-e` FROM o_skill WHERE id=@id;", new string[] { ID }, new string[] { "@id" });
            if(rdr!=null)
            {
                while(rdr.Read())
                {
                    Name = rdr[0].ToString();
                    rdr.Close();
                    break;
                }
                SQLiteDB.TerminateConnection();
            }
        }

        private string PrintLevel()
        {
            string s = Level.ToString();
            while(s.Length<4)
            {
                s = "0" + s;
            }
            return s;
        }

        public override string ToString()
        {
            return ID + PrintLevel();
        }

        //public static 
    }
}
