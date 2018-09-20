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
        List<Races> RaceList = new List<Races>();

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
                        Races r = new Races(lines);
                        RaceList.Add(r);
                    }
                    parser.Close();
                }
                foreach (Races r in RaceList)
                    RaceLB.Items.Add(r.name);
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
            bool?[] cbx = { HeadCB.IsChecked, NeckCB.IsChecked, BackCB.IsChecked, BodyCB.IsChecked,
                HandRCB.IsChecked, HandLCB.IsChecked, RingRCB.IsChecked,
                RingLCB.IsChecked, ArmCB.IsChecked, WaistCB.IsChecked, LegCB.IsChecked };
            Object[] RaceInfo = { "", RaceName.Text, RaceID.Text, PlayableCB, SexSldr.Value, "1", "2",
                HPTB.Text, MPTB.Text, StrTB.Text, EndTB.Text, DexTB.Text, PerTB.Text, LerTB.Text, WilTB.Text, MagTB.Text, ChrTB.Text, SpdTB.Text,
                Races.PrintBody(cbx) };
            Races r = RaceList[RaceLB.SelectedIndex];
            RaceName.Text = r.name;
            RaceID.Text = r.id.ToString();
            HPTB.Text = r.hp.ToString();
            MPTB.Text = r.mp.ToString();
            StrTB.Text = r.str.ToString();
            EndTB.Text = r.end.ToString();
            DexTB.Text = r.dex.ToString();
            PerTB.Text = r.per.ToString();
            LerTB.Text = r.ler.ToString();
            WilTB.Text = r.wil.ToString();
            MagTB.Text = r.mag.ToString();
            ChrTB.Text = r.chr.ToString();
            HeadCB.IsChecked = r.head;
            NeckCB.IsChecked = r.neck;
            BackCB.IsChecked = r.back;
            BodyCB.IsChecked = r.body;
            HandRCB.IsChecked = r.handr;
            HandLCB.IsChecked = r.handl;
            RingRCB.IsChecked = r.ringr;
            RingLCB.IsChecked = r.ringl;
            ArmCB.IsChecked = r.arm;
            WaistCB.IsChecked = r.waist;
            LegCB.IsChecked = r.leg;
            //To do: finish printing out stats, eventually print out skills

        }

        private void AddNewRace_Click(object sender, RoutedEventArgs e)
        {
            String name = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of your race.", "Add Race", "");
            if (!String.IsNullOrEmpty(name) && !name.Contains(","))
            {
                int id = (from kvp in RaceList select kvp.id).Max() + 1;
                RaceList.Add(new Races(name, id));
                RaceLB.Items.Add(name);
                //Todo: Enter race into o_race.csv
            }
        }

        private void UpdateRace_Click(object sender, RoutedEventArgs e)
        {
            bool?[] cbx = { HeadCB.IsChecked, NeckCB.IsChecked, BackCB.IsChecked, BodyCB.IsChecked,
                HandRCB.IsChecked, HandLCB.IsChecked, RingRCB.IsChecked,
                RingLCB.IsChecked, ArmCB.IsChecked, WaistCB.IsChecked, LegCB.IsChecked };
            Object[] RaceInfo = { "", RaceName.Text, RaceID.Text, PlayableCB, SexSldr.Value, "1", "2",
                HPTB.Text, MPTB.Text, StrTB.Text, EndTB.Text, DexTB.Text, PerTB.Text, LerTB.Text, WilTB.Text, MagTB.Text, ChrTB.Text, SpdTB.Text,
                Races.PrintBody(cbx) };
            TextBox[] TextBoxes = { };
            string[] arr = new string[34];
            for (int i = 0; i < arr.Length; i++)
            {

            }
        }

        private void SexSldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderValue.Text = SexSldr.Value.ToString();
        }
    }
}
