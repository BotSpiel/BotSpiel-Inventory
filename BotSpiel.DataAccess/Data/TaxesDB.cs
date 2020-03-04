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

    public class TaxesDB : DbContext
    {

        public TaxesDB(DbContextOptions<TaxesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Taxes> Taxes { get; set; }
		public DbSet<TaxesPost> TaxesPost { get; set; }
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
            modelBuilder.Entity<Taxes>()
                .ToTable("md_vw_Taxes")
                .HasKey(c => new { c.ixTax });
            modelBuilder.Entity<TaxesPost>()
                .ToTable("md_vw_TaxesPost")
                .HasKey(c => new { c.ixTax });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is TaxesPost)).ToList())
            {
                var md_vw_taxespost = e.Entity as TaxesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sTax = cmd.CreateParameter();
                            sTax.ParameterName = "p0";
                            sTax.Value = md_vw_taxespost.sTax;
                            var ixCountry = cmd.CreateParameter();
                            ixCountry.ParameterName = "p1";
                            ixCountry.Value = md_vw_taxespost.ixCountry;
                            var ixCountrySubDivision = cmd.CreateParameter();
                            ixCountrySubDivision.ParameterName = "p2";
                            ixCountrySubDivision.Value = md_vw_taxespost.ixCountrySubDivision;
                            var nRate = cmd.CreateParameter();
                            nRate.ParameterName = "p3";
                            nRate.Value = md_vw_taxespost.nRate;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p4";
                            UserName.Value = md_vw_taxespost.UserName;

                            var ixTax = cmd.CreateParameter();
                            ixTax.ParameterName = "p5";
                            ixTax.DbType = DbType.Int64;
                            ixTax.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateTaxes ");
                            sql.Append("@sTax = @p0, ");
                            sql.Append("@ixCountry = @p1, ");
                            sql.Append("@ixCountrySubDivision = @p2, ");
                            sql.Append("@nRate = @p3, ");
                            if (md_vw_taxespost.UserName != null) { sql.Append("@UserName = @p4, "); }  
                            sql.Append("@ixTax = @p5 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sTax);
                            cmd.Parameters.Add(ixCountry);
                            cmd.Parameters.Add(ixCountrySubDivision);
                            cmd.Parameters.Add(nRate);
                            if (md_vw_taxespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixTax); 
                            cmd.ExecuteNonQuery();
                            md_vw_taxespost.ixTax = (Int64)ixTax.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixTax"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeTaxes @ixTax = @p0, @sTax = @p1, @ixCountry = @p2, @ixCountrySubDivision = @p3, @nRate = @p4, @UserName = @p5", md_vw_taxespost.ixTax, md_vw_taxespost.sTax, md_vw_taxespost.ixCountry, md_vw_taxespost.ixCountrySubDivision, md_vw_taxespost.nRate, md_vw_taxespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteTaxes @ixTax = @p0, @sTax = @p1, @ixCountry = @p2, @ixCountrySubDivision = @p3, @nRate = @p4, @UserName = @p5", md_vw_taxespost.ixTax, md_vw_taxespost.sTax, md_vw_taxespost.ixCountry, md_vw_taxespost.ixCountrySubDivision, md_vw_taxespost.nRate, md_vw_taxespost.UserName);
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
  

