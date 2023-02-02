using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Assignment_Form.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MyRestaurantdatabseContext : DbContext
    {
        public MyRestaurantdatabseContext() : base("ForConnectingstrings")
        {

        }
        public DbSet<User> User { get; set; }
        //The method is using the DbModelBuilder class to map the "User" entity to stored procedures.
        //The base.OnModelCreating(modelBuilder) call at the end of the method is calling the base implementation
        //of this method in case it is overridden in a derived class.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().MapToStoredProcedures();
            base.OnModelCreating(modelBuilder);
        }
    }
}