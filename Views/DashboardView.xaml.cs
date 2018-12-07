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
using DBApp1.Models;
using DBApp1.ViewModels;

namespace DBApp1.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardViewModel DashboardViewModel { get; set; }
        public DashboardView()
        {
            InitializeComponent();
            DashboardViewModel = new DashboardViewModel();
           // try
            //{
                this.DataContext = DashboardViewModel;
            //}
            //catch (Exception exception)
            //{
                
            //}
            
        }

        private void Groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //MessageBox.Show(Groups.SelectedValue.ToString());
            string value = (Groups?.SelectedValue != null) ? Groups.SelectedValue.ToString() : "0";
            int selectedValue = int.Parse(value);
            DashboardViewModel.ChangeCurrentGroup(selectedValue);
        }

        private void ButtonCreateGroup_OnClick(object sender, RoutedEventArgs e)
        {
            var name = TextBox_NewGroupName.Text;
            var desc = TextBox_NewGroupDesc.Text;
            DashboardViewModel.CreateNewGroup(name, desc);
            TextBox_NewGroupDesc.Text = TextBox_NewGroupName.Text = "";
        }

        private void ButtonUpdateGroup_OnClick(object sender, RoutedEventArgs e)
        {
            var name = TextBox_NewGroupName.Text;
            var desc = TextBox_NewGroupDesc.Text;
            DashboardViewModel.UpdateCurrentGroup(name, desc);
        }

        private void Staff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedValue = int.Parse(Staff.SelectedValue.ToString());
            DashboardViewModel.ChangeCurrentStaff(selectedValue);
        }

        private void ButtonCreateStaff_OnClick(object sender, RoutedEventArgs e)
        {
            var lastName = TextBox_NewStaffLastName.Text;
            var firstName = TextBox_NewStaffFirstName.Text;
            var otherNames = TextBox_NewStaffOtherNames.Text;
            var gender = TextBox_NewStaffGender.SelectedIndex == 0 ? "Male": "Female";
            var dob = TextBox_NewStaffDob.Text;
            var groupIndexs = (from object o in ListBox_NewStaffGroup.SelectedItems select ListBox_NewStaffGroup.Items.IndexOf(o)).ToList();
            //MessageBox.Show($"lastName: {lastName} firstName: {firstName} otherNames: {otherNames}, gender: {gender}, dob : {dob} {groupIDs.ToString()}");
           // return;
            var staff = new Staff()
            {
                LastName = lastName,
                FirstName = firstName,
                OtherNames = otherNames,
                Gender = gender,
                Dob = DateTime.Parse(dob)
            };


            foreach (var index in groupIndexs)
            {
                Group group = DashboardViewModel.AllGroups[index];
                if (group != null)
                {
                    staff.StaffGroups.Add(new StaffGroup() {
                        GroupId = group.Id
                    });
                }
            }

            DashboardViewModel.CreateNewStaff(staff);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse(Groups.SelectedValue.ToString());
            DashboardViewModel.DeleteGroup(selectedValue);
        }
    }
}
