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

    public class AddressesDB : DbContext
    {

        public AddressesDB(DbContextOptions<AddressesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is AddressesPost)).ToList())
            {
                var md_vw_addressespost = e.Entity as AddressesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sStreetAndNumberOrPostOfficeBoxOne = cmd.CreateParameter();
                            sStreetAndNumberOrPostOfficeBoxOne.ParameterName = "p0";
                            sStreetAndNumberOrPostOfficeBoxOne.Value = md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxOne;
                            var sStreetAndNumberOrPostOfficeBoxTwo = cmd.CreateParameter();
                            sStreetAndNumberOrPostOfficeBoxTwo.ParameterName = "p1";
                            sStreetAndNumberOrPostOfficeBoxTwo.Value = md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxTwo;
                            var sStreetAndNumberOrPostOfficeBoxThree = cmd.CreateParameter();
                            sStreetAndNumberOrPostOfficeBoxThree.ParameterName = "p2";
                            sStreetAndNumberOrPostOfficeBoxThree.Value = md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxThree;
                            var sCityOrSuburb = cmd.CreateParameter();
                            sCityOrSuburb.ParameterName = "p3";
                            sCityOrSuburb.Value = md_vw_addressespost.sCityOrSuburb;
                            var sZipOrPostCode = cmd.CreateParameter();
                            sZipOrPostCode.ParameterName = "p4";
                            sZipOrPostCode.Value = md_vw_addressespost.sZipOrPostCode;
                            var ixStateOrProvince = cmd.CreateParameter();
                            ixStateOrProvince.ParameterName = "p5";
                            ixStateOrProvince.Value = md_vw_addressespost.ixStateOrProvince;
                            var ixCountry = cmd.CreateParameter();
                            ixCountry.ParameterName = "p6";
                            ixCountry.Value = md_vw_addressespost.ixCountry;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p7";
                            UserName.Value = md_vw_addressespost.UserName;

                            var ixAddress = cmd.CreateParameter();
                            ixAddress.ParameterName = "p8";
                            ixAddress.DbType = DbType.Int64;
                            ixAddress.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateAddresses ");
                            sql.Append("@sStreetAndNumberOrPostOfficeBoxOne = @p0, ");
                            if (md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxTwo != null) { sql.Append("@sStreetAndNumberOrPostOfficeBoxTwo = @p1, "); }  
                            if (md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxThree != null) { sql.Append("@sStreetAndNumberOrPostOfficeBoxThree = @p2, "); }  
                            sql.Append("@sCityOrSuburb = @p3, ");
                            sql.Append("@sZipOrPostCode = @p4, ");
                            sql.Append("@ixStateOrProvince = @p5, ");
                            sql.Append("@ixCountry = @p6, ");
                            if (md_vw_addressespost.UserName != null) { sql.Append("@UserName = @p7, "); }  
                            sql.Append("@ixAddress = @p8 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sStreetAndNumberOrPostOfficeBoxOne);
                            if (md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxTwo != null) { cmd.Parameters.Add(sStreetAndNumberOrPostOfficeBoxTwo); }
                            if (md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxThree != null) { cmd.Parameters.Add(sStreetAndNumberOrPostOfficeBoxThree); }
                            cmd.Parameters.Add(sCityOrSuburb);
                            cmd.Parameters.Add(sZipOrPostCode);
                            cmd.Parameters.Add(ixStateOrProvince);
                            cmd.Parameters.Add(ixCountry);
                            if (md_vw_addressespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixAddress); 
                            cmd.ExecuteNonQuery();
                            md_vw_addressespost.ixAddress = (Int64)ixAddress.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixAddress"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeAddresses @ixAddress = @p0, @sStreetAndNumberOrPostOfficeBoxOne = @p1, @sStreetAndNumberOrPostOfficeBoxTwo = @p2, @sStreetAndNumberOrPostOfficeBoxThree = @p3, @sCityOrSuburb = @p4, @sZipOrPostCode = @p5, @ixStateOrProvince = @p6, @ixCountry = @p7, @UserName = @p8", md_vw_addressespost.ixAddress, md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxOne, md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxTwo, md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxThree, md_vw_addressespost.sCityOrSuburb, md_vw_addressespost.sZipOrPostCode, md_vw_addressespost.ixStateOrProvince, md_vw_addressespost.ixCountry, md_vw_addressespost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteAddresses @ixAddress = @p0, @sStreetAndNumberOrPostOfficeBoxOne = @p1, @sStreetAndNumberOrPostOfficeBoxTwo = @p2, @sStreetAndNumberOrPostOfficeBoxThree = @p3, @sCityOrSuburb = @p4, @sZipOrPostCode = @p5, @ixStateOrProvince = @p6, @ixCountry = @p7, @UserName = @p8", md_vw_addressespost.ixAddress, md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxOne, md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxTwo, md_vw_addressespost.sStreetAndNumberOrPostOfficeBoxThree, md_vw_addressespost.sCityOrSuburb, md_vw_addressespost.sZipOrPostCode, md_vw_addressespost.ixStateOrProvince, md_vw_addressespost.ixCountry, md_vw_addressespost.UserName);
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
  

