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

    public class ReceivingDB : DbContext
    {

        public ReceivingDB(DbContextOptions<ReceivingDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Receiving> Receiving { get; set; }
		public DbSet<ReceivingPost> ReceivingPost { get; set; }
		public DbSet<InventoryStates> InventoryStates { get; set; }
		public DbSet<InventoryStatesPost> InventoryStatesPost { get; set; }
		public DbSet<InventoryLocations> InventoryLocations { get; set; }
		public DbSet<InventoryLocationsPost> InventoryLocationsPost { get; set; }
		public DbSet<InboundOrders> InboundOrders { get; set; }
		public DbSet<InboundOrdersPost> InboundOrdersPost { get; set; }
		public DbSet<Facilities> Facilities { get; set; }
		public DbSet<FacilitiesPost> FacilitiesPost { get; set; }
		public DbSet<FacilityWorkAreas> FacilityWorkAreas { get; set; }
		public DbSet<FacilityWorkAreasPost> FacilityWorkAreasPost { get; set; }
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
		public DbSet<LocationFunctions> LocationFunctions { get; set; }
		public DbSet<LocationFunctionsPost> LocationFunctionsPost { get; set; }
		public DbSet<Statuses> Statuses { get; set; }
		public DbSet<StatusesPost> StatusesPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
		public DbSet<FacilityZones> FacilityZones { get; set; }
		public DbSet<FacilityZonesPost> FacilityZonesPost { get; set; }
		public DbSet<FacilityFloors> FacilityFloors { get; set; }
		public DbSet<FacilityFloorsPost> FacilityFloorsPost { get; set; }
		public DbSet<FacilityAisleFaces> FacilityAisleFaces { get; set; }
		public DbSet<FacilityAisleFacesPost> FacilityAisleFacesPost { get; set; }
		public DbSet<BaySequenceTypes> BaySequenceTypes { get; set; }
		public DbSet<BaySequenceTypesPost> BaySequenceTypesPost { get; set; }
		public DbSet<LogicalOrientations> LogicalOrientations { get; set; }
		public DbSet<LogicalOrientationsPost> LogicalOrientationsPost { get; set; }
		public DbSet<AisleFaceStorageTypes> AisleFaceStorageTypes { get; set; }
		public DbSet<AisleFaceStorageTypesPost> AisleFaceStorageTypesPost { get; set; }
		public DbSet<InventoryLocationSizes> InventoryLocationSizes { get; set; }
		public DbSet<InventoryLocationSizesPost> InventoryLocationSizesPost { get; set; }
		public DbSet<Materials> Materials { get; set; }
		public DbSet<MaterialsPost> MaterialsPost { get; set; }
		public DbSet<MaterialTypes> MaterialTypes { get; set; }
		public DbSet<MaterialTypesPost> MaterialTypesPost { get; set; }
		public DbSet<HandlingUnits> HandlingUnits { get; set; }
		public DbSet<HandlingUnitsPost> HandlingUnitsPost { get; set; }
		public DbSet<HandlingUnitTypes> HandlingUnitTypes { get; set; }
		public DbSet<HandlingUnitTypesPost> HandlingUnitTypesPost { get; set; }
		public DbSet<MaterialHandlingUnitConfigurations> MaterialHandlingUnitConfigurations { get; set; }
		public DbSet<MaterialHandlingUnitConfigurationsPost> MaterialHandlingUnitConfigurationsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receiving>()
                .ToTable("tx_vw_Receiving")
                .HasKey(c => new { c.ixReceipt });
            modelBuilder.Entity<ReceivingPost>()
                .ToTable("tx_vw_ReceivingPost")
                .HasKey(c => new { c.ixReceipt });
            modelBuilder.Entity<InventoryStates>()
                .ToTable("config_vw_InventoryStates")
                .HasKey(c => new { c.ixInventoryState });
            modelBuilder.Entity<InventoryStatesPost>()
                .ToTable("config_vw_InventoryStatesPost")
                .HasKey(c => new { c.ixInventoryState });
            modelBuilder.Entity<InventoryLocations>()
                .ToTable("md_vw_InventoryLocations")
                .HasKey(c => new { c.ixInventoryLocation });
            modelBuilder.Entity<InventoryLocationsPost>()
                .ToTable("md_vw_InventoryLocationsPost")
                .HasKey(c => new { c.ixInventoryLocation });
            modelBuilder.Entity<InboundOrders>()
                .ToTable("tx_vw_InboundOrders")
                .HasKey(c => new { c.ixInboundOrder });
            modelBuilder.Entity<InboundOrdersPost>()
                .ToTable("tx_vw_InboundOrdersPost")
                .HasKey(c => new { c.ixInboundOrder });
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
            modelBuilder.Entity<LocationFunctions>()
                .ToTable("config_vw_LocationFunctions")
                .HasKey(c => new { c.ixLocationFunction });
            modelBuilder.Entity<LocationFunctionsPost>()
                .ToTable("config_vw_LocationFunctionsPost")
                .HasKey(c => new { c.ixLocationFunction });
            modelBuilder.Entity<Statuses>()
                .ToTable("config_vw_Statuses")
                .HasKey(c => new { c.ixStatus });
            modelBuilder.Entity<StatusesPost>()
                .ToTable("config_vw_StatusesPost")
                .HasKey(c => new { c.ixStatus });
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
            modelBuilder.Entity<UnitsOfMeasurement>()
                .ToTable("config_vw_UnitsOfMeasurement")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<UnitsOfMeasurementPost>()
                .ToTable("config_vw_UnitsOfMeasurementPost")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<FacilityZones>()
                .ToTable("md_vw_FacilityZones")
                .HasKey(c => new { c.ixFacilityZone });
            modelBuilder.Entity<FacilityZonesPost>()
                .ToTable("md_vw_FacilityZonesPost")
                .HasKey(c => new { c.ixFacilityZone });
            modelBuilder.Entity<FacilityFloors>()
                .ToTable("md_vw_FacilityFloors")
                .HasKey(c => new { c.ixFacilityFloor });
            modelBuilder.Entity<FacilityFloorsPost>()
                .ToTable("md_vw_FacilityFloorsPost")
                .HasKey(c => new { c.ixFacilityFloor });
            modelBuilder.Entity<FacilityAisleFaces>()
                .ToTable("md_vw_FacilityAisleFaces")
                .HasKey(c => new { c.ixFacilityAisleFace });
            modelBuilder.Entity<FacilityAisleFacesPost>()
                .ToTable("md_vw_FacilityAisleFacesPost")
                .HasKey(c => new { c.ixFacilityAisleFace });
            modelBuilder.Entity<BaySequenceTypes>()
                .ToTable("config_vw_BaySequenceTypes")
                .HasKey(c => new { c.ixBaySequenceType });
            modelBuilder.Entity<BaySequenceTypesPost>()
                .ToTable("config_vw_BaySequenceTypesPost")
                .HasKey(c => new { c.ixBaySequenceType });
            modelBuilder.Entity<LogicalOrientations>()
                .ToTable("config_vw_LogicalOrientations")
                .HasKey(c => new { c.ixLogicalOrientation });
            modelBuilder.Entity<LogicalOrientationsPost>()
                .ToTable("config_vw_LogicalOrientationsPost")
                .HasKey(c => new { c.ixLogicalOrientation });
            modelBuilder.Entity<AisleFaceStorageTypes>()
                .ToTable("config_vw_AisleFaceStorageTypes")
                .HasKey(c => new { c.ixAisleFaceStorageType });
            modelBuilder.Entity<AisleFaceStorageTypesPost>()
                .ToTable("config_vw_AisleFaceStorageTypesPost")
                .HasKey(c => new { c.ixAisleFaceStorageType });
            modelBuilder.Entity<InventoryLocationSizes>()
                .ToTable("md_vw_InventoryLocationSizes")
                .HasKey(c => new { c.ixInventoryLocationSize });
            modelBuilder.Entity<InventoryLocationSizesPost>()
                .ToTable("md_vw_InventoryLocationSizesPost")
                .HasKey(c => new { c.ixInventoryLocationSize });
            modelBuilder.Entity<Materials>()
                .ToTable("md_vw_Materials")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<MaterialsPost>()
                .ToTable("md_vw_MaterialsPost")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<MaterialTypes>()
                .ToTable("config_vw_MaterialTypes")
                .HasKey(c => new { c.ixMaterialType });
            modelBuilder.Entity<MaterialTypesPost>()
                .ToTable("config_vw_MaterialTypesPost")
                .HasKey(c => new { c.ixMaterialType });
            modelBuilder.Entity<HandlingUnits>()
                .ToTable("tx_vw_HandlingUnits")
                .HasKey(c => new { c.ixHandlingUnit });
            modelBuilder.Entity<HandlingUnitsPost>()
                .ToTable("tx_vw_HandlingUnitsPost")
                .HasKey(c => new { c.ixHandlingUnit });
            modelBuilder.Entity<HandlingUnitTypes>()
                .ToTable("config_vw_HandlingUnitTypes")
                .HasKey(c => new { c.ixHandlingUnitType });
            modelBuilder.Entity<HandlingUnitTypesPost>()
                .ToTable("config_vw_HandlingUnitTypesPost")
                .HasKey(c => new { c.ixHandlingUnitType });
            modelBuilder.Entity<MaterialHandlingUnitConfigurations>()
                .ToTable("md_vw_MaterialHandlingUnitConfigurations")
                .HasKey(c => new { c.ixMaterialHandlingUnitConfiguration });
            modelBuilder.Entity<MaterialHandlingUnitConfigurationsPost>()
                .ToTable("md_vw_MaterialHandlingUnitConfigurationsPost")
                .HasKey(c => new { c.ixMaterialHandlingUnitConfiguration });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is ReceivingPost)).ToList())
            {
                var tx_vw_receivingpost = e.Entity as ReceivingPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixInventoryLocation = cmd.CreateParameter();
                            ixInventoryLocation.ParameterName = "p0";
                            ixInventoryLocation.Value = tx_vw_receivingpost.ixInventoryLocation;
                            var ixInboundOrder = cmd.CreateParameter();
                            ixInboundOrder.ParameterName = "p1";
                            ixInboundOrder.Value = tx_vw_receivingpost.ixInboundOrder;
                            var ixHandlingUnit = cmd.CreateParameter();
                            ixHandlingUnit.ParameterName = "p2";
                            ixHandlingUnit.Value = tx_vw_receivingpost.ixHandlingUnit;
                            var ixMaterial = cmd.CreateParameter();
                            ixMaterial.ParameterName = "p3";
                            ixMaterial.Value = tx_vw_receivingpost.ixMaterial;
                            var ixMaterialHandlingUnitConfiguration = cmd.CreateParameter();
                            ixMaterialHandlingUnitConfiguration.ParameterName = "p4";
                            ixMaterialHandlingUnitConfiguration.Value = tx_vw_receivingpost.ixMaterialHandlingUnitConfiguration;
                            var ixHandlingUnitType = cmd.CreateParameter();
                            ixHandlingUnitType.ParameterName = "p5";
                            ixHandlingUnitType.Value = tx_vw_receivingpost.ixHandlingUnitType;
                            var nHandlingUnitQuantity = cmd.CreateParameter();
                            nHandlingUnitQuantity.ParameterName = "p6";
                            nHandlingUnitQuantity.Value = tx_vw_receivingpost.nHandlingUnitQuantity;
                            var sSerialNumber = cmd.CreateParameter();
                            sSerialNumber.ParameterName = "p7";
                            sSerialNumber.Value = tx_vw_receivingpost.sSerialNumber;
                            var sBatchNumber = cmd.CreateParameter();
                            sBatchNumber.ParameterName = "p8";
                            sBatchNumber.Value = tx_vw_receivingpost.sBatchNumber;
                            var dtExpireAt = cmd.CreateParameter();
                            dtExpireAt.ParameterName = "p9";
                            dtExpireAt.Value = tx_vw_receivingpost.dtExpireAt;
                            var nBaseUnitQuantityReceived = cmd.CreateParameter();
                            nBaseUnitQuantityReceived.ParameterName = "p10";
                            nBaseUnitQuantityReceived.Value = tx_vw_receivingpost.nBaseUnitQuantityReceived;
                            var ixInventoryState = cmd.CreateParameter();
                            ixInventoryState.ParameterName = "p11";
                            ixInventoryState.Value = tx_vw_receivingpost.ixInventoryState;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p12";
                            ixStatus.Value = tx_vw_receivingpost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p13";
                            UserName.Value = tx_vw_receivingpost.UserName;

                            var ixReceipt = cmd.CreateParameter();
                            ixReceipt.ParameterName = "p14";
                            ixReceipt.DbType = DbType.Int64;
                            ixReceipt.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateReceiving ");
                            sql.Append("@ixInventoryLocation = @p0, ");
                            sql.Append("@ixInboundOrder = @p1, ");
                            sql.Append("@ixHandlingUnit = @p2, ");
                            sql.Append("@ixMaterial = @p3, ");
                            if (tx_vw_receivingpost.ixMaterialHandlingUnitConfiguration != null) { sql.Append("@ixMaterialHandlingUnitConfiguration = @p4, "); }  
                            if (tx_vw_receivingpost.ixHandlingUnitType != null) { sql.Append("@ixHandlingUnitType = @p5, "); }  
                            if (tx_vw_receivingpost.nHandlingUnitQuantity != null) { sql.Append("@nHandlingUnitQuantity = @p6, "); }  
                            if (tx_vw_receivingpost.sSerialNumber != null) { sql.Append("@sSerialNumber = @p7, "); }  
                            if (tx_vw_receivingpost.sBatchNumber != null) { sql.Append("@sBatchNumber = @p8, "); }  
                            if (tx_vw_receivingpost.dtExpireAt != null) { sql.Append("@dtExpireAt = @p9, "); }  
                            sql.Append("@nBaseUnitQuantityReceived = @p10, ");
                            sql.Append("@ixInventoryState = @p11, ");
                            sql.Append("@ixStatus = @p12, ");
                            if (tx_vw_receivingpost.UserName != null) { sql.Append("@UserName = @p13, "); }  
                            sql.Append("@ixReceipt = @p14 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixInventoryLocation);
                            cmd.Parameters.Add(ixInboundOrder);
                            cmd.Parameters.Add(ixHandlingUnit);
                            cmd.Parameters.Add(ixMaterial);
                            if (tx_vw_receivingpost.ixMaterialHandlingUnitConfiguration != null) { cmd.Parameters.Add(ixMaterialHandlingUnitConfiguration); }
                            if (tx_vw_receivingpost.ixHandlingUnitType != null) { cmd.Parameters.Add(ixHandlingUnitType); }
                            if (tx_vw_receivingpost.nHandlingUnitQuantity != null) { cmd.Parameters.Add(nHandlingUnitQuantity); }
                            if (tx_vw_receivingpost.sSerialNumber != null) { cmd.Parameters.Add(sSerialNumber); }
                            if (tx_vw_receivingpost.sBatchNumber != null) { cmd.Parameters.Add(sBatchNumber); }
                            if (tx_vw_receivingpost.dtExpireAt != null) { cmd.Parameters.Add(dtExpireAt); }
                            cmd.Parameters.Add(nBaseUnitQuantityReceived);
                            cmd.Parameters.Add(ixInventoryState);
                            cmd.Parameters.Add(ixStatus);
                            if (tx_vw_receivingpost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixReceipt); 
                            cmd.ExecuteNonQuery();
                            tx_vw_receivingpost.ixReceipt = (Int64)ixReceipt.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixReceipt"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeReceiving @ixReceipt = @p0, @ixInventoryLocation = @p1, @ixInboundOrder = @p2, @ixHandlingUnit = @p3, @ixMaterial = @p4, @ixMaterialHandlingUnitConfiguration = @p5, @ixHandlingUnitType = @p6, @nHandlingUnitQuantity = @p7, @sSerialNumber = @p8, @sBatchNumber = @p9, @dtExpireAt = @p10, @nBaseUnitQuantityReceived = @p11, @ixInventoryState = @p12, @ixStatus = @p13, @UserName = @p14", tx_vw_receivingpost.ixReceipt, tx_vw_receivingpost.ixInventoryLocation, tx_vw_receivingpost.ixInboundOrder, tx_vw_receivingpost.ixHandlingUnit, tx_vw_receivingpost.ixMaterial, tx_vw_receivingpost.ixMaterialHandlingUnitConfiguration, tx_vw_receivingpost.ixHandlingUnitType, tx_vw_receivingpost.nHandlingUnitQuantity, tx_vw_receivingpost.sSerialNumber, tx_vw_receivingpost.sBatchNumber, tx_vw_receivingpost.dtExpireAt, tx_vw_receivingpost.nBaseUnitQuantityReceived, tx_vw_receivingpost.ixInventoryState, tx_vw_receivingpost.ixStatus, tx_vw_receivingpost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteReceiving @ixReceipt = @p0, @ixInventoryLocation = @p1, @ixInboundOrder = @p2, @ixHandlingUnit = @p3, @ixMaterial = @p4, @ixMaterialHandlingUnitConfiguration = @p5, @ixHandlingUnitType = @p6, @nHandlingUnitQuantity = @p7, @sSerialNumber = @p8, @sBatchNumber = @p9, @dtExpireAt = @p10, @nBaseUnitQuantityReceived = @p11, @ixInventoryState = @p12, @ixStatus = @p13, @UserName = @p14", tx_vw_receivingpost.ixReceipt, tx_vw_receivingpost.ixInventoryLocation, tx_vw_receivingpost.ixInboundOrder, tx_vw_receivingpost.ixHandlingUnit, tx_vw_receivingpost.ixMaterial, tx_vw_receivingpost.ixMaterialHandlingUnitConfiguration, tx_vw_receivingpost.ixHandlingUnitType, tx_vw_receivingpost.nHandlingUnitQuantity, tx_vw_receivingpost.sSerialNumber, tx_vw_receivingpost.sBatchNumber, tx_vw_receivingpost.dtExpireAt, tx_vw_receivingpost.nBaseUnitQuantityReceived, tx_vw_receivingpost.ixInventoryState, tx_vw_receivingpost.ixStatus, tx_vw_receivingpost.UserName);
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
  

