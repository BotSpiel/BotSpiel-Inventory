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

    public class FacilitiesDB : DbContext
    {

        public FacilitiesDB(DbContextOptions<FacilitiesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Facilities> Facilities { get; set; }
		public DbSet<FacilitiesPost> FacilitiesPost { get; set; }
		public DbSet<Addresses> Addresses { get; set; }
		public DbSet<AddressesPost> AddressesPost { get; set; }
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
            modelBuilder.Entity<Facilities>()
                .ToTable("md_vw_Facilities")
                .HasKey(c => new { c.ixFacility });
            modelBuilder.Entity<FacilitiesPost>()
                .ToTable("md_vw_FacilitiesPost")
                .HasKey(c => new { c.ixFacility });
            modelBuilder.Entity<Addresses>()
                .ToTable("md_vw_Addresses")
                .HasKey(c => new { c.ixAddress });
            modelBuilder.Entity<AddressesPost>()
                .ToTable("md_vw_AddressesPost")
                .HasKey(c => new { c.ixAddress });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is FacilitiesPost)).ToList())
            {
                var md_vw_facilitiespost = e.Entity as FacilitiesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sFacility = cmd.CreateParameter();
                            sFacility.ParameterName = "p0";
                            sFacility.Value = md_vw_facilitiespost.sFacility;
                            var ixAddress = cmd.CreateParameter();
                            ixAddress.ParameterName = "p1";
                            ixAddress.Value = md_vw_facilitiespost.ixAddress;
                            var sLatitude = cmd.CreateParameter();
                            sLatitude.ParameterName = "p2";
                            sLatitude.Value = md_vw_facilitiespost.sLatitude;
                            var sLongitude = cmd.CreateParameter();
                            sLongitude.ParameterName = "p3";
                            sLongitude.Value = md_vw_facilitiespost.sLongitude;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p4";
                            UserName.Value = md_vw_facilitiespost.UserName;

                            var ixFacility = cmd.CreateParameter();
                            ixFacility.ParameterName = "p5";
                            ixFacility.DbType = DbType.Int64;
                            ixFacility.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateFacilities ");
                            sql.Append("@sFacility = @p0, ");
                            sql.Append("@ixAddress = @p1, ");
                            if (md_vw_facilitiespost.sLatitude != null) { sql.Append("@sLatitude = @p2, "); }  
                            if (md_vw_facilitiespost.sLongitude != null) { sql.Append("@sLongitude = @p3, "); }  
                            if (md_vw_facilitiespost.UserName != null) { sql.Append("@UserName = @p4, "); }  
                            sql.Append("@ixFacility = @p5 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sFacility);
                            cmd.Parameters.Add(ixAddress);
                            if (md_vw_facilitiespost.sLatitude != null) { cmd.Parameters.Add(sLatitude); }
                            if (md_vw_facilitiespost.sLongitude != null) { cmd.Parameters.Add(sLongitude); }
                            if (md_vw_facilitiespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixFacility); 
                            cmd.ExecuteNonQuery();
                            md_vw_facilitiespost.ixFacility = (Int64)ixFacility.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixFacility"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeFacilities @ixFacility = @p0, @sFacility = @p1, @ixAddress = @p2, @sLatitude = @p3, @sLongitude = @p4, @UserName = @p5", md_vw_facilitiespost.ixFacility, md_vw_facilitiespost.sFacility, md_vw_facilitiespost.ixAddress, md_vw_facilitiespost.sLatitude, md_vw_facilitiespost.sLongitude, md_vw_facilitiespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteFacilities @ixFacility = @p0, @sFacility = @p1, @ixAddress = @p2, @sLatitude = @p3, @sLongitude = @p4, @UserName = @p5", md_vw_facilitiespost.ixFacility, md_vw_facilitiespost.sFacility, md_vw_facilitiespost.ixAddress, md_vw_facilitiespost.sLatitude, md_vw_facilitiespost.sLongitude, md_vw_facilitiespost.UserName);
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
  

