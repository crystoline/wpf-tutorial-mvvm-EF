using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DBApp1.Models;

namespace DBApp1.ViewModels
{
    public class DashboardViewModel: NotifyPropertyChanged
    {
        private CrystoEntities dbContext;

        public Group CurrentGroup { get; set; }
        public Staff CurrentStaff { get; set; }

        private SqlConnection sqlConnection;

        public List<Staff> AllStaff { get; set; }
        public List<Group> AllGroups { get; set; }
        public DashboardViewModel()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBApp1.Properties.Settings.crystoConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);

            AllStaff = new List<Staff>();
            AllGroups = new List<Group>();
            CurrentGroup = new Group();
            CurrentStaff = new Staff();

            dbContext = new CrystoEntities();

            reloadData();

        }

        private void reloadData()
        {
            try
            {
                AllStaff = dbContext.Staffs.Include(s => s.StaffGroups).ToList();
                AllGroups = dbContext.Groups.Include(g => g.StaffGroups).ToList();
            }
            catch (EntityCommandExecutionException e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            if (AllStaff.Count() == 0)
            {
                AllStaff.Add(new Staff()
                {
                    LastName = "Adekoya",
                    FirstName = "Adekunle",
                    OtherNames = "Adeniyi",
                    Dob = new DateTime(1982, 8, 9),
                    Gender = "Male",
                    CreatedAt = DateTime.Today,
                    UpdatedAt = DateTime.Now,
                   // StaffGroups = new List<StaffGroup>() { new StaffGroup(){GroupId = 3, StaffId  =1} }
                });
                AllStaff.Add(new Staff()
                {
                    LastName = "Goriowo",
                    FirstName = "Omolade",
                    OtherNames = "H",
                    Dob = new DateTime(1992, 1, 5),
                    Gender = "Female",
                    CreatedAt = DateTime.Today,
                    UpdatedAt = DateTime.Now
                });

            }

        
            OnPropertyChanged("AllGroups");
            OnPropertyChanged("AllStaff");
        }

     
        internal void UpdateCurrentGroup(string name, string desc)
        {
            CurrentGroup.name = name;
            CurrentGroup.Description = desc;
            CurrentGroup.HasChnaged();
            dbContext.SaveChanges();
            MessageBox.Show("Group" + CurrentGroup.name + " Was Created");
            //var x = AllGroups.Find( g =>g.Id.Equals(CurrentGroup.Id));
            //if (x != null)
            //{
            //    x = CurrentGroup;
            //}
            OnPropertyChanged("AllGroups");
        }

        internal  void CreateNewGroup(string name, string Description)
        {
           
           dbContext.Groups.Add(new Group(){ name =name, Description = Description });
            var r = dbContext.SaveChanges();

            if (r > 0)
            {
                MessageBox.Show("Group " + name + " Was Created");

            }
            reloadData();

        }

        internal void CreateNewStaff(Staff staff)
        {
            dbContext.Staffs.Add(staff);
            dbContext.SaveChanges();
             reloadData();
        }

        internal void ChangeCurrentGroup(int selectedValue)
        {
            //MessageBox.Show(selectedIndex.ToString());
            CurrentGroup = AllGroups.Find(g => g.Id == selectedValue);
            OnPropertyChanged("CurrentGroup");
        }
        internal void ChangeCurrentStaff(int selectedValue)
        {
            CurrentStaff = AllStaff.Find(g => g.Id == selectedValue);
            AllGroups.ForEach(x => x.Check(CurrentStaff.GroupIds));
            OnPropertyChanged("CurrentStaff");
            OnPropertyChanged("CurrentGroup");
            
        }

        internal void DeleteGroup(int id)
        {
            var selcted_staff = from staff in AllStaff
                                join grp in AllGroups on staff.Id equals grp.Id
                                where grp.Id == 1
                                orderby staff.FirstName
                                select staff;
            try
            {
                string query = "delete from [groups] where Id = @Id";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteScalar();
                MessageBox.Show("Group Was Deleted");
            }
            catch(Exception e)
            {
                var trace = e.Message;
                Console.Out.Write(trace);
                MessageBox.Show(trace);
            }
            finally
            {
                sqlConnection.Close();
                reloadData();
            }
        }
    }
}
