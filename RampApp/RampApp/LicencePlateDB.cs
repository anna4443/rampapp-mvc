using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace RampApp
{
    public partial class LicencePlateDB : DbContext
    {
        public LicencePlateDB()
            : base("name=LicencePlateDB")
        {
        }

        public virtual DbSet<LicencePlate> LicencePlate { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
