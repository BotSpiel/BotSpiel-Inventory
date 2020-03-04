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

    public class OutboundOrderLinePackingDB : DbContext
    {

        public OutboundOrderLinePackingDB(DbContextOptions<OutboundOrderLinePackingDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<OutboundOrderLinePacking> OutboundOrderLinePacking { get; set; }
		public DbSet<OutboundOrderLinePackingPost> OutboundOrderLinePackingPost { get; set; }
		public DbSet<OutboundOrderLines> OutboundOrderLines { get; set; }
		public DbSet<OutboundOrderLinesPost> OutboundOrderLinesPost { get; set; }
		public DbSet<OutboundOrders> OutboundOrders { get; set; }
		public DbSet<OutboundOrdersPost> OutboundOrdersPost { get; set; }
		public DbSet<BusinessPartners> BusinessPartners { get; set; }
		public DbSet<BusinessPartnersPost> BusinessPartnersPost { get; set; }
		public DbSet<OutboundOrderTypes> OutboundOrderTypes { get; set; }
		public DbSet<OutboundOrderTypesPost> OutboundOrderTypesPost { get; set; }
		public DbSet<CarrierServices> CarrierServices { get; set; }
		public DbSet<CarrierServicesPost> CarrierServicesPost { get; set; }
		public DbSet<OutboundShipments> OutboundShipments { get; set; }
		public DbSet<OutboundShipmentsPost> OutboundShipmentsPost { get; set; }
		public DbSet<PickBatches> PickBatches { get; set; }
		public DbSet<PickBatchesPost> PickBatchesPost { get; set; }
		public DbSet<BusinessPartnerTypes> BusinessPartnerTypes { get; set; }
		public DbSet<BusinessPartnerTypesPost> BusinessPartnerTypesPost { get; set; }
		public DbSet<OutboundCarrierManifests> OutboundCarrierManifests { get; set; }
		public DbSet<OutboundCarrierManifestsPost> OutboundCarrierManifestsPost { get; set; }
		public DbSet<PickBatchTypes> PickBatchTypes { get; set; }
		public DbSet<PickBatchTypesPost> PickBatchTypesPost { get; set; }
		public DbSet<InventoryLocations> InventoryLocations { get; set; }
		public DbSet<InventoryLocationsPost> InventoryLocationsPost { get; set; }
		public DbSet<Carriers> Carriers { get; set; }
		public DbSet<CarriersPost> CarriersPost { get; set; }
		public DbSet<Companies> Companies { get; set; }
		public DbSet<CompaniesPost> CompaniesPost { get; set; }
		public DbSet<Facilities> Facilities { get; set; }
		public DbSet<FacilitiesPost> FacilitiesPost { get; set; }
		public DbSet<FacilityWorkAreas> FacilityWorkAreas { get; set; }
		public DbSet<FacilityWorkAreasPost> FacilityWorkAreasPost { get; set; }
		public DbSet<CarrierTypes> CarrierTypes { get; set; }
		public DbSet<CarrierTypesPost> CarrierTypesPost { get; set; }
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
            modelBuilder.Entity<OutboundOrderLinePacking>()
                .ToTable("tx_vw_OutboundOrderLinePacking")
                .HasKey(c => new { c.ixOutboundOrderLinePack });
            modelBuilder.Entity<OutboundOrderLinePackingPost>()
                .ToTable("tx_vw_OutboundOrderLinePackingPost")
                .HasKey(c => new { c.ixOutboundOrderLinePack });
            modelBuilder.Entity<OutboundOrderLines>()
                .ToTable("tx_vw_OutboundOrderLines")
                .HasKey(c => new { c.ixOutboundOrderLine });
            modelBuilder.Entity<OutboundOrderLinesPost>()
                .ToTable("tx_vw_OutboundOrderLinesPost")
                .HasKey(c => new { c.ixOutboundOrderLine });
            modelBuilder.Entity<OutboundOrders>()
                .ToTable("tx_vw_OutboundOrders")
                .HasKey(c => new { c.ixOutboundOrder });
            modelBuilder.Entity<OutboundOrdersPost>()
                .ToTable("tx_vw_OutboundOrdersPost")
                .HasKey(c => new { c.ixOutboundOrder });
            modelBuilder.Entity<BusinessPartners>()
                .ToTable("md_vw_BusinessPartners")
                .HasKey(c => new { c.ixBusinessPartner });
            modelBuilder.Entity<BusinessPartnersPost>()
                .ToTable("md_vw_BusinessPartnersPost")
                .HasKey(c => new { c.ixBusinessPartner });
            modelBuilder.Entity<OutboundOrderTypes>()
                .ToTable("config_vw_OutboundOrderTypes")
                .HasKey(c => new { c.ixOutboundOrderType });
            modelBuilder.Entity<OutboundOrderTypesPost>()
                .ToTable("config_vw_OutboundOrderTypesPost")
                .HasKey(c => new { c.ixOutboundOrderType });
            modelBuilder.Entity<CarrierServices>()
                .ToTable("md_vw_CarrierServices")
                .HasKey(c => new { c.ixCarrierService });
            modelBuilder.Entity<CarrierServicesPost>()
                .ToTable("md_vw_CarrierServicesPost")
                .HasKey(c => new { c.ixCarrierService });
            modelBuilder.Entity<OutboundShipments>()
                .ToTable("tx_vw_OutboundShipments")
                .HasKey(c => new { c.ixOutboundShipment });
            modelBuilder.Entity<OutboundShipmentsPost>()
                .ToTable("tx_vw_OutboundShipmentsPost")
                .HasKey(c => new { c.ixOutboundShipment });
            modelBuilder.Entity<PickBatches>()
                .ToTable("tx_vw_PickBatches")
                .HasKey(c => new { c.ixPickBatch });
            modelBuilder.Entity<PickBatchesPost>()
                .ToTable("tx_vw_PickBatchesPost")
                .HasKey(c => new { c.ixPickBatch });
            modelBuilder.Entity<BusinessPartnerTypes>()
                .ToTable("config_vw_BusinessPartnerTypes")
                .HasKey(c => new { c.ixBusinessPartnerType });
            modelBuilder.Entity<BusinessPartnerTypesPost>()
                .ToTable("config_vw_BusinessPartnerTypesPost")
                .HasKey(c => new { c.ixBusinessPartnerType });
            modelBuilder.Entity<OutboundCarrierManifests>()
                .ToTable("tx_vw_OutboundCarrierManifests")
                .HasKey(c => new { c.ixOutboundCarrierManifest });
            modelBuilder.Entity<OutboundCarrierManifestsPost>()
                .ToTable("tx_vw_OutboundCarrierManifestsPost")
                .HasKey(c => new { c.ixOutboundCarrierManifest });
            modelBuilder.Entity<PickBatchTypes>()
                .ToTable("config_vw_PickBatchTypes")
                .HasKey(c => new { c.ixPickBatchType });
            modelBuilder.Entity<PickBatchTypesPost>()
                .ToTable("config_vw_PickBatchTypesPost")
                .HasKey(c => new { c.ixPickBatchType });
            modelBuilder.Entity<InventoryLocations>()
                .ToTable("md_vw_InventoryLocations")
                .HasKey(c => new { c.ixInventoryLocation });
            modelBuilder.Entity<InventoryLocationsPost>()
                .ToTable("md_vw_InventoryLocationsPost")
                .HasKey(c => new { c.ixInventoryLocation });
            modelBuilder.Entity<Carriers>()
                .ToTable("md_vw_Carriers")
                .HasKey(c => new { c.ixCarrier });
            modelBuilder.Entity<CarriersPost>()
                .ToTable("md_vw_CarriersPost")
                .HasKey(c => new { c.ixCarrier });
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
            modelBuilder.Entity<CarrierTypes>()
                .ToTable("config_vw_CarrierTypes")
                .HasKey(c => new { c.ixCarrierType });
            modelBuilder.Entity<CarrierTypesPost>()
                .ToTable("config_vw_CarrierTypesPost")
                .HasKey(c => new { c.ixCarrierType });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is OutboundOrderLinePackingPost)).ToList())
            {
                var tx_vw_outboundorderlinepackingpost = e.Entity as OutboundOrderLinePackingPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixOutboundOrderLine = cmd.CreateParameter();
                            ixOutboundOrderLine.ParameterName = "p0";
                            ixOutboundOrderLine.Value = tx_vw_outboundorderlinepackingpost.ixOutboundOrderLine;
                            var ixHandlingUnit = cmd.CreateParameter();
                            ixHandlingUnit.ParameterName = "p1";
                            ixHandlingUnit.Value = tx_vw_outboundorderlinepackingpost.ixHandlingUnit;
                            var nBaseUnitQuantityPacked = cmd.CreateParameter();
                            nBaseUnitQuantityPacked.ParameterName = "p2";
                            nBaseUnitQuantityPacked.Value = tx_vw_outboundorderlinepackingpost.nBaseUnitQuantityPacked;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p3";
                            ixStatus.Value = tx_vw_outboundorderlinepackingpost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p4";
                            UserName.Value = tx_vw_outboundorderlinepackingpost.UserName;

                            var ixOutboundOrderLinePack = cmd.CreateParameter();
                            ixOutboundOrderLinePack.ParameterName = "p5";
                            ixOutboundOrderLinePack.DbType = DbType.Int64;
                            ixOutboundOrderLinePack.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateOutboundOrderLinePacking ");
                            sql.Append("@ixOutboundOrderLine = @p0, ");
                            sql.Append("@ixHandlingUnit = @p1, ");
                            sql.Append("@nBaseUnitQuantityPacked = @p2, ");
                            sql.Append("@ixStatus = @p3, ");
                            if (tx_vw_outboundorderlinepackingpost.UserName != null) { sql.Append("@UserName = @p4, "); }  
                            sql.Append("@ixOutboundOrderLinePack = @p5 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixOutboundOrderLine);
                            cmd.Parameters.Add(ixHandlingUnit);
                            cmd.Parameters.Add(nBaseUnitQuantityPacked);
                            cmd.Parameters.Add(ixStatus);
                            if (tx_vw_outboundorderlinepackingpost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixOutboundOrderLinePack); 
                            cmd.ExecuteNonQuery();
                            tx_vw_outboundorderlinepackingpost.ixOutboundOrderLinePack = (Int64)ixOutboundOrderLinePack.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixOutboundOrderLinePack"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeOutboundOrderLinePacking @ixOutboundOrderLinePack = @p0, @ixOutboundOrderLine = @p1, @ixHandlingUnit = @p2, @nBaseUnitQuantityPacked = @p3, @ixStatus = @p4, @UserName = @p5", tx_vw_outboundorderlinepackingpost.ixOutboundOrderLinePack, tx_vw_outboundorderlinepackingpost.ixOutboundOrderLine, tx_vw_outboundorderlinepackingpost.ixHandlingUnit, tx_vw_outboundorderlinepackingpost.nBaseUnitQuantityPacked, tx_vw_outboundorderlinepackingpost.ixStatus, tx_vw_outboundorderlinepackingpost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteOutboundOrderLinePacking @ixOutboundOrderLinePack = @p0, @ixOutboundOrderLine = @p1, @ixHandlingUnit = @p2, @nBaseUnitQuantityPacked = @p3, @ixStatus = @p4, @UserName = @p5", tx_vw_outboundorderlinepackingpost.ixOutboundOrderLinePack, tx_vw_outboundorderlinepackingpost.ixOutboundOrderLine, tx_vw_outboundorderlinepackingpost.ixHandlingUnit, tx_vw_outboundorderlinepackingpost.nBaseUnitQuantityPacked, tx_vw_outboundorderlinepackingpost.ixStatus, tx_vw_outboundorderlinepackingpost.UserName);
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
  

