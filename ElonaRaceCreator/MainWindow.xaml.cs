using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace ElonaRaceCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLiteDB DB = new SQLiteDB();
        List<Skill> AllSkills = new List<Skill>();
        List<Race> RaceList = new List<Race>();

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                if (!File.Exists(@".\o_race.csv"))
                {
                    throw new Exception("File \"o_race.csv\" not found. Make sure you are running the program in the elona\\data folder");
                }
                using (TextFieldParser parser = new TextFieldParser(@"o_race.csv", Encoding.GetEncoding("shift_jis")))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    for (int i = 0; i < 2; i++)
                        parser.ReadLine();//skips first two lines
                    while (!parser.EndOfData)
                    {
                        string[] lines = parser.ReadFields();
                        Race r = new Race(lines);
                        RaceList.Add(r);
                    }
                    parser.Close();
                }
                foreach (Race r in RaceList)
                    RaceLB.Items.Add(r.name);
                for (int i = 0; i < 300; i++)
                {
                    Pic1CB.Items.Add(i);
                    Pic2CB.Items.Add(i);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Place program into your \"data\" directory in you Elona folder. \n \n Full Error: " + e.Message,
                    "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                Close();
            }
        }

        private void RaceLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckBox[] cbx = { HeadCB , NeckCB , BackCB , BodyCB ,
                HandRCB , HandLCB , RingRCB ,
                RingLCB , ArmCB , WaistCB , LegCB  };
            TextBox[] stats = { HPTB, MPTB, StrTB, EndTB, DexTB,
                PerTB, LerTB, WilTB, MagTB, ChrTB, SpdTB };
            Race r = RaceList[RaceLB.SelectedIndex];

            RaceName.Text = r.name;
            RaceID.Text = r.id.ToString();

            List<int> RaceStats = r.GetStats();
            for (int i = 0; i < RaceStats.Count(); i++)
                stats[i].Text = RaceStats[i].ToString();

            List<bool> BodyBool = r.GetBody();
            for (int i = 0; i < BodyBool.Count(); i++)
                cbx[i].IsChecked = BodyBool[i];

            PlayableCB.IsChecked = r.playable;

            RaceDesc.Text = r.desc_e;

            SexSldr.Value = r.sex;
            SliderValue.Text = r.sex.ToString();

            Pic1CB.SelectedIndex = r.pic1;
            Pic2CB.SelectedIndex = r.pic2;
            //To do: finish printing out stats, eventually print out skills

        }

        private void AddNewRace_Click(object sender, RoutedEventArgs e)
        {
            String name = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of your race.", "Add Race", "");
            if (!String.IsNullOrEmpty(name) && !name.Contains(","))
            {
                int id = RaceList.Max(r => r.id) + 1;
                RaceList.Add(new Race(name, id));
                RaceLB.Items.Add(name);
                RaceLB.SelectedIndex = id - 2;
                RaceLB_SelectionChanged(RaceLB, null);
                //Todo: Enter race into o_race.csv
            }
        }

        private void UpdateRace_Click(object sender, RoutedEventArgs e)
        {
            Race r = RaceList[RaceLB.SelectedIndex];
            //For setting everything, body is set seperate.
            Object[] RaceInfo = { r.jpname, RaceName.Text, RaceID.Text, PlayableCB.IsChecked,
            SexSldr.Value, Pic1CB.SelectedValue ?? 0, Pic2CB.SelectedValue ?? 0, 0, 0,
            HPTB.Text, MPTB.Text, StrTB.Text, EndTB.Text, DexTB.Text, PerTB.Text,
            LerTB.Text, WilTB.Text, MagTB.Text, ChrTB.Text, SpdTB.Text,
            r.mstyle, r.cstyle, r.resist, r.ageRange, r.minAge, r.blood, r.breeder,
            r.height, r.skill, r.trait, "", r.desc_j, RaceDesc.Text };
            bool?[] cbx = { HeadCB.IsChecked , NeckCB.IsChecked , BackCB.IsChecked , BodyCB.IsChecked ,
                HandRCB.IsChecked , HandLCB.IsChecked , RingRCB.IsChecked ,
                RingLCB.IsChecked , ArmCB.IsChecked , WaistCB.IsChecked , LegCB.IsChecked  };

            string[] arr = new string[33];
            for (int i = 0; i < arr.Length; i++)
                if (RaceInfo[i] is bool)
                    arr[i] = (bool)RaceInfo[i] ? "1" : "";
                else
                    arr[i] = RaceInfo[i].ToString();

            r.AddInfo(arr);
            r.SetBody(cbx);

            MessageBox.Show("Update Completed.");
            //Todo: Update .CSV file, create fields for latter stuff (mstyle to traits)
        }

        private void SexSldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderValue.Text = SexSldr.Value.ToString();
        }
    }
}
