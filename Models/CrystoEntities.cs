using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApp1.Models
{
    class CrystoEntities : DbContext
    {
       
        public CrystoEntities() : base(GetConnectionString())   
        {
           
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBApp1.Properties.Settings.crystoConnectionString"].ConnectionString;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<StaffGroup> StaffGroups { get; set; }
    }
}
