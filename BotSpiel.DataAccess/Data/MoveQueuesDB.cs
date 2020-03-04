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

    public class MoveQueuesDB : DbContext
    {

        public MoveQueuesDB(DbContextOptions<MoveQueuesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<MoveQueues> MoveQueues { get; set; }
		public DbSet<MoveQueuesPost> MoveQueuesPost { get; set; }
		public DbSet<InventoryUnits> InventoryUnits { get; set; }
		public DbSet<InventoryUnitsPost> InventoryUnitsPost { get; set; }
		public DbSet<MoveQueueContexts> MoveQueueContexts { get; set; }
		public DbSet<MoveQueueContextsPost> MoveQueueContextsPost { get; set; }
		public DbSet<MoveQueueTypes> MoveQueueTypes { get; set; }
		public DbSet<MoveQueueTypesPost> MoveQueueTypesPost { get; set; }
		public DbSet<InboundOrderLines> InboundOrderLines { get; set; }
		public DbSet<InboundOrderLinesPost> InboundOrderLinesPost { get; set; }
		public DbSet<OutboundOrderLines> OutboundOrderLines { get; set; }
		public DbSet<OutboundOrderLinesPost> OutboundOrderLinesPost { get; set; }
		public DbSet<InventoryStates> InventoryStates { get; set; }
		public DbSet<InventoryStatesPost> InventoryStatesPost { get; set; }
		public DbSet<InboundOrders> InboundOrders { get; set; }
		public DbSet<InboundOrdersPost> InboundOrdersPost { get; set; }
		public DbSet<OutboundOrders> OutboundOrders { get; set; }
		public DbSet<OutboundOrdersPost> OutboundOrdersPost { get; set; }
		public DbSet<BusinessPartners> BusinessPartners { get; set; }
		public DbSet<BusinessPartnersPost> BusinessPartnersPost { get; set; }
		public DbSet<InboundOrderTypes> InboundOrderTypes { get; set; }
		public DbSet<InboundOrderTypesPost> InboundOrderTypesPost { get; set; }
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
            modelBuilder.Entity<MoveQueues>()
                .ToTable("tx_vw_MoveQueues")
                .HasKey(c => new { c.ixMoveQueue });
            modelBuilder.Entity<MoveQueuesPost>()
                .ToTable("tx_vw_MoveQueuesPost")
                .HasKey(c => new { c.ixMoveQueue });
            modelBuilder.Entity<InventoryUnits>()
                .ToTable("tx_vw_InventoryUnits")
                .HasKey(c => new { c.ixInventoryUnit });
            modelBuilder.Entity<InventoryUnitsPost>()
                .ToTable("tx_vw_InventoryUnitsPost")
                .HasKey(c => new { c.ixInventoryUnit });
            modelBuilder.Entity<MoveQueueContexts>()
                .ToTable("config_vw_MoveQueueContexts")
                .HasKey(c => new { c.ixMoveQueueContext });
            modelBuilder.Entity<MoveQueueContextsPost>()
                .ToTable("config_vw_MoveQueueContextsPost")
                .HasKey(c => new { c.ixMoveQueueContext });
            modelBuilder.Entity<MoveQueueTypes>()
                .ToTable("config_vw_MoveQueueTypes")
                .HasKey(c => new { c.ixMoveQueueType });
            modelBuilder.Entity<MoveQueueTypesPost>()
                .ToTable("config_vw_MoveQueueTypesPost")
                .HasKey(c => new { c.ixMoveQueueType });
            modelBuilder.Entity<InboundOrderLines>()
                .ToTable("tx_vw_InboundOrderLines")
                .HasKey(c => new { c.ixInboundOrderLine });
            modelBuilder.Entity<InboundOrderLinesPost>()
                .ToTable("tx_vw_InboundOrderLinesPost")
                .HasKey(c => new { c.ixInboundOrderLine });
            modelBuilder.Entity<OutboundOrderLines>()
                .ToTable("tx_vw_OutboundOrderLines")
                .HasKey(c => new { c.ixOutboundOrderLine });
            modelBuilder.Entity<OutboundOrderLinesPost>()
                .ToTable("tx_vw_OutboundOrderLinesPost")
                .HasKey(c => new { c.ixOutboundOrderLine });
            modelBuilder.Entity<InventoryStates>()
                .ToTable("config_vw_InventoryStates")
                .HasKey(c => new { c.ixInventoryState });
            modelBuilder.Entity<InventoryStatesPost>()
                .ToTable("config_vw_InventoryStatesPost")
                .HasKey(c => new { c.ixInventoryState });
            modelBuilder.Entity<InboundOrders>()
                .ToTable("tx_vw_InboundOrders")
                .HasKey(c => new { c.ixInboundOrder });
            modelBuilder.Entity<InboundOrdersPost>()
                .ToTable("tx_vw_InboundOrdersPost")
                .HasKey(c => new { c.ixInboundOrder });
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
            modelBuilder.Entity<InboundOrderTypes>()
                .ToTable("config_vw_InboundOrderTypes")
                .HasKey(c => new { c.ixInboundOrderType });
            modelBuilder.Entity<InboundOrderTypesPost>()
                .ToTable("config_vw_InboundOrderTypesPost")
                .HasKey(c => new { c.ixInboundOrderType });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is MoveQueuesPost)).ToList())
            {
                var tx_vw_movequeuespost = e.Entity as MoveQueuesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixMoveQueueType = cmd.CreateParameter();
                            ixMoveQueueType.ParameterName = "p0";
                            ixMoveQueueType.Value = tx_vw_movequeuespost.ixMoveQueueType;
                            var ixMoveQueueContext = cmd.CreateParameter();
                            ixMoveQueueContext.ParameterName = "p1";
                            ixMoveQueueContext.Value = tx_vw_movequeuespost.ixMoveQueueContext;
                            var ixSourceInventoryUnit = cmd.CreateParameter();
                            ixSourceInventoryUnit.ParameterName = "p2";
                            ixSourceInventoryUnit.Value = tx_vw_movequeuespost.ixSourceInventoryUnit;
                            var ixTargetInventoryUnit = cmd.CreateParameter();
                            ixTargetInventoryUnit.ParameterName = "p3";
                            ixTargetInventoryUnit.Value = tx_vw_movequeuespost.ixTargetInventoryUnit;
                            var ixSourceInventoryLocation = cmd.CreateParameter();
                            ixSourceInventoryLocation.ParameterName = "p4";
                            ixSourceInventoryLocation.Value = tx_vw_movequeuespost.ixSourceInventoryLocation;
                            var ixTargetInventoryLocation = cmd.CreateParameter();
                            ixTargetInventoryLocation.ParameterName = "p5";
                            ixTargetInventoryLocation.Value = tx_vw_movequeuespost.ixTargetInventoryLocation;
                            var ixSourceHandlingUnit = cmd.CreateParameter();
                            ixSourceHandlingUnit.ParameterName = "p6";
                            ixSourceHandlingUnit.Value = tx_vw_movequeuespost.ixSourceHandlingUnit;
                            var ixTargetHandlingUnit = cmd.CreateParameter();
                            ixTargetHandlingUnit.ParameterName = "p7";
                            ixTargetHandlingUnit.Value = tx_vw_movequeuespost.ixTargetHandlingUnit;
                            var sPreferredResource = cmd.CreateParameter();
                            sPreferredResource.ParameterName = "p8";
                            sPreferredResource.Value = tx_vw_movequeuespost.sPreferredResource;
                            var nBaseUnitQuantity = cmd.CreateParameter();
                            nBaseUnitQuantity.ParameterName = "p9";
                            nBaseUnitQuantity.Value = tx_vw_movequeuespost.nBaseUnitQuantity;
                            var dtStartBy = cmd.CreateParameter();
                            dtStartBy.ParameterName = "p10";
                            dtStartBy.Value = tx_vw_movequeuespost.dtStartBy;
                            var dtCompleteBy = cmd.CreateParameter();
                            dtCompleteBy.ParameterName = "p11";
                            dtCompleteBy.Value = tx_vw_movequeuespost.dtCompleteBy;
                            var dtStartedAt = cmd.CreateParameter();
                            dtStartedAt.ParameterName = "p12";
                            dtStartedAt.Value = tx_vw_movequeuespost.dtStartedAt;
                            var dtCompletedAt = cmd.CreateParameter();
                            dtCompletedAt.ParameterName = "p13";
                            dtCompletedAt.Value = tx_vw_movequeuespost.dtCompletedAt;
                            var ixInboundOrderLine = cmd.CreateParameter();
                            ixInboundOrderLine.ParameterName = "p14";
                            ixInboundOrderLine.Value = tx_vw_movequeuespost.ixInboundOrderLine;
                            var ixOutboundOrderLine = cmd.CreateParameter();
                            ixOutboundOrderLine.ParameterName = "p15";
                            ixOutboundOrderLine.Value = tx_vw_movequeuespost.ixOutboundOrderLine;
                            var ixPickBatch = cmd.CreateParameter();
                            ixPickBatch.ParameterName = "p16";
                            ixPickBatch.Value = tx_vw_movequeuespost.ixPickBatch;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p17";
                            ixStatus.Value = tx_vw_movequeuespost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p18";
                            UserName.Value = tx_vw_movequeuespost.UserName;

                            var ixMoveQueue = cmd.CreateParameter();
                            ixMoveQueue.ParameterName = "p19";
                            ixMoveQueue.DbType = DbType.Int64;
                            ixMoveQueue.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateMoveQueues ");
                            sql.Append("@ixMoveQueueType = @p0, ");
                            sql.Append("@ixMoveQueueContext = @p1, ");
                            if (tx_vw_movequeuespost.ixSourceInventoryUnit != null) { sql.Append("@ixSourceInventoryUnit = @p2, "); }  
                            if (tx_vw_movequeuespost.ixTargetInventoryUnit != null) { sql.Append("@ixTargetInventoryUnit = @p3, "); }  
                            if (tx_vw_movequeuespost.ixSourceInventoryLocation != null) { sql.Append("@ixSourceInventoryLocation = @p4, "); }  
                            if (tx_vw_movequeuespost.ixTargetInventoryLocation != null) { sql.Append("@ixTargetInventoryLocation = @p5, "); }  
                            if (tx_vw_movequeuespost.ixSourceHandlingUnit != null) { sql.Append("@ixSourceHandlingUnit = @p6, "); }  
                            if (tx_vw_movequeuespost.ixTargetHandlingUnit != null) { sql.Append("@ixTargetHandlingUnit = @p7, "); }  
                            if (tx_vw_movequeuespost.sPreferredResource != null) { sql.Append("@sPreferredResource = @p8, "); }  
                            sql.Append("@nBaseUnitQuantity = @p9, ");
                            if (tx_vw_movequeuespost.dtStartBy != null) { sql.Append("@dtStartBy = @p10, "); }  
                            if (tx_vw_movequeuespost.dtCompleteBy != null) { sql.Append("@dtCompleteBy = @p11, "); }  
                            if (tx_vw_movequeuespost.dtStartedAt != null) { sql.Append("@dtStartedAt = @p12, "); }  
                            if (tx_vw_movequeuespost.dtCompletedAt != null) { sql.Append("@dtCompletedAt = @p13, "); }  
                            if (tx_vw_movequeuespost.ixInboundOrderLine != null) { sql.Append("@ixInboundOrderLine = @p14, "); }  
                            if (tx_vw_movequeuespost.ixOutboundOrderLine != null) { sql.Append("@ixOutboundOrderLine = @p15, "); }  
                            if (tx_vw_movequeuespost.ixPickBatch != null) { sql.Append("@ixPickBatch = @p16, "); }  
                            sql.Append("@ixStatus = @p17, ");
                            if (tx_vw_movequeuespost.UserName != null) { sql.Append("@UserName = @p18, "); }  
                            sql.Append("@ixMoveQueue = @p19 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixMoveQueueType);
                            cmd.Parameters.Add(ixMoveQueueContext);
                            if (tx_vw_movequeuespost.ixSourceInventoryUnit != null) { cmd.Parameters.Add(ixSourceInventoryUnit); }
                            if (tx_vw_movequeuespost.ixTargetInventoryUnit != null) { cmd.Parameters.Add(ixTargetInventoryUnit); }
                            if (tx_vw_movequeuespost.ixSourceInventoryLocation != null) { cmd.Parameters.Add(ixSourceInventoryLocation); }
                            if (tx_vw_movequeuespost.ixTargetInventoryLocation != null) { cmd.Parameters.Add(ixTargetInventoryLocation); }
                            if (tx_vw_movequeuespost.ixSourceHandlingUnit != null) { cmd.Parameters.Add(ixSourceHandlingUnit); }
                            if (tx_vw_movequeuespost.ixTargetHandlingUnit != null) { cmd.Parameters.Add(ixTargetHandlingUnit); }
                            if (tx_vw_movequeuespost.sPreferredResource != null) { cmd.Parameters.Add(sPreferredResource); }
                            cmd.Parameters.Add(nBaseUnitQuantity);
                            if (tx_vw_movequeuespost.dtStartBy != null) { cmd.Parameters.Add(dtStartBy); }
                            if (tx_vw_movequeuespost.dtCompleteBy != null) { cmd.Parameters.Add(dtCompleteBy); }
                            if (tx_vw_movequeuespost.dtStartedAt != null) { cmd.Parameters.Add(dtStartedAt); }
                            if (tx_vw_movequeuespost.dtCompletedAt != null) { cmd.Parameters.Add(dtCompletedAt); }
                            if (tx_vw_movequeuespost.ixInboundOrderLine != null) { cmd.Parameters.Add(ixInboundOrderLine); }
                            if (tx_vw_movequeuespost.ixOutboundOrderLine != null) { cmd.Parameters.Add(ixOutboundOrderLine); }
                            if (tx_vw_movequeuespost.ixPickBatch != null) { cmd.Parameters.Add(ixPickBatch); }
                            cmd.Parameters.Add(ixStatus);
                            if (tx_vw_movequeuespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixMoveQueue); 
                            cmd.ExecuteNonQuery();
                            tx_vw_movequeuespost.ixMoveQueue = (Int64)ixMoveQueue.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixMoveQueue"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeMoveQueues @ixMoveQueue = @p0, @ixMoveQueueType = @p1, @ixMoveQueueContext = @p2, @ixSourceInventoryUnit = @p3, @ixTargetInventoryUnit = @p4, @ixSourceInventoryLocation = @p5, @ixTargetInventoryLocation = @p6, @ixSourceHandlingUnit = @p7, @ixTargetHandlingUnit = @p8, @sPreferredResource = @p9, @nBaseUnitQuantity = @p10, @dtStartBy = @p11, @dtCompleteBy = @p12, @dtStartedAt = @p13, @dtCompletedAt = @p14, @ixInboundOrderLine = @p15, @ixOutboundOrderLine = @p16, @ixPickBatch = @p17, @ixStatus = @p18, @UserName = @p19", tx_vw_movequeuespost.ixMoveQueue, tx_vw_movequeuespost.ixMoveQueueType, tx_vw_movequeuespost.ixMoveQueueContext, tx_vw_movequeuespost.ixSourceInventoryUnit, tx_vw_movequeuespost.ixTargetInventoryUnit, tx_vw_movequeuespost.ixSourceInventoryLocation, tx_vw_movequeuespost.ixTargetInventoryLocation, tx_vw_movequeuespost.ixSourceHandlingUnit, tx_vw_movequeuespost.ixTargetHandlingUnit, tx_vw_movequeuespost.sPreferredResource, tx_vw_movequeuespost.nBaseUnitQuantity, tx_vw_movequeuespost.dtStartBy, tx_vw_movequeuespost.dtCompleteBy, tx_vw_movequeuespost.dtStartedAt, tx_vw_movequeuespost.dtCompletedAt, tx_vw_movequeuespost.ixInboundOrderLine, tx_vw_movequeuespost.ixOutboundOrderLine, tx_vw_movequeuespost.ixPickBatch, tx_vw_movequeuespost.ixStatus, tx_vw_movequeuespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteMoveQueues @ixMoveQueue = @p0, @ixMoveQueueType = @p1, @ixMoveQueueContext = @p2, @ixSourceInventoryUnit = @p3, @ixTargetInventoryUnit = @p4, @ixSourceInventoryLocation = @p5, @ixTargetInventoryLocation = @p6, @ixSourceHandlingUnit = @p7, @ixTargetHandlingUnit = @p8, @sPreferredResource = @p9, @nBaseUnitQuantity = @p10, @dtStartBy = @p11, @dtCompleteBy = @p12, @dtStartedAt = @p13, @dtCompletedAt = @p14, @ixInboundOrderLine = @p15, @ixOutboundOrderLine = @p16, @ixPickBatch = @p17, @ixStatus = @p18, @UserName = @p19", tx_vw_movequeuespost.ixMoveQueue, tx_vw_movequeuespost.ixMoveQueueType, tx_vw_movequeuespost.ixMoveQueueContext, tx_vw_movequeuespost.ixSourceInventoryUnit, tx_vw_movequeuespost.ixTargetInventoryUnit, tx_vw_movequeuespost.ixSourceInventoryLocation, tx_vw_movequeuespost.ixTargetInventoryLocation, tx_vw_movequeuespost.ixSourceHandlingUnit, tx_vw_movequeuespost.ixTargetHandlingUnit, tx_vw_movequeuespost.sPreferredResource, tx_vw_movequeuespost.nBaseUnitQuantity, tx_vw_movequeuespost.dtStartBy, tx_vw_movequeuespost.dtCompleteBy, tx_vw_movequeuespost.dtStartedAt, tx_vw_movequeuespost.dtCompletedAt, tx_vw_movequeuespost.ixInboundOrderLine, tx_vw_movequeuespost.ixOutboundOrderLine, tx_vw_movequeuespost.ixPickBatch, tx_vw_movequeuespost.ixStatus, tx_vw_movequeuespost.UserName);
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
  

