using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace DBApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       private SqlConnection sqlConnection;
        public MainWindow()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["DBApp1.Properties.Settings.crystoConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            String query = "SELECT * FROM groups";

            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection))
            {
                DataTable group = new DataTable();
                sqlDataAdapter.Fill(group);

                var a = group.Rows;
                
            }




        }
    }
}
