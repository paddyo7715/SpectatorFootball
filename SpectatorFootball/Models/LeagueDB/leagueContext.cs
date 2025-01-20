namespace SpectatorFootball.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.SQLite;

    public partial class leagueContext: DbContext
    {

        public leagueContext(string connString)
    : base(new SQLiteConnection() { ConnectionString = connString }, true)
        {
        }

        public leagueContext()
            : base("name=leagueContext")
        {
        }

        public virtual DbSet<Award> Awards { get; set; }
        public virtual DbSet<Conference> Conferences { get; set; }
        public virtual DbSet<DBVersion> DBVersions { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Draft> Drafts { get; set; }
        public virtual DbSet<Franchise> Franchises { get; set; }
        public virtual DbSet<Free_Agency> Free_Agency { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Game_Player_Penalty_Stats> Game_Player_Penalty_Stats { get; set; }
        public virtual DbSet<Game_Player_Stats> Game_Player_Stats { get; set; }
        public virtual DbSet<Game_Scoring_Summary> Game_Scoring_Summary { get; set; }
        public virtual DbSet<Hall_of_Fame> Hall_of_Fame { get; set; }
        public virtual DbSet<Injury> Injuries { get; set; }
        public virtual DbSet<Injury_Log> Injury_Log { get; set; }
        public virtual DbSet<League_Structure_by_Season> League_Structure_by_Season { get; set; }
        public virtual DbSet<Player_Awards> Player_Awards { get; set; }
        public virtual DbSet<Player_Ratings> Player_Ratings { get; set; }
        public virtual DbSet<Player_Retiring_Log> Player_Retiring_Log { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Players_By_Team> Players_By_Team { get; set; }
        public virtual DbSet<Playoff_Teams_by_Season> Playoff_Teams_by_Season { get; set; }
        public virtual DbSet<Season> Seasons { get; set; }
        public virtual DbSet<Teams_by_Season> Teams_by_Season { get; set; }
        public virtual DbSet<Training_Camp_by_Season> Training_Camp_by_Season { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Award>()
                .HasMany(e => e.Player_Awards)
                .WithRequired(e => e.Award)
                .HasForeignKey(e => e.Award_Code)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Franchise>()
                .HasMany(e => e.Drafts)
                .WithRequired(e => e.Franchise)
                .HasForeignKey(e => e.Franchise_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Franchise>()
                .HasMany(e => e.Free_Agency)
                .WithRequired(e => e.Franchise)
                .HasForeignKey(e => e.Franchise_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Franchise>()
                .HasMany(e => e.Game_Player_Penalty_Stats)
                .WithRequired(e => e.Franchise)
                .HasForeignKey(e => e.Franchise_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Franchise>()
                .HasMany(e => e.Game_Player_Stats)
                .WithRequired(e => e.Franchise)
                .HasForeignKey(e => e.Franchise_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Franchise>()
                .HasMany(e => e.Injuries)
                .WithRequired(e => e.Franchise)
                .HasForeignKey(e => e.Franchise_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Franchise>()
                .HasMany(e => e.Players_By_Team)
                .WithRequired(e => e.Franchise)
                .HasForeignKey(e => e.Franchise_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Franchise>()
                .HasMany(e => e.Playoff_Teams_by_Season)
                .WithRequired(e => e.Franchise)
                .HasForeignKey(e => e.Franchise_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Franchise>()
                .HasMany(e => e.Teams_by_Season)
                .WithRequired(e => e.Franchise)
                .HasForeignKey(e => e.Franchise_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Franchise>()
                .HasMany(e => e.Training_Camp_by_Season)
                .WithRequired(e => e.Franchise)
                .HasForeignKey(e => e.Franchise_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Game_Player_Penalty_Stats)
                .WithRequired(e => e.Game)
                .HasForeignKey(e => e.Game_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Game_Player_Stats)
                .WithRequired(e => e.Game)
                .HasForeignKey(e => e.Game_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Game_Scoring_Summary)
                .WithRequired(e => e.Game)
                .HasForeignKey(e => e.Game_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Drafts)
                .WithOptional(e => e.Player)
                .HasForeignKey(e => e.Player_ID);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Free_Agency)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Game_Player_Penalty_Stats)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Game_Player_Stats)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Hall_of_Fame)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Injuries)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Injury_Log)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Player_Awards)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Player_Ratings)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Player_Retiring_Log)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Players_By_Team)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Training_Camp_by_Season)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.Player_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Conferences)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Divisions)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Drafts)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Free_Agency)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Games)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Hall_of_Fame)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Injuries)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Injury_Log)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.League_Structure_by_Season)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Player_Awards)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Player_Ratings)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Player_Retiring_Log)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Players_By_Team)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Playoff_Teams_by_Season)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Teams_by_Season)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Training_Camp_by_Season)
                .WithRequired(e => e.Season)
                .HasForeignKey(e => e.Season_ID)
                .WillCascadeOnDelete(false);
        }
    }
}
