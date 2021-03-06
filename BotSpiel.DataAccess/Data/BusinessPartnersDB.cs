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

    public class BusinessPartnersDB : DbContext
    {

        public BusinessPartnersDB(DbContextOptions<BusinessPartnersDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<BusinessPartners> BusinessPartners { get; set; }
		public DbSet<BusinessPartnersPost> BusinessPartnersPost { get; set; }
		public DbSet<Addresses> Addresses { get; set; }
		public DbSet<AddressesPost> AddressesPost { get; set; }
		public DbSet<Companies> Companies { get; set; }
		public DbSet<CompaniesPost> CompaniesPost { get; set; }
		public DbSet<BusinessPartnerTypes> BusinessPartnerTypes { get; set; }
		public DbSet<BusinessPartnerTypesPost> BusinessPartnerTypesPost { get; set; }
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
            modelBuilder.Entity<BusinessPartners>()
                .ToTable("md_vw_BusinessPartners")
                .HasKey(c => new { c.ixBusinessPartner });
            modelBuilder.Entity<BusinessPartnersPost>()
                .ToTable("md_vw_BusinessPartnersPost")
                .HasKey(c => new { c.ixBusinessPartner });
            modelBuilder.Entity<Addresses>()
                .ToTable("md_vw_Addresses")
                .HasKey(c => new { c.ixAddress });
            modelBuilder.Entity<AddressesPost>()
                .ToTable("md_vw_AddressesPost")
                .HasKey(c => new { c.ixAddress });
            modelBuilder.Entity<Companies>()
                .ToTable("md_vw_Companies")
                .HasKey(c => new { c.ixCompany });
            modelBuilder.Entity<CompaniesPost>()
                .ToTable("md_vw_CompaniesPost")
                .HasKey(c => new { c.ixCompany });
            modelBuilder.Entity<BusinessPartnerTypes>()
                .ToTable("config_vw_BusinessPartnerTypes")
                .HasKey(c => new { c.ixBusinessPartnerType });
            modelBuilder.Entity<BusinessPartnerTypesPost>()
                .ToTable("config_vw_BusinessPartnerTypesPost")
                .HasKey(c => new { c.ixBusinessPartnerType });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is BusinessPartnersPost)).ToList())
            {
                var md_vw_businesspartnerspost = e.Entity as BusinessPartnersPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sBusinessPartner = cmd.CreateParameter();
                            sBusinessPartner.ParameterName = "p0";
                            sBusinessPartner.Value = md_vw_businesspartnerspost.sBusinessPartner;
                            var ixBusinessPartnerType = cmd.CreateParameter();
                            ixBusinessPartnerType.ParameterName = "p1";
                            ixBusinessPartnerType.Value = md_vw_businesspartnerspost.ixBusinessPartnerType;
                            var ixCompany = cmd.CreateParameter();
                            ixCompany.ParameterName = "p2";
                            ixCompany.Value = md_vw_businesspartnerspost.ixCompany;
                            var ixAddress = cmd.CreateParameter();
                            ixAddress.ParameterName = "p3";
                            ixAddress.Value = md_vw_businesspartnerspost.ixAddress;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p4";
                            UserName.Value = md_vw_businesspartnerspost.UserName;

                            var ixBusinessPartner = cmd.CreateParameter();
                            ixBusinessPartner.ParameterName = "p5";
                            ixBusinessPartner.DbType = DbType.Int64;
                            ixBusinessPartner.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateBusinessPartners ");
                            sql.Append("@sBusinessPartner = @p0, ");
                            sql.Append("@ixBusinessPartnerType = @p1, ");
                            sql.Append("@ixCompany = @p2, ");
                            sql.Append("@ixAddress = @p3, ");
                            if (md_vw_businesspartnerspost.UserName != null) { sql.Append("@UserName = @p4, "); }  
                            sql.Append("@ixBusinessPartner = @p5 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sBusinessPartner);
                            cmd.Parameters.Add(ixBusinessPartnerType);
                            cmd.Parameters.Add(ixCompany);
                            cmd.Parameters.Add(ixAddress);
                            if (md_vw_businesspartnerspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixBusinessPartner); 
                            cmd.ExecuteNonQuery();
                            md_vw_businesspartnerspost.ixBusinessPartner = (Int64)ixBusinessPartner.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixBusinessPartner"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeBusinessPartners @ixBusinessPartner = @p0, @sBusinessPartner = @p1, @ixBusinessPartnerType = @p2, @ixCompany = @p3, @ixAddress = @p4, @UserName = @p5", md_vw_businesspartnerspost.ixBusinessPartner, md_vw_businesspartnerspost.sBusinessPartner, md_vw_businesspartnerspost.ixBusinessPartnerType, md_vw_businesspartnerspost.ixCompany, md_vw_businesspartnerspost.ixAddress, md_vw_businesspartnerspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteBusinessPartners @ixBusinessPartner = @p0, @sBusinessPartner = @p1, @ixBusinessPartnerType = @p2, @ixCompany = @p3, @ixAddress = @p4, @UserName = @p5", md_vw_businesspartnerspost.ixBusinessPartner, md_vw_businesspartnerspost.sBusinessPartner, md_vw_businesspartnerspost.ixBusinessPartnerType, md_vw_businesspartnerspost.ixCompany, md_vw_businesspartnerspost.ixAddress, md_vw_businesspartnerspost.UserName);
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
  

