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

    public class InventoryLocationSizesDB : DbContext
    {

        public InventoryLocationSizesDB(DbContextOptions<InventoryLocationSizesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<InventoryLocationSizes> InventoryLocationSizes { get; set; }
		public DbSet<InventoryLocationSizesPost> InventoryLocationSizesPost { get; set; }
		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryLocationSizes>()
                .ToTable("md_vw_InventoryLocationSizes")
                .HasKey(c => new { c.ixInventoryLocationSize });
            modelBuilder.Entity<InventoryLocationSizesPost>()
                .ToTable("md_vw_InventoryLocationSizesPost")
                .HasKey(c => new { c.ixInventoryLocationSize });
            modelBuilder.Entity<UnitsOfMeasurement>()
                .ToTable("config_vw_UnitsOfMeasurement")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<UnitsOfMeasurementPost>()
                .ToTable("config_vw_UnitsOfMeasurementPost")
                .HasKey(c => new { c.ixUnitOfMeasurement });
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
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is InventoryLocationSizesPost)).ToList())
            {
                var md_vw_inventorylocationsizespost = e.Entity as InventoryLocationSizesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sInventoryLocationSize = cmd.CreateParameter();
                            sInventoryLocationSize.ParameterName = "p0";
                            sInventoryLocationSize.Value = md_vw_inventorylocationsizespost.sInventoryLocationSize;
                            var nLength = cmd.CreateParameter();
                            nLength.ParameterName = "p1";
                            nLength.Value = md_vw_inventorylocationsizespost.nLength;
                            var ixLengthUnit = cmd.CreateParameter();
                            ixLengthUnit.ParameterName = "p2";
                            ixLengthUnit.Value = md_vw_inventorylocationsizespost.ixLengthUnit;
                            var nWidth = cmd.CreateParameter();
                            nWidth.ParameterName = "p3";
                            nWidth.Value = md_vw_inventorylocationsizespost.nWidth;
                            var ixWidthUnit = cmd.CreateParameter();
                            ixWidthUnit.ParameterName = "p4";
                            ixWidthUnit.Value = md_vw_inventorylocationsizespost.ixWidthUnit;
                            var nHeight = cmd.CreateParameter();
                            nHeight.ParameterName = "p5";
                            nHeight.Value = md_vw_inventorylocationsizespost.nHeight;
                            var ixHeightUnit = cmd.CreateParameter();
                            ixHeightUnit.ParameterName = "p6";
                            ixHeightUnit.Value = md_vw_inventorylocationsizespost.ixHeightUnit;
                            var nUsableVolume = cmd.CreateParameter();
                            nUsableVolume.ParameterName = "p7";
                            nUsableVolume.Value = md_vw_inventorylocationsizespost.nUsableVolume;
                            var ixUsableVolumeUnit = cmd.CreateParameter();
                            ixUsableVolumeUnit.ParameterName = "p8";
                            ixUsableVolumeUnit.Value = md_vw_inventorylocationsizespost.ixUsableVolumeUnit;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p9";
                            UserName.Value = md_vw_inventorylocationsizespost.UserName;

                            var ixInventoryLocationSize = cmd.CreateParameter();
                            ixInventoryLocationSize.ParameterName = "p10";
                            ixInventoryLocationSize.DbType = DbType.Int64;
                            ixInventoryLocationSize.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateInventoryLocationSizes ");
                            sql.Append("@sInventoryLocationSize = @p0, ");
                            sql.Append("@nLength = @p1, ");
                            sql.Append("@ixLengthUnit = @p2, ");
                            sql.Append("@nWidth = @p3, ");
                            sql.Append("@ixWidthUnit = @p4, ");
                            sql.Append("@nHeight = @p5, ");
                            sql.Append("@ixHeightUnit = @p6, ");
                            sql.Append("@nUsableVolume = @p7, ");
                            sql.Append("@ixUsableVolumeUnit = @p8, ");
                            if (md_vw_inventorylocationsizespost.UserName != null) { sql.Append("@UserName = @p9, "); }  
                            sql.Append("@ixInventoryLocationSize = @p10 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sInventoryLocationSize);
                            cmd.Parameters.Add(nLength);
                            cmd.Parameters.Add(ixLengthUnit);
                            cmd.Parameters.Add(nWidth);
                            cmd.Parameters.Add(ixWidthUnit);
                            cmd.Parameters.Add(nHeight);
                            cmd.Parameters.Add(ixHeightUnit);
                            cmd.Parameters.Add(nUsableVolume);
                            cmd.Parameters.Add(ixUsableVolumeUnit);
                            if (md_vw_inventorylocationsizespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixInventoryLocationSize); 
                            cmd.ExecuteNonQuery();
                            md_vw_inventorylocationsizespost.ixInventoryLocationSize = (Int64)ixInventoryLocationSize.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixInventoryLocationSize"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeInventoryLocationSizes @ixInventoryLocationSize = @p0, @sInventoryLocationSize = @p1, @nLength = @p2, @ixLengthUnit = @p3, @nWidth = @p4, @ixWidthUnit = @p5, @nHeight = @p6, @ixHeightUnit = @p7, @nUsableVolume = @p8, @ixUsableVolumeUnit = @p9, @UserName = @p10", md_vw_inventorylocationsizespost.ixInventoryLocationSize, md_vw_inventorylocationsizespost.sInventoryLocationSize, md_vw_inventorylocationsizespost.nLength, md_vw_inventorylocationsizespost.ixLengthUnit, md_vw_inventorylocationsizespost.nWidth, md_vw_inventorylocationsizespost.ixWidthUnit, md_vw_inventorylocationsizespost.nHeight, md_vw_inventorylocationsizespost.ixHeightUnit, md_vw_inventorylocationsizespost.nUsableVolume, md_vw_inventorylocationsizespost.ixUsableVolumeUnit, md_vw_inventorylocationsizespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteInventoryLocationSizes @ixInventoryLocationSize = @p0, @sInventoryLocationSize = @p1, @nLength = @p2, @ixLengthUnit = @p3, @nWidth = @p4, @ixWidthUnit = @p5, @nHeight = @p6, @ixHeightUnit = @p7, @nUsableVolume = @p8, @ixUsableVolumeUnit = @p9, @UserName = @p10", md_vw_inventorylocationsizespost.ixInventoryLocationSize, md_vw_inventorylocationsizespost.sInventoryLocationSize, md_vw_inventorylocationsizespost.nLength, md_vw_inventorylocationsizespost.ixLengthUnit, md_vw_inventorylocationsizespost.nWidth, md_vw_inventorylocationsizespost.ixWidthUnit, md_vw_inventorylocationsizespost.nHeight, md_vw_inventorylocationsizespost.ixHeightUnit, md_vw_inventorylocationsizespost.nUsableVolume, md_vw_inventorylocationsizespost.ixUsableVolumeUnit, md_vw_inventorylocationsizespost.UserName);
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
  

