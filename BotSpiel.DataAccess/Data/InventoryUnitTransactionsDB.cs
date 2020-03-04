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

    public class InventoryUnitTransactionsDB : DbContext
    {

        public InventoryUnitTransactionsDB(DbContextOptions<InventoryUnitTransactionsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InventoryUnitTransactions> InventoryUnitTransactions { get; set; }
		public DbSet<InventoryUnitTransactionsPost> InventoryUnitTransactionsPost { get; set; }
		public DbSet<InventoryUnits> InventoryUnits { get; set; }
		public DbSet<InventoryUnitsPost> InventoryUnitsPost { get; set; }
		public DbSet<InventoryUnitTransactionContexts> InventoryUnitTransactionContexts { get; set; }
		public DbSet<InventoryUnitTransactionContextsPost> InventoryUnitTransactionContextsPost { get; set; }
		public DbSet<InventoryStates> InventoryStates { get; set; }
		public DbSet<InventoryStatesPost> InventoryStatesPost { get; set; }
		public DbSet<InventoryLocations> InventoryLocations { get; set; }
		public DbSet<InventoryLocationsPost> InventoryLocationsPost { get; set; }
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
            modelBuilder.Entity<InventoryUnitTransactions>()
                .ToTable("tx_vw_InventoryUnitTransactions")
                .HasKey(c => new { c.ixInventoryUnitTransaction });
            modelBuilder.Entity<InventoryUnitTransactionsPost>()
                .ToTable("tx_vw_InventoryUnitTransactionsPost")
                .HasKey(c => new { c.ixInventoryUnitTransaction });
            modelBuilder.Entity<InventoryUnits>()
                .ToTable("tx_vw_InventoryUnits")
                .HasKey(c => new { c.ixInventoryUnit });
            modelBuilder.Entity<InventoryUnitsPost>()
                .ToTable("tx_vw_InventoryUnitsPost")
                .HasKey(c => new { c.ixInventoryUnit });
            modelBuilder.Entity<InventoryUnitTransactionContexts>()
                .ToTable("config_vw_InventoryUnitTransactionContexts")
                .HasKey(c => new { c.ixInventoryUnitTransactionContext });
            modelBuilder.Entity<InventoryUnitTransactionContextsPost>()
                .ToTable("config_vw_InventoryUnitTransactionContextsPost")
                .HasKey(c => new { c.ixInventoryUnitTransactionContext });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is InventoryUnitTransactionsPost)).ToList())
            {
                var tx_vw_inventoryunittransactionspost = e.Entity as InventoryUnitTransactionsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixInventoryUnit = cmd.CreateParameter();
                            ixInventoryUnit.ParameterName = "p0";
                            ixInventoryUnit.Value = tx_vw_inventoryunittransactionspost.ixInventoryUnit;
                            var ixInventoryUnitTransactionContext = cmd.CreateParameter();
                            ixInventoryUnitTransactionContext.ParameterName = "p1";
                            ixInventoryUnitTransactionContext.Value = tx_vw_inventoryunittransactionspost.ixInventoryUnitTransactionContext;
                            var ixFacilityBefore = cmd.CreateParameter();
                            ixFacilityBefore.ParameterName = "p2";
                            ixFacilityBefore.Value = tx_vw_inventoryunittransactionspost.ixFacilityBefore;
                            var ixFacilityAfter = cmd.CreateParameter();
                            ixFacilityAfter.ParameterName = "p3";
                            ixFacilityAfter.Value = tx_vw_inventoryunittransactionspost.ixFacilityAfter;
                            var ixCompanyBefore = cmd.CreateParameter();
                            ixCompanyBefore.ParameterName = "p4";
                            ixCompanyBefore.Value = tx_vw_inventoryunittransactionspost.ixCompanyBefore;
                            var ixCompanyAfter = cmd.CreateParameter();
                            ixCompanyAfter.ParameterName = "p5";
                            ixCompanyAfter.Value = tx_vw_inventoryunittransactionspost.ixCompanyAfter;
                            var ixMaterialBefore = cmd.CreateParameter();
                            ixMaterialBefore.ParameterName = "p6";
                            ixMaterialBefore.Value = tx_vw_inventoryunittransactionspost.ixMaterialBefore;
                            var ixMaterialAfter = cmd.CreateParameter();
                            ixMaterialAfter.ParameterName = "p7";
                            ixMaterialAfter.Value = tx_vw_inventoryunittransactionspost.ixMaterialAfter;
                            var ixInventoryStateBefore = cmd.CreateParameter();
                            ixInventoryStateBefore.ParameterName = "p8";
                            ixInventoryStateBefore.Value = tx_vw_inventoryunittransactionspost.ixInventoryStateBefore;
                            var ixInventoryStateAfter = cmd.CreateParameter();
                            ixInventoryStateAfter.ParameterName = "p9";
                            ixInventoryStateAfter.Value = tx_vw_inventoryunittransactionspost.ixInventoryStateAfter;
                            var ixHandlingUnitBefore = cmd.CreateParameter();
                            ixHandlingUnitBefore.ParameterName = "p10";
                            ixHandlingUnitBefore.Value = tx_vw_inventoryunittransactionspost.ixHandlingUnitBefore;
                            var ixHandlingUnitAfter = cmd.CreateParameter();
                            ixHandlingUnitAfter.ParameterName = "p11";
                            ixHandlingUnitAfter.Value = tx_vw_inventoryunittransactionspost.ixHandlingUnitAfter;
                            var ixInventoryLocationBefore = cmd.CreateParameter();
                            ixInventoryLocationBefore.ParameterName = "p12";
                            ixInventoryLocationBefore.Value = tx_vw_inventoryunittransactionspost.ixInventoryLocationBefore;
                            var ixInventoryLocationAfter = cmd.CreateParameter();
                            ixInventoryLocationAfter.ParameterName = "p13";
                            ixInventoryLocationAfter.Value = tx_vw_inventoryunittransactionspost.ixInventoryLocationAfter;
                            var nBaseUnitQuantityBefore = cmd.CreateParameter();
                            nBaseUnitQuantityBefore.ParameterName = "p14";
                            nBaseUnitQuantityBefore.Value = tx_vw_inventoryunittransactionspost.nBaseUnitQuantityBefore;
                            var nBaseUnitQuantityAfter = cmd.CreateParameter();
                            nBaseUnitQuantityAfter.ParameterName = "p15";
                            nBaseUnitQuantityAfter.Value = tx_vw_inventoryunittransactionspost.nBaseUnitQuantityAfter;
                            var sSerialNumberBefore = cmd.CreateParameter();
                            sSerialNumberBefore.ParameterName = "p16";
                            sSerialNumberBefore.Value = tx_vw_inventoryunittransactionspost.sSerialNumberBefore;
                            var sSerialNumberAfter = cmd.CreateParameter();
                            sSerialNumberAfter.ParameterName = "p17";
                            sSerialNumberAfter.Value = tx_vw_inventoryunittransactionspost.sSerialNumberAfter;
                            var sBatchNumberBefore = cmd.CreateParameter();
                            sBatchNumberBefore.ParameterName = "p18";
                            sBatchNumberBefore.Value = tx_vw_inventoryunittransactionspost.sBatchNumberBefore;
                            var sBatchNumberAfter = cmd.CreateParameter();
                            sBatchNumberAfter.ParameterName = "p19";
                            sBatchNumberAfter.Value = tx_vw_inventoryunittransactionspost.sBatchNumberAfter;
                            var dtExpireAtBefore = cmd.CreateParameter();
                            dtExpireAtBefore.ParameterName = "p20";
                            dtExpireAtBefore.Value = tx_vw_inventoryunittransactionspost.dtExpireAtBefore;
                            var dtExpireAtAfter = cmd.CreateParameter();
                            dtExpireAtAfter.ParameterName = "p21";
                            dtExpireAtAfter.Value = tx_vw_inventoryunittransactionspost.dtExpireAtAfter;
                            var ixStatusBefore = cmd.CreateParameter();
                            ixStatusBefore.ParameterName = "p22";
                            ixStatusBefore.Value = tx_vw_inventoryunittransactionspost.ixStatusBefore;
                            var ixStatusAfter = cmd.CreateParameter();
                            ixStatusAfter.ParameterName = "p23";
                            ixStatusAfter.Value = tx_vw_inventoryunittransactionspost.ixStatusAfter;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p24";
                            UserName.Value = tx_vw_inventoryunittransactionspost.UserName;

                            var ixInventoryUnitTransaction = cmd.CreateParameter();
                            ixInventoryUnitTransaction.ParameterName = "p25";
                            ixInventoryUnitTransaction.DbType = DbType.Int64;
                            ixInventoryUnitTransaction.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateInventoryUnitTransactions ");
                            sql.Append("@ixInventoryUnit = @p0, ");
                            sql.Append("@ixInventoryUnitTransactionContext = @p1, ");
                            if (tx_vw_inventoryunittransactionspost.ixFacilityBefore != null) { sql.Append("@ixFacilityBefore = @p2, "); }  
                            sql.Append("@ixFacilityAfter = @p3, ");
                            if (tx_vw_inventoryunittransactionspost.ixCompanyBefore != null) { sql.Append("@ixCompanyBefore = @p4, "); }  
                            sql.Append("@ixCompanyAfter = @p5, ");
                            if (tx_vw_inventoryunittransactionspost.ixMaterialBefore != null) { sql.Append("@ixMaterialBefore = @p6, "); }  
                            sql.Append("@ixMaterialAfter = @p7, ");
                            if (tx_vw_inventoryunittransactionspost.ixInventoryStateBefore != null) { sql.Append("@ixInventoryStateBefore = @p8, "); }  
                            sql.Append("@ixInventoryStateAfter = @p9, ");
                            if (tx_vw_inventoryunittransactionspost.ixHandlingUnitBefore != null) { sql.Append("@ixHandlingUnitBefore = @p10, "); }  
                            if (tx_vw_inventoryunittransactionspost.ixHandlingUnitAfter != null) { sql.Append("@ixHandlingUnitAfter = @p11, "); }  
                            if (tx_vw_inventoryunittransactionspost.ixInventoryLocationBefore != null) { sql.Append("@ixInventoryLocationBefore = @p12, "); }  
                            sql.Append("@ixInventoryLocationAfter = @p13, ");
                            if (tx_vw_inventoryunittransactionspost.nBaseUnitQuantityBefore != null) { sql.Append("@nBaseUnitQuantityBefore = @p14, "); }  
                            sql.Append("@nBaseUnitQuantityAfter = @p15, ");
                            if (tx_vw_inventoryunittransactionspost.sSerialNumberBefore != null) { sql.Append("@sSerialNumberBefore = @p16, "); }  
                            if (tx_vw_inventoryunittransactionspost.sSerialNumberAfter != null) { sql.Append("@sSerialNumberAfter = @p17, "); }  
                            if (tx_vw_inventoryunittransactionspost.sBatchNumberBefore != null) { sql.Append("@sBatchNumberBefore = @p18, "); }  
                            if (tx_vw_inventoryunittransactionspost.sBatchNumberAfter != null) { sql.Append("@sBatchNumberAfter = @p19, "); }  
                            if (tx_vw_inventoryunittransactionspost.dtExpireAtBefore != null) { sql.Append("@dtExpireAtBefore = @p20, "); }  
                            if (tx_vw_inventoryunittransactionspost.dtExpireAtAfter != null) { sql.Append("@dtExpireAtAfter = @p21, "); }  
                            if (tx_vw_inventoryunittransactionspost.ixStatusBefore != null) { sql.Append("@ixStatusBefore = @p22, "); }  
                            sql.Append("@ixStatusAfter = @p23, ");
                            if (tx_vw_inventoryunittransactionspost.UserName != null) { sql.Append("@UserName = @p24, "); }  
                            sql.Append("@ixInventoryUnitTransaction = @p25 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixInventoryUnit);
                            cmd.Parameters.Add(ixInventoryUnitTransactionContext);
                            if (tx_vw_inventoryunittransactionspost.ixFacilityBefore != null) { cmd.Parameters.Add(ixFacilityBefore); }
                            cmd.Parameters.Add(ixFacilityAfter);
                            if (tx_vw_inventoryunittransactionspost.ixCompanyBefore != null) { cmd.Parameters.Add(ixCompanyBefore); }
                            cmd.Parameters.Add(ixCompanyAfter);
                            if (tx_vw_inventoryunittransactionspost.ixMaterialBefore != null) { cmd.Parameters.Add(ixMaterialBefore); }
                            cmd.Parameters.Add(ixMaterialAfter);
                            if (tx_vw_inventoryunittransactionspost.ixInventoryStateBefore != null) { cmd.Parameters.Add(ixInventoryStateBefore); }
                            cmd.Parameters.Add(ixInventoryStateAfter);
                            if (tx_vw_inventoryunittransactionspost.ixHandlingUnitBefore != null) { cmd.Parameters.Add(ixHandlingUnitBefore); }
                            if (tx_vw_inventoryunittransactionspost.ixHandlingUnitAfter != null) { cmd.Parameters.Add(ixHandlingUnitAfter); }
                            if (tx_vw_inventoryunittransactionspost.ixInventoryLocationBefore != null) { cmd.Parameters.Add(ixInventoryLocationBefore); }
                            cmd.Parameters.Add(ixInventoryLocationAfter);
                            if (tx_vw_inventoryunittransactionspost.nBaseUnitQuantityBefore != null) { cmd.Parameters.Add(nBaseUnitQuantityBefore); }
                            cmd.Parameters.Add(nBaseUnitQuantityAfter);
                            if (tx_vw_inventoryunittransactionspost.sSerialNumberBefore != null) { cmd.Parameters.Add(sSerialNumberBefore); }
                            if (tx_vw_inventoryunittransactionspost.sSerialNumberAfter != null) { cmd.Parameters.Add(sSerialNumberAfter); }
                            if (tx_vw_inventoryunittransactionspost.sBatchNumberBefore != null) { cmd.Parameters.Add(sBatchNumberBefore); }
                            if (tx_vw_inventoryunittransactionspost.sBatchNumberAfter != null) { cmd.Parameters.Add(sBatchNumberAfter); }
                            if (tx_vw_inventoryunittransactionspost.dtExpireAtBefore != null) { cmd.Parameters.Add(dtExpireAtBefore); }
                            if (tx_vw_inventoryunittransactionspost.dtExpireAtAfter != null) { cmd.Parameters.Add(dtExpireAtAfter); }
                            if (tx_vw_inventoryunittransactionspost.ixStatusBefore != null) { cmd.Parameters.Add(ixStatusBefore); }
                            cmd.Parameters.Add(ixStatusAfter);
                            if (tx_vw_inventoryunittransactionspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInventoryUnitTransaction); 
                            cmd.ExecuteNonQuery();
                            tx_vw_inventoryunittransactionspost.ixInventoryUnitTransaction = (Int64)ixInventoryUnitTransaction.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInventoryUnitTransaction"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeInventoryUnitTransactions @ixInventoryUnitTransaction = @p0, @ixInventoryUnit = @p1, @ixInventoryUnitTransactionContext = @p2, @ixFacilityBefore = @p3, @ixFacilityAfter = @p4, @ixCompanyBefore = @p5, @ixCompanyAfter = @p6, @ixMaterialBefore = @p7, @ixMaterialAfter = @p8, @ixInventoryStateBefore = @p9, @ixInventoryStateAfter = @p10, @ixHandlingUnitBefore = @p11, @ixHandlingUnitAfter = @p12, @ixInventoryLocationBefore = @p13, @ixInventoryLocationAfter = @p14, @nBaseUnitQuantityBefore = @p15, @nBaseUnitQuantityAfter = @p16, @sSerialNumberBefore = @p17, @sSerialNumberAfter = @p18, @sBatchNumberBefore = @p19, @sBatchNumberAfter = @p20, @dtExpireAtBefore = @p21, @dtExpireAtAfter = @p22, @ixStatusBefore = @p23, @ixStatusAfter = @p24, @UserName = @p25", tx_vw_inventoryunittransactionspost.ixInventoryUnitTransaction, tx_vw_inventoryunittransactionspost.ixInventoryUnit, tx_vw_inventoryunittransactionspost.ixInventoryUnitTransactionContext, tx_vw_inventoryunittransactionspost.ixFacilityBefore, tx_vw_inventoryunittransactionspost.ixFacilityAfter, tx_vw_inventoryunittransactionspost.ixCompanyBefore, tx_vw_inventoryunittransactionspost.ixCompanyAfter, tx_vw_inventoryunittransactionspost.ixMaterialBefore, tx_vw_inventoryunittransactionspost.ixMaterialAfter, tx_vw_inventoryunittransactionspost.ixInventoryStateBefore, tx_vw_inventoryunittransactionspost.ixInventoryStateAfter, tx_vw_inventoryunittransactionspost.ixHandlingUnitBefore, tx_vw_inventoryunittransactionspost.ixHandlingUnitAfter, tx_vw_inventoryunittransactionspost.ixInventoryLocationBefore, tx_vw_inventoryunittransactionspost.ixInventoryLocationAfter, tx_vw_inventoryunittransactionspost.nBaseUnitQuantityBefore, tx_vw_inventoryunittransactionspost.nBaseUnitQuantityAfter, tx_vw_inventoryunittransactionspost.sSerialNumberBefore, tx_vw_inventoryunittransactionspost.sSerialNumberAfter, tx_vw_inventoryunittransactionspost.sBatchNumberBefore, tx_vw_inventoryunittransactionspost.sBatchNumberAfter, tx_vw_inventoryunittransactionspost.dtExpireAtBefore, tx_vw_inventoryunittransactionspost.dtExpireAtAfter, tx_vw_inventoryunittransactionspost.ixStatusBefore, tx_vw_inventoryunittransactionspost.ixStatusAfter, tx_vw_inventoryunittransactionspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteInventoryUnitTransactions @ixInventoryUnitTransaction = @p0, @ixInventoryUnit = @p1, @ixInventoryUnitTransactionContext = @p2, @ixFacilityBefore = @p3, @ixFacilityAfter = @p4, @ixCompanyBefore = @p5, @ixCompanyAfter = @p6, @ixMaterialBefore = @p7, @ixMaterialAfter = @p8, @ixInventoryStateBefore = @p9, @ixInventoryStateAfter = @p10, @ixHandlingUnitBefore = @p11, @ixHandlingUnitAfter = @p12, @ixInventoryLocationBefore = @p13, @ixInventoryLocationAfter = @p14, @nBaseUnitQuantityBefore = @p15, @nBaseUnitQuantityAfter = @p16, @sSerialNumberBefore = @p17, @sSerialNumberAfter = @p18, @sBatchNumberBefore = @p19, @sBatchNumberAfter = @p20, @dtExpireAtBefore = @p21, @dtExpireAtAfter = @p22, @ixStatusBefore = @p23, @ixStatusAfter = @p24, @UserName = @p25", tx_vw_inventoryunittransactionspost.ixInventoryUnitTransaction, tx_vw_inventoryunittransactionspost.ixInventoryUnit, tx_vw_inventoryunittransactionspost.ixInventoryUnitTransactionContext, tx_vw_inventoryunittransactionspost.ixFacilityBefore, tx_vw_inventoryunittransactionspost.ixFacilityAfter, tx_vw_inventoryunittransactionspost.ixCompanyBefore, tx_vw_inventoryunittransactionspost.ixCompanyAfter, tx_vw_inventoryunittransactionspost.ixMaterialBefore, tx_vw_inventoryunittransactionspost.ixMaterialAfter, tx_vw_inventoryunittransactionspost.ixInventoryStateBefore, tx_vw_inventoryunittransactionspost.ixInventoryStateAfter, tx_vw_inventoryunittransactionspost.ixHandlingUnitBefore, tx_vw_inventoryunittransactionspost.ixHandlingUnitAfter, tx_vw_inventoryunittransactionspost.ixInventoryLocationBefore, tx_vw_inventoryunittransactionspost.ixInventoryLocationAfter, tx_vw_inventoryunittransactionspost.nBaseUnitQuantityBefore, tx_vw_inventoryunittransactionspost.nBaseUnitQuantityAfter, tx_vw_inventoryunittransactionspost.sSerialNumberBefore, tx_vw_inventoryunittransactionspost.sSerialNumberAfter, tx_vw_inventoryunittransactionspost.sBatchNumberBefore, tx_vw_inventoryunittransactionspost.sBatchNumberAfter, tx_vw_inventoryunittransactionspost.dtExpireAtBefore, tx_vw_inventoryunittransactionspost.dtExpireAtAfter, tx_vw_inventoryunittransactionspost.ixStatusBefore, tx_vw_inventoryunittransactionspost.ixStatusAfter, tx_vw_inventoryunittransactionspost.UserName);
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
  

