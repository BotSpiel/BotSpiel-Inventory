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

    public class FacilityAisleFacesDB : DbContext
    {

        public FacilityAisleFacesDB(DbContextOptions<FacilityAisleFacesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Universes> Universes { get; set; }
		public DbSet<UniversesPost> UniversesPost { get; set; }
		public DbSet<Galaxies> Galaxies { get; set; }
		public DbSet<GalaxiesPost> GalaxiesPost { get; set; }
		public DbSet<PlanetarySystems> PlanetarySystems { get; set; }
		public DbSet<PlanetarySystemsPost> PlanetarySystemsPost { get; set; }
		public DbSet<Planets> Planets { get; set; }
		public DbSet<PlanetsPost> PlanetsPost { get; set; }
		public DbSet<PlanetRegions> PlanetRegions { get; set; }
		public DbSet<PlanetRegionsPost> PlanetRegionsPost { get; set; }
		public DbSet<PlanetSubRegions> PlanetSubRegions { get; set; }
		public DbSet<PlanetSubRegionsPost> PlanetSubRegionsPost { get; set; }
		public DbSet<Countries> Countries { get; set; }
		public DbSet<CountriesPost> CountriesPost { get; set; }
		public DbSet<CountrySubDivisions> CountrySubDivisions { get; set; }
		public DbSet<CountrySubDivisionsPost> CountrySubDivisionsPost { get; set; }
		public DbSet<LocationFunctions> LocationFunctions { get; set; }
		public DbSet<LocationFunctionsPost> LocationFunctionsPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
		public DbSet<Addresses> Addresses { get; set; }
		public DbSet<AddressesPost> AddressesPost { get; set; }
		public DbSet<Facilities> Facilities { get; set; }
		public DbSet<FacilitiesPost> FacilitiesPost { get; set; }
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
            modelBuilder.Entity<Universes>()
                .ToTable("md_vw_Universes")
                .HasKey(c => new { c.ixUniverse });
            modelBuilder.Entity<UniversesPost>()
                .ToTable("md_vw_UniversesPost")
                .HasKey(c => new { c.ixUniverse });
            modelBuilder.Entity<Galaxies>()
                .ToTable("md_vw_Galaxies")
                .HasKey(c => new { c.ixGalaxy });
            modelBuilder.Entity<GalaxiesPost>()
                .ToTable("md_vw_GalaxiesPost")
                .HasKey(c => new { c.ixGalaxy });
            modelBuilder.Entity<PlanetarySystems>()
                .ToTable("md_vw_PlanetarySystems")
                .HasKey(c => new { c.ixPlanetarySystem });
            modelBuilder.Entity<PlanetarySystemsPost>()
                .ToTable("md_vw_PlanetarySystemsPost")
                .HasKey(c => new { c.ixPlanetarySystem });
            modelBuilder.Entity<Planets>()
                .ToTable("md_vw_Planets")
                .HasKey(c => new { c.ixPlanet });
            modelBuilder.Entity<PlanetsPost>()
                .ToTable("md_vw_PlanetsPost")
                .HasKey(c => new { c.ixPlanet });
            modelBuilder.Entity<PlanetRegions>()
                .ToTable("md_vw_PlanetRegions")
                .HasKey(c => new { c.ixPlanetRegion });
            modelBuilder.Entity<PlanetRegionsPost>()
                .ToTable("md_vw_PlanetRegionsPost")
                .HasKey(c => new { c.ixPlanetRegion });
            modelBuilder.Entity<PlanetSubRegions>()
                .ToTable("md_vw_PlanetSubRegions")
                .HasKey(c => new { c.ixPlanetSubRegion });
            modelBuilder.Entity<PlanetSubRegionsPost>()
                .ToTable("md_vw_PlanetSubRegionsPost")
                .HasKey(c => new { c.ixPlanetSubRegion });
            modelBuilder.Entity<Countries>()
                .ToTable("md_vw_Countries")
                .HasKey(c => new { c.ixCountry });
            modelBuilder.Entity<CountriesPost>()
                .ToTable("md_vw_CountriesPost")
                .HasKey(c => new { c.ixCountry });
            modelBuilder.Entity<CountrySubDivisions>()
                .ToTable("md_vw_CountrySubDivisions")
                .HasKey(c => new { c.ixCountrySubDivision });
            modelBuilder.Entity<CountrySubDivisionsPost>()
                .ToTable("md_vw_CountrySubDivisionsPost")
                .HasKey(c => new { c.ixCountrySubDivision });
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
            modelBuilder.Entity<Addresses>()
                .ToTable("md_vw_Addresses")
                .HasKey(c => new { c.ixAddress });
            modelBuilder.Entity<AddressesPost>()
                .ToTable("md_vw_AddressesPost")
                .HasKey(c => new { c.ixAddress });
            modelBuilder.Entity<Facilities>()
                .ToTable("md_vw_Facilities")
                .HasKey(c => new { c.ixFacility });
            modelBuilder.Entity<FacilitiesPost>()
                .ToTable("md_vw_FacilitiesPost")
                .HasKey(c => new { c.ixFacility });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is FacilityAisleFacesPost)).ToList())
            {
                var md_vw_facilityaislefacespost = e.Entity as FacilityAisleFacesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sFacilityAisleFace = cmd.CreateParameter();
                            sFacilityAisleFace.ParameterName = "p0";
                            sFacilityAisleFace.Value = md_vw_facilityaislefacespost.sFacilityAisleFace;
                            var ixFacility = cmd.CreateParameter();
                            ixFacility.ParameterName = "p1";
                            ixFacility.Value = md_vw_facilityaislefacespost.ixFacility;
                            var ixFacilityFloor = cmd.CreateParameter();
                            ixFacilityFloor.ParameterName = "p2";
                            ixFacilityFloor.Value = md_vw_facilityaislefacespost.ixFacilityFloor;
                            var nSequence = cmd.CreateParameter();
                            nSequence.ParameterName = "p3";
                            nSequence.Value = md_vw_facilityaislefacespost.nSequence;
                            var ixBaySequenceType = cmd.CreateParameter();
                            ixBaySequenceType.ParameterName = "p4";
                            ixBaySequenceType.Value = md_vw_facilityaislefacespost.ixBaySequenceType;
                            var ixPairedAisleFace = cmd.CreateParameter();
                            ixPairedAisleFace.ParameterName = "p5";
                            ixPairedAisleFace.Value = md_vw_facilityaislefacespost.ixPairedAisleFace;
                            var ixLogicalOrientation = cmd.CreateParameter();
                            ixLogicalOrientation.ParameterName = "p6";
                            ixLogicalOrientation.Value = md_vw_facilityaislefacespost.ixLogicalOrientation;
                            var ixAisleFaceStorageType = cmd.CreateParameter();
                            ixAisleFaceStorageType.ParameterName = "p7";
                            ixAisleFaceStorageType.Value = md_vw_facilityaislefacespost.ixAisleFaceStorageType;
                            var nXOffset = cmd.CreateParameter();
                            nXOffset.ParameterName = "p8";
                            nXOffset.Value = md_vw_facilityaislefacespost.nXOffset;
                            var ixXOffsetUnit = cmd.CreateParameter();
                            ixXOffsetUnit.ParameterName = "p9";
                            ixXOffsetUnit.Value = md_vw_facilityaislefacespost.ixXOffsetUnit;
                            var nYOffset = cmd.CreateParameter();
                            nYOffset.ParameterName = "p10";
                            nYOffset.Value = md_vw_facilityaislefacespost.nYOffset;
                            var ixYOffsetUnit = cmd.CreateParameter();
                            ixYOffsetUnit.ParameterName = "p11";
                            ixYOffsetUnit.Value = md_vw_facilityaislefacespost.ixYOffsetUnit;
                            var nLevels = cmd.CreateParameter();
                            nLevels.ParameterName = "p12";
                            nLevels.Value = md_vw_facilityaislefacespost.nLevels;
                            var nDefaultNumberOfBays = cmd.CreateParameter();
                            nDefaultNumberOfBays.ParameterName = "p13";
                            nDefaultNumberOfBays.Value = md_vw_facilityaislefacespost.nDefaultNumberOfBays;
                            var nDefaultNumberOfSlotsInBay = cmd.CreateParameter();
                            nDefaultNumberOfSlotsInBay.ParameterName = "p14";
                            nDefaultNumberOfSlotsInBay.Value = md_vw_facilityaislefacespost.nDefaultNumberOfSlotsInBay;
                            var ixDefaultFacilityZone = cmd.CreateParameter();
                            ixDefaultFacilityZone.ParameterName = "p15";
                            ixDefaultFacilityZone.Value = md_vw_facilityaislefacespost.ixDefaultFacilityZone;
                            var ixDefaultLocationFunction = cmd.CreateParameter();
                            ixDefaultLocationFunction.ParameterName = "p16";
                            ixDefaultLocationFunction.Value = md_vw_facilityaislefacespost.ixDefaultLocationFunction;
                            var ixDefaultInventoryLocationSize = cmd.CreateParameter();
                            ixDefaultInventoryLocationSize.ParameterName = "p17";
                            ixDefaultInventoryLocationSize.Value = md_vw_facilityaislefacespost.ixDefaultInventoryLocationSize;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p18";
                            UserName.Value = md_vw_facilityaislefacespost.UserName;

                            var ixFacilityAisleFace = cmd.CreateParameter();
                            ixFacilityAisleFace.ParameterName = "p19";
                            ixFacilityAisleFace.DbType = DbType.Int64;
                            ixFacilityAisleFace.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateFacilityAisleFaces ");
                            sql.Append("@sFacilityAisleFace = @p0, ");
                            sql.Append("@ixFacility = @p1, ");
                            sql.Append("@ixFacilityFloor = @p2, ");
                            sql.Append("@nSequence = @p3, ");
                            sql.Append("@ixBaySequenceType = @p4, ");
                            if (md_vw_facilityaislefacespost.ixPairedAisleFace != null) { sql.Append("@ixPairedAisleFace = @p5, "); }  
                            sql.Append("@ixLogicalOrientation = @p6, ");
                            sql.Append("@ixAisleFaceStorageType = @p7, ");
                            if (md_vw_facilityaislefacespost.nXOffset != null) { sql.Append("@nXOffset = @p8, "); }  
                            if (md_vw_facilityaislefacespost.ixXOffsetUnit != null) { sql.Append("@ixXOffsetUnit = @p9, "); }  
                            if (md_vw_facilityaislefacespost.nYOffset != null) { sql.Append("@nYOffset = @p10, "); }  
                            if (md_vw_facilityaislefacespost.ixYOffsetUnit != null) { sql.Append("@ixYOffsetUnit = @p11, "); }  
                            sql.Append("@nLevels = @p12, ");
                            if (md_vw_facilityaislefacespost.nDefaultNumberOfBays != null) { sql.Append("@nDefaultNumberOfBays = @p13, "); }  
                            if (md_vw_facilityaislefacespost.nDefaultNumberOfSlotsInBay != null) { sql.Append("@nDefaultNumberOfSlotsInBay = @p14, "); }  
                            if (md_vw_facilityaislefacespost.ixDefaultFacilityZone != null) { sql.Append("@ixDefaultFacilityZone = @p15, "); }  
                            if (md_vw_facilityaislefacespost.ixDefaultLocationFunction != null) { sql.Append("@ixDefaultLocationFunction = @p16, "); }  
                            sql.Append("@ixDefaultInventoryLocationSize = @p17, ");
                            if (md_vw_facilityaislefacespost.UserName != null) { sql.Append("@UserName = @p18, "); }  
                            sql.Append("@ixFacilityAisleFace = @p19 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sFacilityAisleFace);
                            cmd.Parameters.Add(ixFacility);
                            cmd.Parameters.Add(ixFacilityFloor);
                            cmd.Parameters.Add(nSequence);
                            cmd.Parameters.Add(ixBaySequenceType);
                            if (md_vw_facilityaislefacespost.ixPairedAisleFace != null) { cmd.Parameters.Add(ixPairedAisleFace); }
                            cmd.Parameters.Add(ixLogicalOrientation);
                            cmd.Parameters.Add(ixAisleFaceStorageType);
                            if (md_vw_facilityaislefacespost.nXOffset != null) { cmd.Parameters.Add(nXOffset); }
                            if (md_vw_facilityaislefacespost.ixXOffsetUnit != null) { cmd.Parameters.Add(ixXOffsetUnit); }
                            if (md_vw_facilityaislefacespost.nYOffset != null) { cmd.Parameters.Add(nYOffset); }
                            if (md_vw_facilityaislefacespost.ixYOffsetUnit != null) { cmd.Parameters.Add(ixYOffsetUnit); }
                            cmd.Parameters.Add(nLevels);
                            if (md_vw_facilityaislefacespost.nDefaultNumberOfBays != null) { cmd.Parameters.Add(nDefaultNumberOfBays); }
                            if (md_vw_facilityaislefacespost.nDefaultNumberOfSlotsInBay != null) { cmd.Parameters.Add(nDefaultNumberOfSlotsInBay); }
                            if (md_vw_facilityaislefacespost.ixDefaultFacilityZone != null) { cmd.Parameters.Add(ixDefaultFacilityZone); }
                            if (md_vw_facilityaislefacespost.ixDefaultLocationFunction != null) { cmd.Parameters.Add(ixDefaultLocationFunction); }
                            cmd.Parameters.Add(ixDefaultInventoryLocationSize);
                            if (md_vw_facilityaislefacespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixFacilityAisleFace); 
                            cmd.ExecuteNonQuery();
                            md_vw_facilityaislefacespost.ixFacilityAisleFace = (Int64)ixFacilityAisleFace.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixFacilityAisleFace"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeFacilityAisleFaces @ixFacilityAisleFace = @p0, @sFacilityAisleFace = @p1, @ixFacility = @p2, @ixFacilityFloor = @p3, @nSequence = @p4, @ixBaySequenceType = @p5, @ixPairedAisleFace = @p6, @ixLogicalOrientation = @p7, @ixAisleFaceStorageType = @p8, @nXOffset = @p9, @ixXOffsetUnit = @p10, @nYOffset = @p11, @ixYOffsetUnit = @p12, @nLevels = @p13, @nDefaultNumberOfBays = @p14, @nDefaultNumberOfSlotsInBay = @p15, @ixDefaultFacilityZone = @p16, @ixDefaultLocationFunction = @p17, @ixDefaultInventoryLocationSize = @p18, @UserName = @p19", md_vw_facilityaislefacespost.ixFacilityAisleFace, md_vw_facilityaislefacespost.sFacilityAisleFace, md_vw_facilityaislefacespost.ixFacility, md_vw_facilityaislefacespost.ixFacilityFloor, md_vw_facilityaislefacespost.nSequence, md_vw_facilityaislefacespost.ixBaySequenceType, md_vw_facilityaislefacespost.ixPairedAisleFace, md_vw_facilityaislefacespost.ixLogicalOrientation, md_vw_facilityaislefacespost.ixAisleFaceStorageType, md_vw_facilityaislefacespost.nXOffset, md_vw_facilityaislefacespost.ixXOffsetUnit, md_vw_facilityaislefacespost.nYOffset, md_vw_facilityaislefacespost.ixYOffsetUnit, md_vw_facilityaislefacespost.nLevels, md_vw_facilityaislefacespost.nDefaultNumberOfBays, md_vw_facilityaislefacespost.nDefaultNumberOfSlotsInBay, md_vw_facilityaislefacespost.ixDefaultFacilityZone, md_vw_facilityaislefacespost.ixDefaultLocationFunction, md_vw_facilityaislefacespost.ixDefaultInventoryLocationSize, md_vw_facilityaislefacespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteFacilityAisleFaces @ixFacilityAisleFace = @p0, @sFacilityAisleFace = @p1, @ixFacility = @p2, @ixFacilityFloor = @p3, @nSequence = @p4, @ixBaySequenceType = @p5, @ixPairedAisleFace = @p6, @ixLogicalOrientation = @p7, @ixAisleFaceStorageType = @p8, @nXOffset = @p9, @ixXOffsetUnit = @p10, @nYOffset = @p11, @ixYOffsetUnit = @p12, @nLevels = @p13, @nDefaultNumberOfBays = @p14, @nDefaultNumberOfSlotsInBay = @p15, @ixDefaultFacilityZone = @p16, @ixDefaultLocationFunction = @p17, @ixDefaultInventoryLocationSize = @p18, @UserName = @p19", md_vw_facilityaislefacespost.ixFacilityAisleFace, md_vw_facilityaislefacespost.sFacilityAisleFace, md_vw_facilityaislefacespost.ixFacility, md_vw_facilityaislefacespost.ixFacilityFloor, md_vw_facilityaislefacespost.nSequence, md_vw_facilityaislefacespost.ixBaySequenceType, md_vw_facilityaislefacespost.ixPairedAisleFace, md_vw_facilityaislefacespost.ixLogicalOrientation, md_vw_facilityaislefacespost.ixAisleFaceStorageType, md_vw_facilityaislefacespost.nXOffset, md_vw_facilityaislefacespost.ixXOffsetUnit, md_vw_facilityaislefacespost.nYOffset, md_vw_facilityaislefacespost.ixYOffsetUnit, md_vw_facilityaislefacespost.nLevels, md_vw_facilityaislefacespost.nDefaultNumberOfBays, md_vw_facilityaislefacespost.nDefaultNumberOfSlotsInBay, md_vw_facilityaislefacespost.ixDefaultFacilityZone, md_vw_facilityaislefacespost.ixDefaultLocationFunction, md_vw_facilityaislefacespost.ixDefaultInventoryLocationSize, md_vw_facilityaislefacespost.UserName);
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
  

