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

    public class InventoryUnitsDB : DbContext
    {

        public InventoryUnitsDB(DbContextOptions<InventoryUnitsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InventoryUnits> InventoryUnits { get; set; }
		public DbSet<InventoryUnitsPost> InventoryUnitsPost { get; set; }
		public DbSet<Facilities> Facilities { get; set; }
		public DbSet<FacilitiesPost> FacilitiesPost { get; set; }
		public DbSet<InventoryStates> InventoryStates { get; set; }
		public DbSet<InventoryStatesPost> InventoryStatesPost { get; set; }
		public DbSet<InventoryLocations> InventoryLocations { get; set; }
		public DbSet<InventoryLocationsPost> InventoryLocationsPost { get; set; }
		public DbSet<Addresses> Addresses { get; set; }
		public DbSet<AddressesPost> AddressesPost { get; set; }
		public DbSet<Companies> Companies { get; set; }
		public DbSet<CompaniesPost> CompaniesPost { get; set; }
		public DbSet<FacilityWorkAreas> FacilityWorkAreas { get; set; }
		public DbSet<FacilityWorkAreasPost> FacilityWorkAreasPost { get; set; }
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
            modelBuilder.Entity<InventoryUnits>()
                .ToTable("tx_vw_InventoryUnits")
                .HasKey(c => new { c.ixInventoryUnit });
            modelBuilder.Entity<InventoryUnitsPost>()
                .ToTable("tx_vw_InventoryUnitsPost")
                .HasKey(c => new { c.ixInventoryUnit });
            modelBuilder.Entity<Facilities>()
                .ToTable("md_vw_Facilities")
                .HasKey(c => new { c.ixFacility });
            modelBuilder.Entity<FacilitiesPost>()
                .ToTable("md_vw_FacilitiesPost")
                .HasKey(c => new { c.ixFacility });
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
            modelBuilder.Entity<FacilityWorkAreas>()
                .ToTable("md_vw_FacilityWorkAreas")
                .HasKey(c => new { c.ixFacilityWorkArea });
            modelBuilder.Entity<FacilityWorkAreasPost>()
                .ToTable("md_vw_FacilityWorkAreasPost")
                .HasKey(c => new { c.ixFacilityWorkArea });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is InventoryUnitsPost)).ToList())
            {
                var tx_vw_inventoryunitspost = e.Entity as InventoryUnitsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixFacility = cmd.CreateParameter();
                            ixFacility.ParameterName = "p0";
                            ixFacility.Value = tx_vw_inventoryunitspost.ixFacility;
                            var ixCompany = cmd.CreateParameter();
                            ixCompany.ParameterName = "p1";
                            ixCompany.Value = tx_vw_inventoryunitspost.ixCompany;
                            var ixMaterial = cmd.CreateParameter();
                            ixMaterial.ParameterName = "p2";
                            ixMaterial.Value = tx_vw_inventoryunitspost.ixMaterial;
                            var ixInventoryState = cmd.CreateParameter();
                            ixInventoryState.ParameterName = "p3";
                            ixInventoryState.Value = tx_vw_inventoryunitspost.ixInventoryState;
                            var ixHandlingUnit = cmd.CreateParameter();
                            ixHandlingUnit.ParameterName = "p4";
                            ixHandlingUnit.Value = tx_vw_inventoryunitspost.ixHandlingUnit;
                            var ixInventoryLocation = cmd.CreateParameter();
                            ixInventoryLocation.ParameterName = "p5";
                            ixInventoryLocation.Value = tx_vw_inventoryunitspost.ixInventoryLocation;
                            var nBaseUnitQuantity = cmd.CreateParameter();
                            nBaseUnitQuantity.ParameterName = "p6";
                            nBaseUnitQuantity.Value = tx_vw_inventoryunitspost.nBaseUnitQuantity;
                            var sSerialNumber = cmd.CreateParameter();
                            sSerialNumber.ParameterName = "p7";
                            sSerialNumber.Value = tx_vw_inventoryunitspost.sSerialNumber;
                            var sBatchNumber = cmd.CreateParameter();
                            sBatchNumber.ParameterName = "p8";
                            sBatchNumber.Value = tx_vw_inventoryunitspost.sBatchNumber;
                            var dtExpireAt = cmd.CreateParameter();
                            dtExpireAt.ParameterName = "p9";
                            dtExpireAt.Value = tx_vw_inventoryunitspost.dtExpireAt;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p10";
                            ixStatus.Value = tx_vw_inventoryunitspost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p11";
                            UserName.Value = tx_vw_inventoryunitspost.UserName;

                            var ixInventoryUnit = cmd.CreateParameter();
                            ixInventoryUnit.ParameterName = "p12";
                            ixInventoryUnit.DbType = DbType.Int64;
                            ixInventoryUnit.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateInventoryUnits ");
                            sql.Append("@ixFacility = @p0, ");
                            sql.Append("@ixCompany = @p1, ");
                            sql.Append("@ixMaterial = @p2, ");
                            sql.Append("@ixInventoryState = @p3, ");
                            if (tx_vw_inventoryunitspost.ixHandlingUnit != null) { sql.Append("@ixHandlingUnit = @p4, "); }  
                            sql.Append("@ixInventoryLocation = @p5, ");
                            sql.Append("@nBaseUnitQuantity = @p6, ");
                            if (tx_vw_inventoryunitspost.sSerialNumber != null) { sql.Append("@sSerialNumber = @p7, "); }  
                            if (tx_vw_inventoryunitspost.sBatchNumber != null) { sql.Append("@sBatchNumber = @p8, "); }  
                            if (tx_vw_inventoryunitspost.dtExpireAt != null) { sql.Append("@dtExpireAt = @p9, "); }  
                            sql.Append("@ixStatus = @p10, ");
                            if (tx_vw_inventoryunitspost.UserName != null) { sql.Append("@UserName = @p11, "); }  
                            sql.Append("@ixInventoryUnit = @p12 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixFacility);
                            cmd.Parameters.Add(ixCompany);
                            cmd.Parameters.Add(ixMaterial);
                            cmd.Parameters.Add(ixInventoryState);
                            if (tx_vw_inventoryunitspost.ixHandlingUnit != null) { cmd.Parameters.Add(ixHandlingUnit); }
                            cmd.Parameters.Add(ixInventoryLocation);
                            cmd.Parameters.Add(nBaseUnitQuantity);
                            if (tx_vw_inventoryunitspost.sSerialNumber != null) { cmd.Parameters.Add(sSerialNumber); }
                            if (tx_vw_inventoryunitspost.sBatchNumber != null) { cmd.Parameters.Add(sBatchNumber); }
                            if (tx_vw_inventoryunitspost.dtExpireAt != null) { cmd.Parameters.Add(dtExpireAt); }
                            cmd.Parameters.Add(ixStatus);
                            if (tx_vw_inventoryunitspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInventoryUnit); 
                            cmd.ExecuteNonQuery();
                            tx_vw_inventoryunitspost.ixInventoryUnit = (Int64)ixInventoryUnit.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInventoryUnit"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeInventoryUnits @ixInventoryUnit = @p0, @ixFacility = @p1, @ixCompany = @p2, @ixMaterial = @p3, @ixInventoryState = @p4, @ixHandlingUnit = @p5, @ixInventoryLocation = @p6, @nBaseUnitQuantity = @p7, @sSerialNumber = @p8, @sBatchNumber = @p9, @dtExpireAt = @p10, @ixStatus = @p11, @UserName = @p12", tx_vw_inventoryunitspost.ixInventoryUnit, tx_vw_inventoryunitspost.ixFacility, tx_vw_inventoryunitspost.ixCompany, tx_vw_inventoryunitspost.ixMaterial, tx_vw_inventoryunitspost.ixInventoryState, tx_vw_inventoryunitspost.ixHandlingUnit, tx_vw_inventoryunitspost.ixInventoryLocation, tx_vw_inventoryunitspost.nBaseUnitQuantity, tx_vw_inventoryunitspost.sSerialNumber, tx_vw_inventoryunitspost.sBatchNumber, tx_vw_inventoryunitspost.dtExpireAt, tx_vw_inventoryunitspost.ixStatus, tx_vw_inventoryunitspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteInventoryUnits @ixInventoryUnit = @p0, @ixFacility = @p1, @ixCompany = @p2, @ixMaterial = @p3, @ixInventoryState = @p4, @ixHandlingUnit = @p5, @ixInventoryLocation = @p6, @nBaseUnitQuantity = @p7, @sSerialNumber = @p8, @sBatchNumber = @p9, @dtExpireAt = @p10, @ixStatus = @p11, @UserName = @p12", tx_vw_inventoryunitspost.ixInventoryUnit, tx_vw_inventoryunitspost.ixFacility, tx_vw_inventoryunitspost.ixCompany, tx_vw_inventoryunitspost.ixMaterial, tx_vw_inventoryunitspost.ixInventoryState, tx_vw_inventoryunitspost.ixHandlingUnit, tx_vw_inventoryunitspost.ixInventoryLocation, tx_vw_inventoryunitspost.nBaseUnitQuantity, tx_vw_inventoryunitspost.sSerialNumber, tx_vw_inventoryunitspost.sBatchNumber, tx_vw_inventoryunitspost.dtExpireAt, tx_vw_inventoryunitspost.ixStatus, tx_vw_inventoryunitspost.UserName);
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
  

