namespace SpectatorFootball.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Windows.Markup;
    using System.Data.SQLite;

    public partial class settingsContext: DbContext
    {
        public settingsContext()
            : base("name=settingsContext")
        {
            
        }

        public settingsContext(string connString)
    : base(new SQLiteConnection() { ConnectionString = connString }, true)

        {
        }


        public virtual DbSet<HomeTown> HomeTowns { get; set; }
        public virtual DbSet<Potential_First_Names> Potential_First_Names { get; set; }
        public virtual DbSet<Potential_Last_Names> Potential_Last_Names { get; set; }
        public virtual DbSet<Stock_Teams> Stock_Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
