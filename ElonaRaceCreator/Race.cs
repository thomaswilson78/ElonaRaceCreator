using System;
using System.Collections.Generic;
using System.Linq;

namespace ElonaRaceCreator
{
    class Race
    {
        #region variables
        public string jpname, name, skill, trait, desc_j, desc_e;
        public int id, spd, pic1, pic2, dv, pv, hp, mp, minAge, ageRange, breeder, height;
        public bool playable, blood, head, neck, back, body, handr, handl, ringr, ringl, arm, waist, leg;
        public byte str, end, dex, per, ler, wil, mag, chr, sex, mstyle, cstyle, resist;

        List<Skill> Skills = new List<Skill>();
        #endregion

        public Race(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public Race(string[] arr)
        {
            AddInfo(arr);
        }

        /// <summary>
        /// Sets all values in Object
        /// </summary>
        /// <param name="arr"></param>
        public void AddInfo(string[] arr)
        {
            jpname = arr[0];
            name = arr[1];
            id = Helper.IntParse(arr[2]);
            playable = arr[3].Equals("1");
            sex = Helper.ByteParse(arr[4]);
            pic1 = Helper.IntParse(arr[5]);
            pic2 = Helper.IntParse(arr[6]);
            dv = Helper.IntParse(arr[7]);
            pv = Helper.IntParse(arr[8]);
            hp = Helper.IntParse(arr[9]);
            mp = Helper.IntParse(arr[10]);
            str = Helper.ByteParse(arr[11]);
            end = Helper.ByteParse(arr[12]);
            dex = Helper.ByteParse(arr[13]);
            per = Helper.ByteParse(arr[14]);
            ler = Helper.ByteParse(arr[15]);
            wil = Helper.ByteParse(arr[16]);
            mag = Helper.ByteParse(arr[17]);
            chr = Helper.ByteParse(arr[18]);
            spd = Helper.IntParse(arr[19]);
            mstyle = Helper.ByteParse(arr[20]);
            cstyle = Helper.ByteParse(arr[21]);
            resist = Helper.ByteParse(arr[22]);
            ageRange = Helper.IntParse(arr[23]);
            minAge = Helper.IntParse(arr[24]);
            blood = arr[25].Equals("1");
            breeder = Helper.IntParse(arr[26]);
            height = Helper.IntParse(arr[27]);
            skill = arr[28];
            trait = arr[29];
            List<string> ls = GetBody(arr[30]);
            SetBody(ls);
            desc_j = arr[31];
            desc_e = arr[32];
        }

        
        /// <summary>
        /// Sets body based on characters from csv file.
        /// </summary>
        /// <param name="ls"></param>
        private void SetBody(List<string> ls)
        {
            foreach (string s in ls)
            {
                switch (s)
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

        /// <summary>
        /// For updating object's body information.
        /// </summary>
        /// <param name="list"></param>
        public void SetBody(bool?[] list)
        {
            head = list[0] ?? false;
            neck = list[1] ?? false;
            back = list[2] ?? false;
            body = list[3] ?? false;
            handr = list[4] ?? false;
            handl = list[5] ?? false;
            ringr = list[6] ?? false;
            ringl = list[7] ?? false;
            arm = list[8] ?? false;
            waist = list[9] ?? false;
            leg = list[10] ?? false;
        }

        /// <summary>
        /// Reads through string of Japanese Kanji for body parts and converts them into a list for parsing through.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private List<string> GetBody(string s)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < s.Length; i += 2)
                ls.Add(s[i].ToString());

            return ls;
        }

        /// <summary>
        /// Returns body info as bool list.
        /// </summary>
        /// <returns></returns>
        public List<bool> GetBody()
        {
            return new List<bool>() { head, neck, back, body, handr, handl, ringr, ringl, arm, waist, leg };
        }

        /// <summary>
        /// Returns stats as integer list.
        /// </summary>
        /// <returns></returns>
        public List<int> GetStats()
        {
            return new List<int>() { hp, mp, str, end, dex, per, ler, wil, mag, chr, spd };
        }

        /// <summary>
        /// For ToString(), turns bool values into Kanji for writing back into CSV file
        /// </summary>
        /// <returns></returns>
        private string PrintBody()
        {
            string s = "";
            bool[] b = { head, neck, back, body, handr, handl, ringr, ringl, arm, waist, leg };
            string[] parts = { "頭", "首", "体", "背", "手", "手", "指", "指", "腕", "腰", "足" };
            for (int i = 0; i < b.Length; i++)
                if (b[i])
                    s += parts[i] + "|";
            return s;
        }

        //static public string PrintBody(bool?[] cbx)
        //{
        //    string s = "";
        //    string[] parts = { "頭", "首", "体", "背", "手", "手", "指", "指", "腕", "腰", "足" };
        //    for (int i = 0; i < cbx.Length; i++)
        //        if (cbx[i] != null && cbx[i] == true)
        //            s += parts[i] + "|";
        //    return s;
        //}

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
                Helper.ReturnNonZero(dv), Helper.ReturnNonZero(pv), hp, mp,
                str, end, dex, per, ler, wil, mag, chr, spd,
                Helper.ReturnNonZero(mstyle), Helper.ReturnNonZero(cstyle), Helper.ReturnNonZero(resist),
                ageRange, minAge, blood, breeder, height, skill, trait,
                PrintBody(), desc_j, desc_e, ""};

            string print = jpname;
            for (int i = 0; i < 33; i++)
            {
                if (arr[i] is bool)
                    print += (bool)arr[i] == true ? ",1" : ",";
                else if (arr[i] is int || arr[i] is byte)
                    print += "," + arr[i].ToString();
                else
                    print += "," + arr[i];
            }
            return print;
        }
    }
}
