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

    public class CountryLocationsDB : DbContext
    {

        public CountryLocationsDB(DbContextOptions<CountryLocationsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<CountryLocations> CountryLocations { get; set; }
		public DbSet<CountryLocationsPost> CountryLocationsPost { get; set; }
		public DbSet<CountrySubDivisions> CountrySubDivisions { get; set; }
		public DbSet<CountrySubDivisionsPost> CountrySubDivisionsPost { get; set; }
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
            modelBuilder.Entity<CountryLocations>()
                .ToTable("md_vw_CountryLocations")
                .HasKey(c => new { c.ixCountryLocation });
            modelBuilder.Entity<CountryLocationsPost>()
                .ToTable("md_vw_CountryLocationsPost")
                .HasKey(c => new { c.ixCountryLocation });
            modelBuilder.Entity<CountrySubDivisions>()
                .ToTable("md_vw_CountrySubDivisions")
                .HasKey(c => new { c.ixCountrySubDivision });
            modelBuilder.Entity<CountrySubDivisionsPost>()
                .ToTable("md_vw_CountrySubDivisionsPost")
                .HasKey(c => new { c.ixCountrySubDivision });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is CountryLocationsPost)).ToList())
            {
                var md_vw_countrylocationspost = e.Entity as CountryLocationsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sCountryLocation = cmd.CreateParameter();
                            sCountryLocation.ParameterName = "p0";
                            sCountryLocation.Value = md_vw_countrylocationspost.sCountryLocation;
                            var ixCountrySubDivision = cmd.CreateParameter();
                            ixCountrySubDivision.ParameterName = "p1";
                            ixCountrySubDivision.Value = md_vw_countrylocationspost.ixCountrySubDivision;
                            var sLocationCode = cmd.CreateParameter();
                            sLocationCode.ParameterName = "p2";
                            sLocationCode.Value = md_vw_countrylocationspost.sLocationCode;
                            var sNameWithoutDiacritics = cmd.CreateParameter();
                            sNameWithoutDiacritics.ParameterName = "p3";
                            sNameWithoutDiacritics.Value = md_vw_countrylocationspost.sNameWithoutDiacritics;
                            var sLatitude = cmd.CreateParameter();
                            sLatitude.ParameterName = "p4";
                            sLatitude.Value = md_vw_countrylocationspost.sLatitude;
                            var sLongitude = cmd.CreateParameter();
                            sLongitude.ParameterName = "p5";
                            sLongitude.Value = md_vw_countrylocationspost.sLongitude;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p6";
                            UserName.Value = md_vw_countrylocationspost.UserName;

                            var ixCountryLocation = cmd.CreateParameter();
                            ixCountryLocation.ParameterName = "p7";
                            ixCountryLocation.DbType = DbType.Int64;
                            ixCountryLocation.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateCountryLocations ");
                            sql.Append("@sCountryLocation = @p0, ");
                            sql.Append("@ixCountrySubDivision = @p1, ");
                            if (md_vw_countrylocationspost.sLocationCode != null) { sql.Append("@sLocationCode = @p2, "); }  
                            if (md_vw_countrylocationspost.sNameWithoutDiacritics != null) { sql.Append("@sNameWithoutDiacritics = @p3, "); }  
                            if (md_vw_countrylocationspost.sLatitude != null) { sql.Append("@sLatitude = @p4, "); }  
                            if (md_vw_countrylocationspost.sLongitude != null) { sql.Append("@sLongitude = @p5, "); }  
                            if (md_vw_countrylocationspost.UserName != null) { sql.Append("@UserName = @p6, "); }  
                            sql.Append("@ixCountryLocation = @p7 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sCountryLocation);
                            cmd.Parameters.Add(ixCountrySubDivision);
                            if (md_vw_countrylocationspost.sLocationCode != null) { cmd.Parameters.Add(sLocationCode); }
                            if (md_vw_countrylocationspost.sNameWithoutDiacritics != null) { cmd.Parameters.Add(sNameWithoutDiacritics); }
                            if (md_vw_countrylocationspost.sLatitude != null) { cmd.Parameters.Add(sLatitude); }
                            if (md_vw_countrylocationspost.sLongitude != null) { cmd.Parameters.Add(sLongitude); }
                            if (md_vw_countrylocationspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixCountryLocation); 
                            cmd.ExecuteNonQuery();
                            md_vw_countrylocationspost.ixCountryLocation = (Int64)ixCountryLocation.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixCountryLocation"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeCountryLocations @ixCountryLocation = @p0, @sCountryLocation = @p1, @ixCountrySubDivision = @p2, @sLocationCode = @p3, @sNameWithoutDiacritics = @p4, @sLatitude = @p5, @sLongitude = @p6, @UserName = @p7", md_vw_countrylocationspost.ixCountryLocation, md_vw_countrylocationspost.sCountryLocation, md_vw_countrylocationspost.ixCountrySubDivision, md_vw_countrylocationspost.sLocationCode, md_vw_countrylocationspost.sNameWithoutDiacritics, md_vw_countrylocationspost.sLatitude, md_vw_countrylocationspost.sLongitude, md_vw_countrylocationspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteCountryLocations @ixCountryLocation = @p0, @sCountryLocation = @p1, @ixCountrySubDivision = @p2, @sLocationCode = @p3, @sNameWithoutDiacritics = @p4, @sLatitude = @p5, @sLongitude = @p6, @UserName = @p7", md_vw_countrylocationspost.ixCountryLocation, md_vw_countrylocationspost.sCountryLocation, md_vw_countrylocationspost.ixCountrySubDivision, md_vw_countrylocationspost.sLocationCode, md_vw_countrylocationspost.sNameWithoutDiacritics, md_vw_countrylocationspost.sLatitude, md_vw_countrylocationspost.sLongitude, md_vw_countrylocationspost.UserName);
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
  

