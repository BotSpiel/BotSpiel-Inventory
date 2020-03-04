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

    public class SetUpExecutionParametersDB : DbContext
    {

        public SetUpExecutionParametersDB(DbContextOptions<SetUpExecutionParametersDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<SetUpExecutionParameters> SetUpExecutionParameters { get; set; }
		public DbSet<SetUpExecutionParametersPost> SetUpExecutionParametersPost { get; set; }
		public DbSet<Companies> Companies { get; set; }
		public DbSet<CompaniesPost> CompaniesPost { get; set; }
		public DbSet<Facilities> Facilities { get; set; }
		public DbSet<FacilitiesPost> FacilitiesPost { get; set; }
		public DbSet<FacilityWorkAreas> FacilityWorkAreas { get; set; }
		public DbSet<FacilityWorkAreasPost> FacilityWorkAreasPost { get; set; }
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
            modelBuilder.Entity<SetUpExecutionParameters>()
                .ToTable("tx_vw_SetUpExecutionParameters")
                .HasKey(c => new { c.ixSetUpExecutionParameter });
            modelBuilder.Entity<SetUpExecutionParametersPost>()
                .ToTable("tx_vw_SetUpExecutionParametersPost")
                .HasKey(c => new { c.ixSetUpExecutionParameter });
            modelBuilder.Entity<Companies>()
                .ToTable("md_vw_Companies")
                .HasKey(c => new { c.ixCompany });
            modelBuilder.Entity<CompaniesPost>()
                .ToTable("md_vw_CompaniesPost")
                .HasKey(c => new { c.ixCompany });
            modelBuilder.Entity<Facilities>()
                .ToTable("md_vw_Facilities")
                .HasKey(c => new { c.ixFacility });
            modelBuilder.Entity<FacilitiesPost>()
                .ToTable("md_vw_FacilitiesPost")
                .HasKey(c => new { c.ixFacility });
            modelBuilder.Entity<FacilityWorkAreas>()
                .ToTable("md_vw_FacilityWorkAreas")
                .HasKey(c => new { c.ixFacilityWorkArea });
            modelBuilder.Entity<FacilityWorkAreasPost>()
                .ToTable("md_vw_FacilityWorkAreasPost")
                .HasKey(c => new { c.ixFacilityWorkArea });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is SetUpExecutionParametersPost)).ToList())
            {
                var tx_vw_setupexecutionparameterspost = e.Entity as SetUpExecutionParametersPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixFacility = cmd.CreateParameter();
                            ixFacility.ParameterName = "p0";
                            ixFacility.Value = tx_vw_setupexecutionparameterspost.ixFacility;
                            var ixCompany = cmd.CreateParameter();
                            ixCompany.ParameterName = "p1";
                            ixCompany.Value = tx_vw_setupexecutionparameterspost.ixCompany;
                            var ixFacilityWorkArea = cmd.CreateParameter();
                            ixFacilityWorkArea.ParameterName = "p2";
                            ixFacilityWorkArea.Value = tx_vw_setupexecutionparameterspost.ixFacilityWorkArea;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p3";
                            UserName.Value = tx_vw_setupexecutionparameterspost.UserName;

                            var ixSetUpExecutionParameter = cmd.CreateParameter();
                            ixSetUpExecutionParameter.ParameterName = "p4";
                            ixSetUpExecutionParameter.DbType = DbType.Int64;
                            ixSetUpExecutionParameter.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateSetUpExecutionParameters ");
                            sql.Append("@ixFacility = @p0, ");
                            sql.Append("@ixCompany = @p1, ");
                            sql.Append("@ixFacilityWorkArea = @p2, ");
                            if (tx_vw_setupexecutionparameterspost.UserName != null) { sql.Append("@UserName = @p3, "); }  
                            sql.Append("@ixSetUpExecutionParameter = @p4 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixFacility);
                            cmd.Parameters.Add(ixCompany);
                            cmd.Parameters.Add(ixFacilityWorkArea);
                            if (tx_vw_setupexecutionparameterspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixSetUpExecutionParameter); 
                            cmd.ExecuteNonQuery();
                            tx_vw_setupexecutionparameterspost.ixSetUpExecutionParameter = (Int64)ixSetUpExecutionParameter.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixSetUpExecutionParameter"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeSetUpExecutionParameters @ixSetUpExecutionParameter = @p0, @ixFacility = @p1, @ixCompany = @p2, @ixFacilityWorkArea = @p3, @UserName = @p4", tx_vw_setupexecutionparameterspost.ixSetUpExecutionParameter, tx_vw_setupexecutionparameterspost.ixFacility, tx_vw_setupexecutionparameterspost.ixCompany, tx_vw_setupexecutionparameterspost.ixFacilityWorkArea, tx_vw_setupexecutionparameterspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteSetUpExecutionParameters @ixSetUpExecutionParameter = @p0, @ixFacility = @p1, @ixCompany = @p2, @ixFacilityWorkArea = @p3, @UserName = @p4", tx_vw_setupexecutionparameterspost.ixSetUpExecutionParameter, tx_vw_setupexecutionparameterspost.ixFacility, tx_vw_setupexecutionparameterspost.ixCompany, tx_vw_setupexecutionparameterspost.ixFacilityWorkArea, tx_vw_setupexecutionparameterspost.UserName);
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
  

