using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElonaRaceCreator
{
    class Races
    {
        #region variables
        public string jpname, name, skill, trait, desc_j, desc_e;
        public int id, spd, pic1, pic2, dv, pv, hp, mp, minAge, ageRange, breeder, height;
        public bool playable, blood, head, neck, back, body, handr, handl, ringr, ringl, arm, waist, leg;
        public byte str, end, dex, per, ler, wil, mag, chr, sex, mstyle, cstyle, resist;

        List<Skill> Skills = new List<Skill>();
        #endregion

        public Races(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public Races(string[] arr)
        {
            AddInfo(arr);
        }
        
        public void AddInfo(string[] arr)
        {
            jpname = arr[0];
            name = arr[1];
            id = IntParse(arr[2]);
            playable = arr[3].Equals("1");
            sex = ByteParse(arr[4]);
            pic1 = IntParse(arr[5]);
            pic2 = IntParse(arr[6]);
            dv = IntParse(arr[7]);
            pv = IntParse(arr[8]);
            hp = IntParse(arr[9]);
            mp = IntParse(arr[10]);
            str = ByteParse(arr[11]);
            end = ByteParse(arr[12]);
            dex = ByteParse(arr[13]);
            per = ByteParse(arr[14]);
            ler = ByteParse(arr[15]);
            wil = ByteParse(arr[16]);
            mag = ByteParse(arr[17]);
            chr = ByteParse(arr[18]);
            spd = IntParse(arr[19]);
            mstyle = ByteParse(arr[20]);
            cstyle = ByteParse(arr[21]);
            resist = ByteParse(arr[22]);
            ageRange = IntParse(arr[23]);
            minAge = IntParse(arr[24]);
            blood = arr[25].Equals("1");
            breeder = IntParse(arr[26]);
            height = IntParse(arr[27]);
            skill = arr[28];
            trait = arr[29];
            List<string> ls = GetBody(arr[30]);
            SetBody(ls);
            desc_j = arr[31];
            desc_e = arr[32];
            
        }

        static public int IntParse(string x)
        {
            if (int.TryParse(x, out int result))
                return result;
            else
                return 0;
        }

        static public byte ByteParse(string x)
        {
            if (byte.TryParse(x, out byte result))
                return result;
            else
                return 0;
        }

        private string ReturnNonZero(int v)
        {
            if (v > 0)
                return v.ToString();
            else
                return "";
        }

        public void SetBody(List<string> ls)
        {
            foreach(string s in ls)
            {
                switch(s)
                {
                    case "頭":
                        head = true;
                        break;
                    case "首":
                        neck = true;
                        break;
                    case "体":
                        body = true;
                        break;
                    case "背":
                        back = true;
                        break;
                    case "手":
                        if (handr)
                            handl = true;
                        else
                            handr = true;
                        break;
                    case "指":
                        if (ringr)
                            ringl = true;
                        else
                            ringr = true;
                        break;
                    case "腕":
                        arm = true;
                        break;
                    case "腰":
                        waist = true;
                        break;
                    case "足":
                        leg = true;
                        break;
                    default:
                        Console.WriteLine("IDK lol");
                        break;
                }
            }
        }

        private List<string> GetBody(string s)
        {
            List<string> ls = new List<string>();
            for (int i=0;i<s.Length;i+=2)
                ls.Add(s[i].ToString());
            
            return ls;
        }

        public string PrintBody()
        {
            string s ="";
            bool[] b = { head, neck, back, body, handr, handl, ringr, ringl, arm, waist, leg };
            string[] parts = { "頭", "首", "体", "背", "手", "手", "指", "指", "腕", "腰", "足" };
            for (int i = 0; i < b.Length; i++)
                if (b[i])
                    s += parts[i] + "|";
            return s;
        }
        static public string PrintBody(bool?[] cbx)
        {
            string s = "";
            string[] parts = { "頭", "首", "体", "背", "手", "手", "指", "指", "腕", "腰", "足" };
            for (int i = 0; i < cbx.Length; i++)
                if (cbx[i]!=null && cbx[i]==true)
                    s += parts[i] + "|";
            return s;
        }

        private void SetTraits()
        {
            //To do later when I understand what the values mean for traits
        }
        public void AssignTraits()
        {
            //To do later when I understand what the values mean for traits
        }

        private void SetSkills()
        {
            //To do later when I understand what the values mean for skills
        }
        public void AssignSkills()
        {
            //To do later when I understand what the values mean for skills
        }

        public override string ToString()
        {
            object[] arr =
                {name, id, playable, sex, pic1, pic2,
                ReturnNonZero(dv), ReturnNonZero(pv), hp, mp,
                str, end, dex, per, ler, wil, mag, chr, spd,
                ReturnNonZero(mstyle), ReturnNonZero(cstyle), ReturnNonZero(resist),
                ageRange, minAge, blood, breeder, height, skill, trait,
                PrintBody(), desc_j, desc_e, ""};

            string print = jpname;
            for (int i = 0; i < 33; i++)
            {
                if (arr[i] is bool)
                {
                    if ((bool)arr[i])
                    {
                        print += ",1";
                    }
                    else
                    {
                        print += ",";
                    }
                }
                else if (arr[i] is int || arr[i] is byte)
                    print += "," + arr[i].ToString();
                else
                    print += "," + arr[i];
            }
            return print;
        }
    }
}
