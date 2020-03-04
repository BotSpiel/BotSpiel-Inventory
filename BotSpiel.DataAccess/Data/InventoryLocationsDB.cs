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

    public class InventoryLocationsDB : DbContext
    {

        public InventoryLocationsDB(DbContextOptions<InventoryLocationsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is InventoryLocationsPost)).ToList())
            {
                var md_vw_inventorylocationspost = e.Entity as InventoryLocationsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sInventoryLocation = cmd.CreateParameter();
                            sInventoryLocation.ParameterName = "p0";
                            sInventoryLocation.Value = md_vw_inventorylocationspost.sInventoryLocation;
                            var ixLocationFunction = cmd.CreateParameter();
                            ixLocationFunction.ParameterName = "p1";
                            ixLocationFunction.Value = md_vw_inventorylocationspost.ixLocationFunction;
                            var ixFacility = cmd.CreateParameter();
                            ixFacility.ParameterName = "p2";
                            ixFacility.Value = md_vw_inventorylocationspost.ixFacility;
                            var ixCompany = cmd.CreateParameter();
                            ixCompany.ParameterName = "p3";
                            ixCompany.Value = md_vw_inventorylocationspost.ixCompany;
                            var ixFacilityFloor = cmd.CreateParameter();
                            ixFacilityFloor.ParameterName = "p4";
                            ixFacilityFloor.Value = md_vw_inventorylocationspost.ixFacilityFloor;
                            var ixFacilityZone = cmd.CreateParameter();
                            ixFacilityZone.ParameterName = "p5";
                            ixFacilityZone.Value = md_vw_inventorylocationspost.ixFacilityZone;
                            var ixFacilityWorkArea = cmd.CreateParameter();
                            ixFacilityWorkArea.ParameterName = "p6";
                            ixFacilityWorkArea.Value = md_vw_inventorylocationspost.ixFacilityWorkArea;
                            var ixFacilityAisleFace = cmd.CreateParameter();
                            ixFacilityAisleFace.ParameterName = "p7";
                            ixFacilityAisleFace.Value = md_vw_inventorylocationspost.ixFacilityAisleFace;
                            var sLevel = cmd.CreateParameter();
                            sLevel.ParameterName = "p8";
                            sLevel.Value = md_vw_inventorylocationspost.sLevel;
                            var sBay = cmd.CreateParameter();
                            sBay.ParameterName = "p9";
                            sBay.Value = md_vw_inventorylocationspost.sBay;
                            var sSlot = cmd.CreateParameter();
                            sSlot.ParameterName = "p10";
                            sSlot.Value = md_vw_inventorylocationspost.sSlot;
                            var ixInventoryLocationSize = cmd.CreateParameter();
                            ixInventoryLocationSize.ParameterName = "p11";
                            ixInventoryLocationSize.Value = md_vw_inventorylocationspost.ixInventoryLocationSize;
                            var nSequence = cmd.CreateParameter();
                            nSequence.ParameterName = "p12";
                            nSequence.Value = md_vw_inventorylocationspost.nSequence;
                            var nXOffset = cmd.CreateParameter();
                            nXOffset.ParameterName = "p13";
                            nXOffset.Value = md_vw_inventorylocationspost.nXOffset;
                            var ixXOffsetUnit = cmd.CreateParameter();
                            ixXOffsetUnit.ParameterName = "p14";
                            ixXOffsetUnit.Value = md_vw_inventorylocationspost.ixXOffsetUnit;
                            var nYOffset = cmd.CreateParameter();
                            nYOffset.ParameterName = "p15";
                            nYOffset.Value = md_vw_inventorylocationspost.nYOffset;
                            var ixYOffsetUnit = cmd.CreateParameter();
                            ixYOffsetUnit.ParameterName = "p16";
                            ixYOffsetUnit.Value = md_vw_inventorylocationspost.ixYOffsetUnit;
                            var nZOffset = cmd.CreateParameter();
                            nZOffset.ParameterName = "p17";
                            nZOffset.Value = md_vw_inventorylocationspost.nZOffset;
                            var ixZOffsetUnit = cmd.CreateParameter();
                            ixZOffsetUnit.ParameterName = "p18";
                            ixZOffsetUnit.Value = md_vw_inventorylocationspost.ixZOffsetUnit;
                            var sLatitude = cmd.CreateParameter();
                            sLatitude.ParameterName = "p19";
                            sLatitude.Value = md_vw_inventorylocationspost.sLatitude;
                            var sLongitude = cmd.CreateParameter();
                            sLongitude.ParameterName = "p20";
                            sLongitude.Value = md_vw_inventorylocationspost.sLongitude;
                            var bTrackUtilisation = cmd.CreateParameter();
                            bTrackUtilisation.ParameterName = "p21";
                            bTrackUtilisation.Value = md_vw_inventorylocationspost.bTrackUtilisation;
                            var nUtilisationPercent = cmd.CreateParameter();
                            nUtilisationPercent.ParameterName = "p22";
                            nUtilisationPercent.Value = md_vw_inventorylocationspost.nUtilisationPercent;
                            var nQueuedUtilisationPercent = cmd.CreateParameter();
                            nQueuedUtilisationPercent.ParameterName = "p23";
                            nQueuedUtilisationPercent.Value = md_vw_inventorylocationspost.nQueuedUtilisationPercent;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p24";
                            UserName.Value = md_vw_inventorylocationspost.UserName;

                            var ixInventoryLocation = cmd.CreateParameter();
                            ixInventoryLocation.ParameterName = "p25";
                            ixInventoryLocation.DbType = DbType.Int64;
                            ixInventoryLocation.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateInventoryLocations ");
                            sql.Append("@sInventoryLocation = @p0, ");
                            sql.Append("@ixLocationFunction = @p1, ");
                            sql.Append("@ixFacility = @p2, ");
                            if (md_vw_inventorylocationspost.ixCompany != null) { sql.Append("@ixCompany = @p3, "); }  
                            sql.Append("@ixFacilityFloor = @p4, ");
                            sql.Append("@ixFacilityZone = @p5, ");
                            sql.Append("@ixFacilityWorkArea = @p6, ");
                            sql.Append("@ixFacilityAisleFace = @p7, ");
                            if (md_vw_inventorylocationspost.sLevel != null) { sql.Append("@sLevel = @p8, "); }  
                            if (md_vw_inventorylocationspost.sBay != null) { sql.Append("@sBay = @p9, "); }  
                            if (md_vw_inventorylocationspost.sSlot != null) { sql.Append("@sSlot = @p10, "); }  
                            if (md_vw_inventorylocationspost.ixInventoryLocationSize != null) { sql.Append("@ixInventoryLocationSize = @p11, "); }  
                            sql.Append("@nSequence = @p12, ");
                            if (md_vw_inventorylocationspost.nXOffset != null) { sql.Append("@nXOffset = @p13, "); }  
                            if (md_vw_inventorylocationspost.ixXOffsetUnit != null) { sql.Append("@ixXOffsetUnit = @p14, "); }  
                            if (md_vw_inventorylocationspost.nYOffset != null) { sql.Append("@nYOffset = @p15, "); }  
                            if (md_vw_inventorylocationspost.ixYOffsetUnit != null) { sql.Append("@ixYOffsetUnit = @p16, "); }  
                            if (md_vw_inventorylocationspost.nZOffset != null) { sql.Append("@nZOffset = @p17, "); }  
                            if (md_vw_inventorylocationspost.ixZOffsetUnit != null) { sql.Append("@ixZOffsetUnit = @p18, "); }  
                            if (md_vw_inventorylocationspost.sLatitude != null) { sql.Append("@sLatitude = @p19, "); }  
                            if (md_vw_inventorylocationspost.sLongitude != null) { sql.Append("@sLongitude = @p20, "); }  
                            sql.Append("@bTrackUtilisation = @p21, ");
                            if (md_vw_inventorylocationspost.nUtilisationPercent != null) { sql.Append("@nUtilisationPercent = @p22, "); }  
                            if (md_vw_inventorylocationspost.nQueuedUtilisationPercent != null) { sql.Append("@nQueuedUtilisationPercent = @p23, "); }  
                            if (md_vw_inventorylocationspost.UserName != null) { sql.Append("@UserName = @p24, "); }  
                            sql.Append("@ixInventoryLocation = @p25 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sInventoryLocation);
                            cmd.Parameters.Add(ixLocationFunction);
                            cmd.Parameters.Add(ixFacility);
                            if (md_vw_inventorylocationspost.ixCompany != null) { cmd.Parameters.Add(ixCompany); }
                            cmd.Parameters.Add(ixFacilityFloor);
                            cmd.Parameters.Add(ixFacilityZone);
                            cmd.Parameters.Add(ixFacilityWorkArea);
                            cmd.Parameters.Add(ixFacilityAisleFace);
                            if (md_vw_inventorylocationspost.sLevel != null) { cmd.Parameters.Add(sLevel); }
                            if (md_vw_inventorylocationspost.sBay != null) { cmd.Parameters.Add(sBay); }
                            if (md_vw_inventorylocationspost.sSlot != null) { cmd.Parameters.Add(sSlot); }
                            if (md_vw_inventorylocationspost.ixInventoryLocationSize != null) { cmd.Parameters.Add(ixInventoryLocationSize); }
                            cmd.Parameters.Add(nSequence);
                            if (md_vw_inventorylocationspost.nXOffset != null) { cmd.Parameters.Add(nXOffset); }
                            if (md_vw_inventorylocationspost.ixXOffsetUnit != null) { cmd.Parameters.Add(ixXOffsetUnit); }
                            if (md_vw_inventorylocationspost.nYOffset != null) { cmd.Parameters.Add(nYOffset); }
                            if (md_vw_inventorylocationspost.ixYOffsetUnit != null) { cmd.Parameters.Add(ixYOffsetUnit); }
                            if (md_vw_inventorylocationspost.nZOffset != null) { cmd.Parameters.Add(nZOffset); }
                            if (md_vw_inventorylocationspost.ixZOffsetUnit != null) { cmd.Parameters.Add(ixZOffsetUnit); }
                            if (md_vw_inventorylocationspost.sLatitude != null) { cmd.Parameters.Add(sLatitude); }
                            if (md_vw_inventorylocationspost.sLongitude != null) { cmd.Parameters.Add(sLongitude); }
                            cmd.Parameters.Add(bTrackUtilisation);
                            if (md_vw_inventorylocationspost.nUtilisationPercent != null) { cmd.Parameters.Add(nUtilisationPercent); }
                            if (md_vw_inventorylocationspost.nQueuedUtilisationPercent != null) { cmd.Parameters.Add(nQueuedUtilisationPercent); }
                            if (md_vw_inventorylocationspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInventoryLocation); 
                            cmd.ExecuteNonQuery();
                            md_vw_inventorylocationspost.ixInventoryLocation = (Int64)ixInventoryLocation.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInventoryLocation"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeInventoryLocations @ixInventoryLocation = @p0, @sInventoryLocation = @p1, @ixLocationFunction = @p2, @ixFacility = @p3, @ixCompany = @p4, @ixFacilityFloor = @p5, @ixFacilityZone = @p6, @ixFacilityWorkArea = @p7, @ixFacilityAisleFace = @p8, @sLevel = @p9, @sBay = @p10, @sSlot = @p11, @ixInventoryLocationSize = @p12, @nSequence = @p13, @nXOffset = @p14, @ixXOffsetUnit = @p15, @nYOffset = @p16, @ixYOffsetUnit = @p17, @nZOffset = @p18, @ixZOffsetUnit = @p19, @sLatitude = @p20, @sLongitude = @p21, @bTrackUtilisation = @p22, @nUtilisationPercent = @p23, @nQueuedUtilisationPercent = @p24, @UserName = @p25", md_vw_inventorylocationspost.ixInventoryLocation, md_vw_inventorylocationspost.sInventoryLocation, md_vw_inventorylocationspost.ixLocationFunction, md_vw_inventorylocationspost.ixFacility, md_vw_inventorylocationspost.ixCompany, md_vw_inventorylocationspost.ixFacilityFloor, md_vw_inventorylocationspost.ixFacilityZone, md_vw_inventorylocationspost.ixFacilityWorkArea, md_vw_inventorylocationspost.ixFacilityAisleFace, md_vw_inventorylocationspost.sLevel, md_vw_inventorylocationspost.sBay, md_vw_inventorylocationspost.sSlot, md_vw_inventorylocationspost.ixInventoryLocationSize, md_vw_inventorylocationspost.nSequence, md_vw_inventorylocationspost.nXOffset, md_vw_inventorylocationspost.ixXOffsetUnit, md_vw_inventorylocationspost.nYOffset, md_vw_inventorylocationspost.ixYOffsetUnit, md_vw_inventorylocationspost.nZOffset, md_vw_inventorylocationspost.ixZOffsetUnit, md_vw_inventorylocationspost.sLatitude, md_vw_inventorylocationspost.sLongitude, md_vw_inventorylocationspost.bTrackUtilisation, md_vw_inventorylocationspost.nUtilisationPercent, md_vw_inventorylocationspost.nQueuedUtilisationPercent, md_vw_inventorylocationspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteInventoryLocations @ixInventoryLocation = @p0, @sInventoryLocation = @p1, @ixLocationFunction = @p2, @ixFacility = @p3, @ixCompany = @p4, @ixFacilityFloor = @p5, @ixFacilityZone = @p6, @ixFacilityWorkArea = @p7, @ixFacilityAisleFace = @p8, @sLevel = @p9, @sBay = @p10, @sSlot = @p11, @ixInventoryLocationSize = @p12, @nSequence = @p13, @nXOffset = @p14, @ixXOffsetUnit = @p15, @nYOffset = @p16, @ixYOffsetUnit = @p17, @nZOffset = @p18, @ixZOffsetUnit = @p19, @sLatitude = @p20, @sLongitude = @p21, @bTrackUtilisation = @p22, @nUtilisationPercent = @p23, @nQueuedUtilisationPercent = @p24, @UserName = @p25", md_vw_inventorylocationspost.ixInventoryLocation, md_vw_inventorylocationspost.sInventoryLocation, md_vw_inventorylocationspost.ixLocationFunction, md_vw_inventorylocationspost.ixFacility, md_vw_inventorylocationspost.ixCompany, md_vw_inventorylocationspost.ixFacilityFloor, md_vw_inventorylocationspost.ixFacilityZone, md_vw_inventorylocationspost.ixFacilityWorkArea, md_vw_inventorylocationspost.ixFacilityAisleFace, md_vw_inventorylocationspost.sLevel, md_vw_inventorylocationspost.sBay, md_vw_inventorylocationspost.sSlot, md_vw_inventorylocationspost.ixInventoryLocationSize, md_vw_inventorylocationspost.nSequence, md_vw_inventorylocationspost.nXOffset, md_vw_inventorylocationspost.ixXOffsetUnit, md_vw_inventorylocationspost.nYOffset, md_vw_inventorylocationspost.ixYOffsetUnit, md_vw_inventorylocationspost.nZOffset, md_vw_inventorylocationspost.ixZOffsetUnit, md_vw_inventorylocationspost.sLatitude, md_vw_inventorylocationspost.sLongitude, md_vw_inventorylocationspost.bTrackUtilisation, md_vw_inventorylocationspost.nUtilisationPercent, md_vw_inventorylocationspost.nQueuedUtilisationPercent, md_vw_inventorylocationspost.UserName);
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
  

