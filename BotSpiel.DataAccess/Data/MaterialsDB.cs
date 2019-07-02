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

    public class MaterialsDB : DbContext
    {

        public MaterialsDB(DbContextOptions<MaterialsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Materials> Materials { get; set; }
		public DbSet<MaterialsPost> MaterialsPost { get; set; }
		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
		public DbSet<MaterialTypes> MaterialTypes { get; set; }
		public DbSet<MaterialTypesPost> MaterialTypesPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Materials>()
                .ToTable("md_vw_Materials")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<MaterialsPost>()
                .ToTable("md_vw_MaterialsPost")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<UnitsOfMeasurement>()
                .ToTable("config_vw_UnitsOfMeasurement")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<UnitsOfMeasurementPost>()
                .ToTable("config_vw_UnitsOfMeasurementPost")
                .HasKey(c => new { c.ixUnitOfMeasurement });
            modelBuilder.Entity<MaterialTypes>()
                .ToTable("config_vw_MaterialTypes")
                .HasKey(c => new { c.ixMaterialType });
            modelBuilder.Entity<MaterialTypesPost>()
                .ToTable("config_vw_MaterialTypesPost")
                .HasKey(c => new { c.ixMaterialType });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is MaterialsPost)).ToList())
            {
                var md_vw_materialspost = e.Entity as MaterialsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sMaterial = cmd.CreateParameter();
                            sMaterial.ParameterName = "p0";
                            sMaterial.Value = md_vw_materialspost.sMaterial;
                            var sDescription = cmd.CreateParameter();
                            sDescription.ParameterName = "p1";
                            sDescription.Value = md_vw_materialspost.sDescription;
                            var ixMaterialType = cmd.CreateParameter();
                            ixMaterialType.ParameterName = "p2";
                            ixMaterialType.Value = md_vw_materialspost.ixMaterialType;
                            var ixBaseUnit = cmd.CreateParameter();
                            ixBaseUnit.ParameterName = "p3";
                            ixBaseUnit.Value = md_vw_materialspost.ixBaseUnit;
                            var bTrackSerialNumber = cmd.CreateParameter();
                            bTrackSerialNumber.ParameterName = "p4";
                            bTrackSerialNumber.Value = md_vw_materialspost.bTrackSerialNumber;
                            var bTrackBatchNumber = cmd.CreateParameter();
                            bTrackBatchNumber.ParameterName = "p5";
                            bTrackBatchNumber.Value = md_vw_materialspost.bTrackBatchNumber;
                            var bTrackExpiry = cmd.CreateParameter();
                            bTrackExpiry.ParameterName = "p6";
                            bTrackExpiry.Value = md_vw_materialspost.bTrackExpiry;
                            var nDensity = cmd.CreateParameter();
                            nDensity.ParameterName = "p7";
                            nDensity.Value = md_vw_materialspost.nDensity;
                            var ixDensityUnit = cmd.CreateParameter();
                            ixDensityUnit.ParameterName = "p8";
                            ixDensityUnit.Value = md_vw_materialspost.ixDensityUnit;
                            var nShelflife = cmd.CreateParameter();
                            nShelflife.ParameterName = "p9";
                            nShelflife.Value = md_vw_materialspost.nShelflife;
                            var ixShelflifeUnit = cmd.CreateParameter();
                            ixShelflifeUnit.ParameterName = "p10";
                            ixShelflifeUnit.Value = md_vw_materialspost.ixShelflifeUnit;
                            var nLength = cmd.CreateParameter();
                            nLength.ParameterName = "p11";
                            nLength.Value = md_vw_materialspost.nLength;
                            var ixLengthUnit = cmd.CreateParameter();
                            ixLengthUnit.ParameterName = "p12";
                            ixLengthUnit.Value = md_vw_materialspost.ixLengthUnit;
                            var nWidth = cmd.CreateParameter();
                            nWidth.ParameterName = "p13";
                            nWidth.Value = md_vw_materialspost.nWidth;
                            var ixWidthUnit = cmd.CreateParameter();
                            ixWidthUnit.ParameterName = "p14";
                            ixWidthUnit.Value = md_vw_materialspost.ixWidthUnit;
                            var nHeight = cmd.CreateParameter();
                            nHeight.ParameterName = "p15";
                            nHeight.Value = md_vw_materialspost.nHeight;
                            var ixHeightUnit = cmd.CreateParameter();
                            ixHeightUnit.ParameterName = "p16";
                            ixHeightUnit.Value = md_vw_materialspost.ixHeightUnit;
                            var nWeight = cmd.CreateParameter();
                            nWeight.ParameterName = "p17";
                            nWeight.Value = md_vw_materialspost.nWeight;
                            var ixWeightUnit = cmd.CreateParameter();
                            ixWeightUnit.ParameterName = "p18";
                            ixWeightUnit.Value = md_vw_materialspost.ixWeightUnit;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p19";
                            UserName.Value = md_vw_materialspost.UserName;

                            var ixMaterial = cmd.CreateParameter();
                            ixMaterial.ParameterName = "p20";
                            ixMaterial.DbType = DbType.Int64;
                            ixMaterial.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateMaterials ");
                            sql.Append("@sMaterial = @p0, ");
                            sql.Append("@sDescription = @p1, ");
                            sql.Append("@ixMaterialType = @p2, ");
                            sql.Append("@ixBaseUnit = @p3, ");
                            sql.Append("@bTrackSerialNumber = @p4, ");
                            sql.Append("@bTrackBatchNumber = @p5, ");
                            sql.Append("@bTrackExpiry = @p6, ");
                            if (md_vw_materialspost.nDensity != null) { sql.Append("@nDensity = @p7, "); }  
                            if (md_vw_materialspost.ixDensityUnit != null) { sql.Append("@ixDensityUnit = @p8, "); }  
                            if (md_vw_materialspost.nShelflife != null) { sql.Append("@nShelflife = @p9, "); }  
                            if (md_vw_materialspost.ixShelflifeUnit != null) { sql.Append("@ixShelflifeUnit = @p10, "); }  
                            if (md_vw_materialspost.nLength != null) { sql.Append("@nLength = @p11, "); }  
                            if (md_vw_materialspost.ixLengthUnit != null) { sql.Append("@ixLengthUnit = @p12, "); }  
                            if (md_vw_materialspost.nWidth != null) { sql.Append("@nWidth = @p13, "); }  
                            if (md_vw_materialspost.ixWidthUnit != null) { sql.Append("@ixWidthUnit = @p14, "); }  
                            if (md_vw_materialspost.nHeight != null) { sql.Append("@nHeight = @p15, "); }  
                            if (md_vw_materialspost.ixHeightUnit != null) { sql.Append("@ixHeightUnit = @p16, "); }  
                            if (md_vw_materialspost.nWeight != null) { sql.Append("@nWeight = @p17, "); }  
                            if (md_vw_materialspost.ixWeightUnit != null) { sql.Append("@ixWeightUnit = @p18, "); }  
                            if (md_vw_materialspost.UserName != null) { sql.Append("@UserName = @p19, "); }  
                            sql.Append("@ixMaterial = @p20 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sMaterial);
                            cmd.Parameters.Add(sDescription);
                            cmd.Parameters.Add(ixMaterialType);
                            cmd.Parameters.Add(ixBaseUnit);
                            cmd.Parameters.Add(bTrackSerialNumber);
                            cmd.Parameters.Add(bTrackBatchNumber);
                            cmd.Parameters.Add(bTrackExpiry);
                            if (md_vw_materialspost.nDensity != null) { cmd.Parameters.Add(nDensity); }
                            if (md_vw_materialspost.ixDensityUnit != null) { cmd.Parameters.Add(ixDensityUnit); }
                            if (md_vw_materialspost.nShelflife != null) { cmd.Parameters.Add(nShelflife); }
                            if (md_vw_materialspost.ixShelflifeUnit != null) { cmd.Parameters.Add(ixShelflifeUnit); }
                            if (md_vw_materialspost.nLength != null) { cmd.Parameters.Add(nLength); }
                            if (md_vw_materialspost.ixLengthUnit != null) { cmd.Parameters.Add(ixLengthUnit); }
                            if (md_vw_materialspost.nWidth != null) { cmd.Parameters.Add(nWidth); }
                            if (md_vw_materialspost.ixWidthUnit != null) { cmd.Parameters.Add(ixWidthUnit); }
                            if (md_vw_materialspost.nHeight != null) { cmd.Parameters.Add(nHeight); }
                            if (md_vw_materialspost.ixHeightUnit != null) { cmd.Parameters.Add(ixHeightUnit); }
                            if (md_vw_materialspost.nWeight != null) { cmd.Parameters.Add(nWeight); }
                            if (md_vw_materialspost.ixWeightUnit != null) { cmd.Parameters.Add(ixWeightUnit); }
                            if (md_vw_materialspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixMaterial); 
                            cmd.ExecuteNonQuery();
                            md_vw_materialspost.ixMaterial = (Int64)ixMaterial.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixMaterial"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeMaterials @ixMaterial = @p0, @sMaterial = @p1, @sDescription = @p2, @ixMaterialType = @p3, @ixBaseUnit = @p4, @bTrackSerialNumber = @p5, @bTrackBatchNumber = @p6, @bTrackExpiry = @p7, @nDensity = @p8, @ixDensityUnit = @p9, @nShelflife = @p10, @ixShelflifeUnit = @p11, @nLength = @p12, @ixLengthUnit = @p13, @nWidth = @p14, @ixWidthUnit = @p15, @nHeight = @p16, @ixHeightUnit = @p17, @nWeight = @p18, @ixWeightUnit = @p19, @UserName = @p20", md_vw_materialspost.ixMaterial, md_vw_materialspost.sMaterial, md_vw_materialspost.sDescription, md_vw_materialspost.ixMaterialType, md_vw_materialspost.ixBaseUnit, md_vw_materialspost.bTrackSerialNumber, md_vw_materialspost.bTrackBatchNumber, md_vw_materialspost.bTrackExpiry, md_vw_materialspost.nDensity, md_vw_materialspost.ixDensityUnit, md_vw_materialspost.nShelflife, md_vw_materialspost.ixShelflifeUnit, md_vw_materialspost.nLength, md_vw_materialspost.ixLengthUnit, md_vw_materialspost.nWidth, md_vw_materialspost.ixWidthUnit, md_vw_materialspost.nHeight, md_vw_materialspost.ixHeightUnit, md_vw_materialspost.nWeight, md_vw_materialspost.ixWeightUnit, md_vw_materialspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteMaterials @ixMaterial = @p0, @sMaterial = @p1, @sDescription = @p2, @ixMaterialType = @p3, @ixBaseUnit = @p4, @bTrackSerialNumber = @p5, @bTrackBatchNumber = @p6, @bTrackExpiry = @p7, @nDensity = @p8, @ixDensityUnit = @p9, @nShelflife = @p10, @ixShelflifeUnit = @p11, @nLength = @p12, @ixLengthUnit = @p13, @nWidth = @p14, @ixWidthUnit = @p15, @nHeight = @p16, @ixHeightUnit = @p17, @nWeight = @p18, @ixWeightUnit = @p19, @UserName = @p20", md_vw_materialspost.ixMaterial, md_vw_materialspost.sMaterial, md_vw_materialspost.sDescription, md_vw_materialspost.ixMaterialType, md_vw_materialspost.ixBaseUnit, md_vw_materialspost.bTrackSerialNumber, md_vw_materialspost.bTrackBatchNumber, md_vw_materialspost.bTrackExpiry, md_vw_materialspost.nDensity, md_vw_materialspost.ixDensityUnit, md_vw_materialspost.nShelflife, md_vw_materialspost.ixShelflifeUnit, md_vw_materialspost.nLength, md_vw_materialspost.ixLengthUnit, md_vw_materialspost.nWidth, md_vw_materialspost.ixWidthUnit, md_vw_materialspost.nHeight, md_vw_materialspost.ixHeightUnit, md_vw_materialspost.nWeight, md_vw_materialspost.ixWeightUnit, md_vw_materialspost.UserName);
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
  

