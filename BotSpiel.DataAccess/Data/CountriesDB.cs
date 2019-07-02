using System;
using System.Data;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;
using System.Web;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Data
{

    public class CountriesDB : DbContext
    {

        public CountriesDB(DbContextOptions<CountriesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Countries> Countries { get; set; }
		public DbSet<CountriesPost> CountriesPost { get; set; }
		public DbSet<PlanetSubRegions> PlanetSubRegions { get; set; }
		public DbSet<PlanetSubRegionsPost> PlanetSubRegionsPost { get; set; }
		public DbSet<PlanetRegions> PlanetRegions { get; set; }
		public DbSet<PlanetRegionsPost> PlanetRegionsPost { get; set; }
		public DbSet<Planets> Planets { get; set; }
		public DbSet<PlanetsPost> PlanetsPost { get; set; }
		public DbSet<PlanetarySystems> PlanetarySystems { get; set; }
		public DbSet<PlanetarySystemsPost> PlanetarySystemsPost { get; set; }
		public DbSet<Galaxies> Galaxies { get; set; }
		public DbSet<GalaxiesPost> GalaxiesPost { get; set; }
		public DbSet<Universes> Universes { get; set; }
		public DbSet<UniversesPost> UniversesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Countries>()
                .ToTable("md_vw_Countries")
                .HasKey(c => new { c.ixCountry });
            modelBuilder.Entity<CountriesPost>()
                .ToTable("md_vw_CountriesPost")
                .HasKey(c => new { c.ixCountry });
            modelBuilder.Entity<PlanetSubRegions>()
                .ToTable("md_vw_PlanetSubRegions")
                .HasKey(c => new { c.ixPlanetSubRegion });
            modelBuilder.Entity<PlanetSubRegionsPost>()
                .ToTable("md_vw_PlanetSubRegionsPost")
                .HasKey(c => new { c.ixPlanetSubRegion });
            modelBuilder.Entity<PlanetRegions>()
                .ToTable("md_vw_PlanetRegions")
                .HasKey(c => new { c.ixPlanetRegion });
            modelBuilder.Entity<PlanetRegionsPost>()
                .ToTable("md_vw_PlanetRegionsPost")
                .HasKey(c => new { c.ixPlanetRegion });
            modelBuilder.Entity<Planets>()
                .ToTable("md_vw_Planets")
                .HasKey(c => new { c.ixPlanet });
            modelBuilder.Entity<PlanetsPost>()
                .ToTable("md_vw_PlanetsPost")
                .HasKey(c => new { c.ixPlanet });
            modelBuilder.Entity<PlanetarySystems>()
                .ToTable("md_vw_PlanetarySystems")
                .HasKey(c => new { c.ixPlanetarySystem });
            modelBuilder.Entity<PlanetarySystemsPost>()
                .ToTable("md_vw_PlanetarySystemsPost")
                .HasKey(c => new { c.ixPlanetarySystem });
            modelBuilder.Entity<Galaxies>()
                .ToTable("md_vw_Galaxies")
                .HasKey(c => new { c.ixGalaxy });
            modelBuilder.Entity<GalaxiesPost>()
                .ToTable("md_vw_GalaxiesPost")
                .HasKey(c => new { c.ixGalaxy });
            modelBuilder.Entity<Universes>()
                .ToTable("md_vw_Universes")
                .HasKey(c => new { c.ixUniverse });
            modelBuilder.Entity<UniversesPost>()
                .ToTable("md_vw_UniversesPost")
                .HasKey(c => new { c.ixUniverse });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is CountriesPost)).ToList())
            {
                var md_vw_countriespost = e.Entity as CountriesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sCountry = cmd.CreateParameter();
                            sCountry.ParameterName = "p0";
                            sCountry.Value = md_vw_countriespost.sCountry;
                            var ixPlanetSubRegion = cmd.CreateParameter();
                            ixPlanetSubRegion.ParameterName = "p1";
                            ixPlanetSubRegion.Value = md_vw_countriespost.ixPlanetSubRegion;
                            var sCountryCode = cmd.CreateParameter();
                            sCountryCode.ParameterName = "p2";
                            sCountryCode.Value = md_vw_countriespost.sCountryCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p3";
                            UserName.Value = md_vw_countriespost.UserName;

                            var ixCountry = cmd.CreateParameter();
                            ixCountry.ParameterName = "p4";
                            ixCountry.DbType = DbType.Int64;
                            ixCountry.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateCountries ");
                            sql.Append("@sCountry = @p0, ");
                            sql.Append("@ixPlanetSubRegion = @p1, ");
                            sql.Append("@sCountryCode = @p2, ");
                            if (md_vw_countriespost.UserName != null) { sql.Append("@UserName = @p3, "); }  
                            sql.Append("@ixCountry = @p4 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sCountry);
                            cmd.Parameters.Add(ixPlanetSubRegion);
                            cmd.Parameters.Add(sCountryCode);
                            if (md_vw_countriespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixCountry); 
                            cmd.ExecuteNonQuery();
                            md_vw_countriespost.ixCountry = (Int64)ixCountry.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixCountry"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeCountries @ixCountry = @p0, @sCountry = @p1, @ixPlanetSubRegion = @p2, @sCountryCode = @p3, @UserName = @p4", md_vw_countriespost.ixCountry, md_vw_countriespost.sCountry, md_vw_countriespost.ixPlanetSubRegion, md_vw_countriespost.sCountryCode, md_vw_countriespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteCountries @ixCountry = @p0, @sCountry = @p1, @ixPlanetSubRegion = @p2, @sCountryCode = @p3, @UserName = @p4", md_vw_countriespost.ixCountry, md_vw_countriespost.sCountry, md_vw_countriespost.ixPlanetSubRegion, md_vw_countriespost.sCountryCode, md_vw_countriespost.UserName);
                        e.State = EntityState.Detached;                           
						break;
                }

                ++changes;
            }



            return changes; 
        }

        public void Rollback()
        {
            ChangeTracker.Entries().ToList().ForEach(x =>
            {
                x.State = EntityState.Detached;
            });
        }

    }
}
  

