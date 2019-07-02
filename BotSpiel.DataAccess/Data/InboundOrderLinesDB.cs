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

    public class InboundOrderLinesDB : DbContext
    {

        public InboundOrderLinesDB(DbContextOptions<InboundOrderLinesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InboundOrderLines> InboundOrderLines { get; set; }
		public DbSet<InboundOrderLinesPost> InboundOrderLinesPost { get; set; }
		public DbSet<MaterialHandlingUnitConfigurations> MaterialHandlingUnitConfigurations { get; set; }
		public DbSet<MaterialHandlingUnitConfigurationsPost> MaterialHandlingUnitConfigurationsPost { get; set; }
		public DbSet<InboundOrders> InboundOrders { get; set; }
		public DbSet<InboundOrdersPost> InboundOrdersPost { get; set; }
		public DbSet<Statuses> Statuses { get; set; }
		public DbSet<StatusesPost> StatusesPost { get; set; }
		public DbSet<Facilities> Facilities { get; set; }
		public DbSet<FacilitiesPost> FacilitiesPost { get; set; }
		public DbSet<Materials> Materials { get; set; }
		public DbSet<MaterialsPost> MaterialsPost { get; set; }
		public DbSet<HandlingUnitTypes> HandlingUnitTypes { get; set; }
		public DbSet<HandlingUnitTypesPost> HandlingUnitTypesPost { get; set; }
		public DbSet<BusinessPartners> BusinessPartners { get; set; }
		public DbSet<BusinessPartnersPost> BusinessPartnersPost { get; set; }
		public DbSet<InboundOrderTypes> InboundOrderTypes { get; set; }
		public DbSet<InboundOrderTypesPost> InboundOrderTypesPost { get; set; }
		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
		public DbSet<Addresses> Addresses { get; set; }
		public DbSet<AddressesPost> AddressesPost { get; set; }
		public DbSet<Companies> Companies { get; set; }
		public DbSet<CompaniesPost> CompaniesPost { get; set; }
		public DbSet<MaterialTypes> MaterialTypes { get; set; }
		public DbSet<MaterialTypesPost> MaterialTypesPost { get; set; }
		public DbSet<BusinessPartnerTypes> BusinessPartnerTypes { get; set; }
		public DbSet<BusinessPartnerTypesPost> BusinessPartnerTypesPost { get; set; }
		public DbSet<CountrySubDivisions> CountrySubDivisions { get; set; }
		public DbSet<CountrySubDivisionsPost> CountrySubDivisionsPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
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
            modelBuilder.Entity<InboundOrderLines>()
                .ToTable("tx_vw_InboundOrderLines")
                .HasKey(c => new { c.ixInboundOrderLine });
            modelBuilder.Entity<InboundOrderLinesPost>()
                .ToTable("tx_vw_InboundOrderLinesPost")
                .HasKey(c => new { c.ixInboundOrderLine });
            modelBuilder.Entity<MaterialHandlingUnitConfigurations>()
                .ToTable("md_vw_MaterialHandlingUnitConfigurations")
                .HasKey(c => new { c.ixMaterialHandlingUnitConfiguration });
            modelBuilder.Entity<MaterialHandlingUnitConfigurationsPost>()
                .ToTable("md_vw_MaterialHandlingUnitConfigurationsPost")
                .HasKey(c => new { c.ixMaterialHandlingUnitConfiguration });
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
            modelBuilder.Entity<Materials>()
                .ToTable("md_vw_Materials")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<MaterialsPost>()
                .ToTable("md_vw_MaterialsPost")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<HandlingUnitTypes>()
                .ToTable("config_vw_HandlingUnitTypes")
                .HasKey(c => new { c.ixHandlingUnitType });
            modelBuilder.Entity<HandlingUnitTypesPost>()
                .ToTable("config_vw_HandlingUnitTypesPost")
                .HasKey(c => new { c.ixHandlingUnitType });
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
            modelBuilder.Entity<UnitsOfMeasurement>()
                .ToTable("config_vw_UnitsOfMeasurement")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<UnitsOfMeasurementPost>()
                .ToTable("config_vw_UnitsOfMeasurementPost")
                .HasKey(c => new { c.ixUnitOfMeasurement });
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
            modelBuilder.Entity<MaterialTypes>()
                .ToTable("config_vw_MaterialTypes")
                .HasKey(c => new { c.ixMaterialType });
            modelBuilder.Entity<MaterialTypesPost>()
                .ToTable("config_vw_MaterialTypesPost")
                .HasKey(c => new { c.ixMaterialType });
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
            modelBuilder.Entity<MeasurementSystems>()
                .ToTable("md_vw_MeasurementSystems")
                .HasKey(c => new { c.ixMeasurementSystem });
            modelBuilder.Entity<MeasurementSystemsPost>()
                .ToTable("md_vw_MeasurementSystemsPost")
                .HasKey(c => new { c.ixMeasurementSystem });
            modelBuilder.Entity<MeasurementUnitsOf>()
                .ToTable("config_vw_MeasurementUnitsOf")
                .HasKey(c => new { c.ixMeasurementUnitOf });
            modelBuilder.Entity<MeasurementUnitsOfPost>()
                .ToTable("config_vw_MeasurementUnitsOfPost")
                .HasKey(c => new { c.ixMeasurementUnitOf });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is InboundOrderLinesPost)).ToList())
            {
                var tx_vw_inboundorderlinespost = e.Entity as InboundOrderLinesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixInboundOrder = cmd.CreateParameter();
                            ixInboundOrder.ParameterName = "p0";
                            ixInboundOrder.Value = tx_vw_inboundorderlinespost.ixInboundOrder;
                            var sOrderLineReference = cmd.CreateParameter();
                            sOrderLineReference.ParameterName = "p1";
                            sOrderLineReference.Value = tx_vw_inboundorderlinespost.sOrderLineReference;
                            var ixMaterial = cmd.CreateParameter();
                            ixMaterial.ParameterName = "p2";
                            ixMaterial.Value = tx_vw_inboundorderlinespost.ixMaterial;
                            var ixMaterialHandlingUnitConfiguration = cmd.CreateParameter();
                            ixMaterialHandlingUnitConfiguration.ParameterName = "p3";
                            ixMaterialHandlingUnitConfiguration.Value = tx_vw_inboundorderlinespost.ixMaterialHandlingUnitConfiguration;
                            var ixHandlingUnitType = cmd.CreateParameter();
                            ixHandlingUnitType.ParameterName = "p4";
                            ixHandlingUnitType.Value = tx_vw_inboundorderlinespost.ixHandlingUnitType;
                            var nHandlingUnitQuantity = cmd.CreateParameter();
                            nHandlingUnitQuantity.ParameterName = "p5";
                            nHandlingUnitQuantity.Value = tx_vw_inboundorderlinespost.nHandlingUnitQuantity;
                            var nBaseUnitQuantityExpected = cmd.CreateParameter();
                            nBaseUnitQuantityExpected.ParameterName = "p6";
                            nBaseUnitQuantityExpected.Value = tx_vw_inboundorderlinespost.nBaseUnitQuantityExpected;
                            var nBaseUnitQuantityReceived = cmd.CreateParameter();
                            nBaseUnitQuantityReceived.ParameterName = "p7";
                            nBaseUnitQuantityReceived.Value = tx_vw_inboundorderlinespost.nBaseUnitQuantityReceived;
                            var sBatchNumber = cmd.CreateParameter();
                            sBatchNumber.ParameterName = "p8";
                            sBatchNumber.Value = tx_vw_inboundorderlinespost.sBatchNumber;
                            var sSerialNumber = cmd.CreateParameter();
                            sSerialNumber.ParameterName = "p9";
                            sSerialNumber.Value = tx_vw_inboundorderlinespost.sSerialNumber;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p10";
                            ixStatus.Value = tx_vw_inboundorderlinespost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p11";
                            UserName.Value = tx_vw_inboundorderlinespost.UserName;

                            var ixInboundOrderLine = cmd.CreateParameter();
                            ixInboundOrderLine.ParameterName = "p12";
                            ixInboundOrderLine.DbType = DbType.Int64;
                            ixInboundOrderLine.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateInboundOrderLines ");
                            sql.Append("@ixInboundOrder = @p0, ");
                            sql.Append("@sOrderLineReference = @p1, ");
                            sql.Append("@ixMaterial = @p2, ");
                            if (tx_vw_inboundorderlinespost.ixMaterialHandlingUnitConfiguration != null) { sql.Append("@ixMaterialHandlingUnitConfiguration = @p3, "); }  
                            if (tx_vw_inboundorderlinespost.ixHandlingUnitType != null) { sql.Append("@ixHandlingUnitType = @p4, "); }  
                            if (tx_vw_inboundorderlinespost.nHandlingUnitQuantity != null) { sql.Append("@nHandlingUnitQuantity = @p5, "); }  
                            sql.Append("@nBaseUnitQuantityExpected = @p6, ");
                            sql.Append("@nBaseUnitQuantityReceived = @p7, ");
                            if (tx_vw_inboundorderlinespost.sBatchNumber != null) { sql.Append("@sBatchNumber = @p8, "); }  
                            if (tx_vw_inboundorderlinespost.sSerialNumber != null) { sql.Append("@sSerialNumber = @p9, "); }  
                            sql.Append("@ixStatus = @p10, ");
                            if (tx_vw_inboundorderlinespost.UserName != null) { sql.Append("@UserName = @p11, "); }  
                            sql.Append("@ixInboundOrderLine = @p12 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixInboundOrder);
                            cmd.Parameters.Add(sOrderLineReference);
                            cmd.Parameters.Add(ixMaterial);
                            if (tx_vw_inboundorderlinespost.ixMaterialHandlingUnitConfiguration != null) { cmd.Parameters.Add(ixMaterialHandlingUnitConfiguration); }
                            if (tx_vw_inboundorderlinespost.ixHandlingUnitType != null) { cmd.Parameters.Add(ixHandlingUnitType); }
                            if (tx_vw_inboundorderlinespost.nHandlingUnitQuantity != null) { cmd.Parameters.Add(nHandlingUnitQuantity); }
                            cmd.Parameters.Add(nBaseUnitQuantityExpected);
                            cmd.Parameters.Add(nBaseUnitQuantityReceived);
                            if (tx_vw_inboundorderlinespost.sBatchNumber != null) { cmd.Parameters.Add(sBatchNumber); }
                            if (tx_vw_inboundorderlinespost.sSerialNumber != null) { cmd.Parameters.Add(sSerialNumber); }
                            cmd.Parameters.Add(ixStatus);
                            if (tx_vw_inboundorderlinespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInboundOrderLine); 
                            cmd.ExecuteNonQuery();
                            tx_vw_inboundorderlinespost.ixInboundOrderLine = (Int64)ixInboundOrderLine.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInboundOrderLine"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeInboundOrderLines @ixInboundOrderLine = @p0, @ixInboundOrder = @p1, @sOrderLineReference = @p2, @ixMaterial = @p3, @ixMaterialHandlingUnitConfiguration = @p4, @ixHandlingUnitType = @p5, @nHandlingUnitQuantity = @p6, @nBaseUnitQuantityExpected = @p7, @nBaseUnitQuantityReceived = @p8, @sBatchNumber = @p9, @sSerialNumber = @p10, @ixStatus = @p11, @UserName = @p12", tx_vw_inboundorderlinespost.ixInboundOrderLine, tx_vw_inboundorderlinespost.ixInboundOrder, tx_vw_inboundorderlinespost.sOrderLineReference, tx_vw_inboundorderlinespost.ixMaterial, tx_vw_inboundorderlinespost.ixMaterialHandlingUnitConfiguration, tx_vw_inboundorderlinespost.ixHandlingUnitType, tx_vw_inboundorderlinespost.nHandlingUnitQuantity, tx_vw_inboundorderlinespost.nBaseUnitQuantityExpected, tx_vw_inboundorderlinespost.nBaseUnitQuantityReceived, tx_vw_inboundorderlinespost.sBatchNumber, tx_vw_inboundorderlinespost.sSerialNumber, tx_vw_inboundorderlinespost.ixStatus, tx_vw_inboundorderlinespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteInboundOrderLines @ixInboundOrderLine = @p0, @ixInboundOrder = @p1, @sOrderLineReference = @p2, @ixMaterial = @p3, @ixMaterialHandlingUnitConfiguration = @p4, @ixHandlingUnitType = @p5, @nHandlingUnitQuantity = @p6, @nBaseUnitQuantityExpected = @p7, @nBaseUnitQuantityReceived = @p8, @sBatchNumber = @p9, @sSerialNumber = @p10, @ixStatus = @p11, @UserName = @p12", tx_vw_inboundorderlinespost.ixInboundOrderLine, tx_vw_inboundorderlinespost.ixInboundOrder, tx_vw_inboundorderlinespost.sOrderLineReference, tx_vw_inboundorderlinespost.ixMaterial, tx_vw_inboundorderlinespost.ixMaterialHandlingUnitConfiguration, tx_vw_inboundorderlinespost.ixHandlingUnitType, tx_vw_inboundorderlinespost.nHandlingUnitQuantity, tx_vw_inboundorderlinespost.nBaseUnitQuantityExpected, tx_vw_inboundorderlinespost.nBaseUnitQuantityReceived, tx_vw_inboundorderlinespost.sBatchNumber, tx_vw_inboundorderlinespost.sSerialNumber, tx_vw_inboundorderlinespost.ixStatus, tx_vw_inboundorderlinespost.UserName);
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
  

