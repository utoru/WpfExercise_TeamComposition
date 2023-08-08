using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlTypes;
using System.Xml;
using static System.Net.WebRequestMethods;

namespace TeamComposition
{
    public partial class MainWindow : Window
    {
        Dictionary<string, List<string>> teamData; //Using dictionary to save each member and their hate list
        public MainWindow()
        {
            InitializeComponent();
            string path = AppDomain.CurrentDomain.BaseDirectory + "Config.xml"; //Path to the file
            teamData = ExtractDataFromXml(path);
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            if (teamData != null)
            {
                foreach (string name in teamData.Keys) 
                {
                    cb1.Items.Add(name);
                    cb2.Items.Add(name);
                    cb3.Items.Add(name);
                    cb4.Items.Add(name);
                    cb5.Items.Add(name);
                }
            }
        }

        private Dictionary<string, List<string>> ExtractDataFromXml(string xmlFilePath) //Reading the XML file, storing the data in dictionary and returning it
        {
            try
            {
                Dictionary<string, List<string>> teamData = new Dictionary<string, List<string>>();
                DataSet dataSet = new DataSet();

                dataSet.ReadXml(xmlFilePath);

                if (dataSet.Tables.Count > 0)
                {
                    DataTable table = dataSet.Tables[0]; //Choosing the first table
                    foreach (DataRow row in table.Rows) //Cycling through each row, every row contains one member
                    {
                        string name = row["name"].ToString(); //Getting the "name" column and converting it into string
                        List<string> hates = new List<string>(); //List for storing hated members

                        foreach (DataRow hateRow in row.GetChildRows("Member_Hate")) //Cycling through the nested rows of each member, which contains data about hate
                        {
                            string hatedName = hateRow["name"].ToString();
                            hates.Add(hatedName);
                        }
                        teamData.Add(name, hates); //Adding one member and their corresponding hate list into the dictionary
                    }
                }
                return teamData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading XML file: {ex.Message}");
                return null;
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox selectedComboBox = (ComboBox)sender; //This object references the comboBox that triggered this event
            string selectedName = (string)selectedComboBox.SelectedItem; //This string contains the name of the selected item of the selected comboBox
            selectedComboBox.IsEnabled = false;

            bool success = false; //Helper variable for determining fully stacked team
            bool fail = false; //Helper boolean to display the fail message in case it's impossible to finish the team

            foreach (string hatedMember in teamData.Keys) //Cycle through all members
            {
                if (teamData[hatedMember].Contains(selectedName)) //Triggers if the selected member hates somebody
                {
                    int selectedMembers = 0;
                    foreach (ComboBox comboBox in new ComboBox[] { cb1, cb2, cb3, cb4, cb5 }) //Checking for every comboBox
                    {
                        //Removing from members that cannot be selected due to being hated by the selected member + removing the selected member to avoid duplicate members
                        if (comboBox != selectedComboBox && comboBox.SelectedItem == null)
                        {
                            comboBox.Items.Remove(hatedMember);
                            comboBox.Items.Remove(selectedName);
                        }
                        if (comboBox.SelectedItem != null) //If selectedMembers reach 5, it means all comboBoxes have a selected member -> Success
                        {
                            selectedMembers++;
                        }
                        if (!comboBox.HasItems) //If any comboBox runs out of options, it's considered a fail
                        {
                            fail = true;
                        }
                    }
                    if (selectedMembers > 4) success = true;
                }
            }
            if (success == true) MessageBox.Show("Success!");
            else if (fail == true) MessageBox.Show("Fail!");
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            foreach (ComboBox comboBox in new ComboBox[] { cb1, cb2, cb3, cb4, cb5 })
            {
                comboBox.Items.Clear();
                comboBox.IsEnabled = true;
            }
            FillComboBoxes();
        }
    }
}
