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

    public class InboundOrdersDB : DbContext
    {

        public InboundOrdersDB(DbContextOptions<InboundOrdersDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InboundOrders> InboundOrders { get; set; }
		public DbSet<InboundOrdersPost> InboundOrdersPost { get; set; }
		public DbSet<Statuses> Statuses { get; set; }
		public DbSet<StatusesPost> StatusesPost { get; set; }
		public DbSet<Facilities> Facilities { get; set; }
		public DbSet<FacilitiesPost> FacilitiesPost { get; set; }
		public DbSet<BusinessPartners> BusinessPartners { get; set; }
		public DbSet<BusinessPartnersPost> BusinessPartnersPost { get; set; }
		public DbSet<InboundOrderTypes> InboundOrderTypes { get; set; }
		public DbSet<InboundOrderTypesPost> InboundOrderTypesPost { get; set; }
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
            modelBuilder.Entity<InboundOrders>()
                .ToTable("tx_vw_InboundOrders")
                .HasKey(c => new { c.ixInboundOrder });
            modelBuilder.Entity<InboundOrdersPost>()
                .ToTable("tx_vw_InboundOrdersPost")
                .HasKey(c => new { c.ixInboundOrder });
            modelBuilder.Entity<Statuses>()
                .ToTable("config_vw_Statuses")
                .HasKey(c => new { c.ixStatus });
            modelBuilder.Entity<StatusesPost>()
                .ToTable("config_vw_StatusesPost")
                .HasKey(c => new { c.ixStatus });
            modelBuilder.Entity<Facilities>()
                .ToTable("md_vw_Facilities")
                .HasKey(c => new { c.ixFacility });
            modelBuilder.Entity<FacilitiesPost>()
                .ToTable("md_vw_FacilitiesPost")
                .HasKey(c => new { c.ixFacility });
            modelBuilder.Entity<BusinessPartners>()
                .ToTable("md_vw_BusinessPartners")
                .HasKey(c => new { c.ixBusinessPartner });
            modelBuilder.Entity<BusinessPartnersPost>()
                .ToTable("md_vw_BusinessPartnersPost")
                .HasKey(c => new { c.ixBusinessPartner });
            modelBuilder.Entity<InboundOrderTypes>()
                .ToTable("config_vw_InboundOrderTypes")
                .HasKey(c => new { c.ixInboundOrderType });
            modelBuilder.Entity<InboundOrderTypesPost>()
                .ToTable("config_vw_InboundOrderTypesPost")
                .HasKey(c => new { c.ixInboundOrderType });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is InboundOrdersPost)).ToList())
            {
                var tx_vw_inboundorderspost = e.Entity as InboundOrdersPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sOrderReference = cmd.CreateParameter();
                            sOrderReference.ParameterName = "p0";
                            sOrderReference.Value = tx_vw_inboundorderspost.sOrderReference;
                            var ixInboundOrderType = cmd.CreateParameter();
                            ixInboundOrderType.ParameterName = "p1";
                            ixInboundOrderType.Value = tx_vw_inboundorderspost.ixInboundOrderType;
                            var ixFacility = cmd.CreateParameter();
                            ixFacility.ParameterName = "p2";
                            ixFacility.Value = tx_vw_inboundorderspost.ixFacility;
                            var ixCompany = cmd.CreateParameter();
                            ixCompany.ParameterName = "p3";
                            ixCompany.Value = tx_vw_inboundorderspost.ixCompany;
                            var ixBusinessPartner = cmd.CreateParameter();
                            ixBusinessPartner.ParameterName = "p4";
                            ixBusinessPartner.Value = tx_vw_inboundorderspost.ixBusinessPartner;
                            var dtExpectedAt = cmd.CreateParameter();
                            dtExpectedAt.ParameterName = "p5";
                            dtExpectedAt.Value = tx_vw_inboundorderspost.dtExpectedAt;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p6";
                            ixStatus.Value = tx_vw_inboundorderspost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p7";
                            UserName.Value = tx_vw_inboundorderspost.UserName;

                            var ixInboundOrder = cmd.CreateParameter();
                            ixInboundOrder.ParameterName = "p8";
                            ixInboundOrder.DbType = DbType.Int64;
                            ixInboundOrder.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateInboundOrders ");
                            sql.Append("@sOrderReference = @p0, ");
                            sql.Append("@ixInboundOrderType = @p1, ");
                            sql.Append("@ixFacility = @p2, ");
                            sql.Append("@ixCompany = @p3, ");
                            sql.Append("@ixBusinessPartner = @p4, ");
                            if (tx_vw_inboundorderspost.dtExpectedAt != null) { sql.Append("@dtExpectedAt = @p5, "); }  
                            sql.Append("@ixStatus = @p6, ");
                            if (tx_vw_inboundorderspost.UserName != null) { sql.Append("@UserName = @p7, "); }  
                            sql.Append("@ixInboundOrder = @p8 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sOrderReference);
                            cmd.Parameters.Add(ixInboundOrderType);
                            cmd.Parameters.Add(ixFacility);
                            cmd.Parameters.Add(ixCompany);
                            cmd.Parameters.Add(ixBusinessPartner);
                            if (tx_vw_inboundorderspost.dtExpectedAt != null) { cmd.Parameters.Add(dtExpectedAt); }
                            cmd.Parameters.Add(ixStatus);
                            if (tx_vw_inboundorderspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInboundOrder); 
                            cmd.ExecuteNonQuery();
                            tx_vw_inboundorderspost.ixInboundOrder = (Int64)ixInboundOrder.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInboundOrder"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeInboundOrders @ixInboundOrder = @p0, @sOrderReference = @p1, @ixInboundOrderType = @p2, @ixFacility = @p3, @ixCompany = @p4, @ixBusinessPartner = @p5, @dtExpectedAt = @p6, @ixStatus = @p7, @UserName = @p8", tx_vw_inboundorderspost.ixInboundOrder, tx_vw_inboundorderspost.sOrderReference, tx_vw_inboundorderspost.ixInboundOrderType, tx_vw_inboundorderspost.ixFacility, tx_vw_inboundorderspost.ixCompany, tx_vw_inboundorderspost.ixBusinessPartner, tx_vw_inboundorderspost.dtExpectedAt, tx_vw_inboundorderspost.ixStatus, tx_vw_inboundorderspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteInboundOrders @ixInboundOrder = @p0, @sOrderReference = @p1, @ixInboundOrderType = @p2, @ixFacility = @p3, @ixCompany = @p4, @ixBusinessPartner = @p5, @dtExpectedAt = @p6, @ixStatus = @p7, @UserName = @p8", tx_vw_inboundorderspost.ixInboundOrder, tx_vw_inboundorderspost.sOrderReference, tx_vw_inboundorderspost.ixInboundOrderType, tx_vw_inboundorderspost.ixFacility, tx_vw_inboundorderspost.ixCompany, tx_vw_inboundorderspost.ixBusinessPartner, tx_vw_inboundorderspost.dtExpectedAt, tx_vw_inboundorderspost.ixStatus, tx_vw_inboundorderspost.UserName);
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
  

