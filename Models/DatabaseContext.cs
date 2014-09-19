using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HIMS.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext() : base("ConnectionString") { }        
        public DbSet<BusinessUnit> BusinessUnits { get; set; }
        public DbSet<SiteUser> Users { get; set; }
        public DbSet<UserBusinessUnit> UserBusinessUnits { get; set; }
        
    }
}