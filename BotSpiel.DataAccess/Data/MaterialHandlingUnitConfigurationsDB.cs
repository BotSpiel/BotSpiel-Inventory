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

    public class MaterialHandlingUnitConfigurationsDB : DbContext
    {

        public MaterialHandlingUnitConfigurationsDB(DbContextOptions<MaterialHandlingUnitConfigurationsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<MaterialHandlingUnitConfigurations> MaterialHandlingUnitConfigurations { get; set; }
		public DbSet<MaterialHandlingUnitConfigurationsPost> MaterialHandlingUnitConfigurationsPost { get; set; }
		public DbSet<Materials> Materials { get; set; }
		public DbSet<MaterialsPost> MaterialsPost { get; set; }
		public DbSet<HandlingUnitTypes> HandlingUnitTypes { get; set; }
		public DbSet<HandlingUnitTypesPost> HandlingUnitTypesPost { get; set; }
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
            modelBuilder.Entity<MaterialHandlingUnitConfigurations>()
                .ToTable("md_vw_MaterialHandlingUnitConfigurations")
                .HasKey(c => new { c.ixMaterialHandlingUnitConfiguration });
            modelBuilder.Entity<MaterialHandlingUnitConfigurationsPost>()
                .ToTable("md_vw_MaterialHandlingUnitConfigurationsPost")
                .HasKey(c => new { c.ixMaterialHandlingUnitConfiguration });
            modelBuilder.Entity<Materials>()
                .ToTable("md_vw_Materials")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<MaterialsPost>()
                .ToTable("md_vw_MaterialsPost")
                .HasKey(c => new { c.ixMaterial });
            modelBuilder.Entity<HandlingUnitTypes>()
                .ToTable("config_vw_HandlingUnitTypes")
                .HasKey(c => new { c.ixHandlingUnitType });
            modelBuilder.Entity<HandlingUnitTypesPost>()
                .ToTable("config_vw_HandlingUnitTypesPost")
                .HasKey(c => new { c.ixHandlingUnitType });
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is MaterialHandlingUnitConfigurationsPost)).ToList())
            {
                var md_vw_materialhandlingunitconfigurationspost = e.Entity as MaterialHandlingUnitConfigurationsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var ixMaterial = cmd.CreateParameter();
                            ixMaterial.ParameterName = "p0";
                            ixMaterial.Value = md_vw_materialhandlingunitconfigurationspost.ixMaterial;
                            var nNestingLevel = cmd.CreateParameter();
                            nNestingLevel.ParameterName = "p1";
                            nNestingLevel.Value = md_vw_materialhandlingunitconfigurationspost.nNestingLevel;
                            var ixHandlingUnitType = cmd.CreateParameter();
                            ixHandlingUnitType.ParameterName = "p2";
                            ixHandlingUnitType.Value = md_vw_materialhandlingunitconfigurationspost.ixHandlingUnitType;
                            var nQuantity = cmd.CreateParameter();
                            nQuantity.ParameterName = "p3";
                            nQuantity.Value = md_vw_materialhandlingunitconfigurationspost.nQuantity;
                            var nLength = cmd.CreateParameter();
                            nLength.ParameterName = "p4";
                            nLength.Value = md_vw_materialhandlingunitconfigurationspost.nLength;
                            var ixLengthUnit = cmd.CreateParameter();
                            ixLengthUnit.ParameterName = "p5";
                            ixLengthUnit.Value = md_vw_materialhandlingunitconfigurationspost.ixLengthUnit;
                            var nWidth = cmd.CreateParameter();
                            nWidth.ParameterName = "p6";
                            nWidth.Value = md_vw_materialhandlingunitconfigurationspost.nWidth;
                            var ixWidthUnit = cmd.CreateParameter();
                            ixWidthUnit.ParameterName = "p7";
                            ixWidthUnit.Value = md_vw_materialhandlingunitconfigurationspost.ixWidthUnit;
                            var nHeight = cmd.CreateParameter();
                            nHeight.ParameterName = "p8";
                            nHeight.Value = md_vw_materialhandlingunitconfigurationspost.nHeight;
                            var ixHeightUnit = cmd.CreateParameter();
                            ixHeightUnit.ParameterName = "p9";
                            ixHeightUnit.Value = md_vw_materialhandlingunitconfigurationspost.ixHeightUnit;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p10";
                            UserName.Value = md_vw_materialhandlingunitconfigurationspost.UserName;

                            var ixMaterialHandlingUnitConfiguration = cmd.CreateParameter();
                            ixMaterialHandlingUnitConfiguration.ParameterName = "p11";
                            ixMaterialHandlingUnitConfiguration.DbType = DbType.Int64;
                            ixMaterialHandlingUnitConfiguration.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.md_sp_CreateMaterialHandlingUnitConfigurations ");
                            sql.Append("@ixMaterial = @p0, ");
                            sql.Append("@nNestingLevel = @p1, ");
                            sql.Append("@ixHandlingUnitType = @p2, ");
                            sql.Append("@nQuantity = @p3, ");
                            if (md_vw_materialhandlingunitconfigurationspost.nLength != null) { sql.Append("@nLength = @p4, "); }  
                            if (md_vw_materialhandlingunitconfigurationspost.ixLengthUnit != null) { sql.Append("@ixLengthUnit = @p5, "); }  
                            if (md_vw_materialhandlingunitconfigurationspost.nWidth != null) { sql.Append("@nWidth = @p6, "); }  
                            if (md_vw_materialhandlingunitconfigurationspost.ixWidthUnit != null) { sql.Append("@ixWidthUnit = @p7, "); }  
                            if (md_vw_materialhandlingunitconfigurationspost.nHeight != null) { sql.Append("@nHeight = @p8, "); }  
                            if (md_vw_materialhandlingunitconfigurationspost.ixHeightUnit != null) { sql.Append("@ixHeightUnit = @p9, "); }  
                            if (md_vw_materialhandlingunitconfigurationspost.UserName != null) { sql.Append("@UserName = @p10, "); }  
                            sql.Append("@ixMaterialHandlingUnitConfiguration = @p11 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(ixMaterial);
                            cmd.Parameters.Add(nNestingLevel);
                            cmd.Parameters.Add(ixHandlingUnitType);
                            cmd.Parameters.Add(nQuantity);
                            if (md_vw_materialhandlingunitconfigurationspost.nLength != null) { cmd.Parameters.Add(nLength); }
                            if (md_vw_materialhandlingunitconfigurationspost.ixLengthUnit != null) { cmd.Parameters.Add(ixLengthUnit); }
                            if (md_vw_materialhandlingunitconfigurationspost.nWidth != null) { cmd.Parameters.Add(nWidth); }
                            if (md_vw_materialhandlingunitconfigurationspost.ixWidthUnit != null) { cmd.Parameters.Add(ixWidthUnit); }
                            if (md_vw_materialhandlingunitconfigurationspost.nHeight != null) { cmd.Parameters.Add(nHeight); }
                            if (md_vw_materialhandlingunitconfigurationspost.ixHeightUnit != null) { cmd.Parameters.Add(ixHeightUnit); }
                            if (md_vw_materialhandlingunitconfigurationspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixMaterialHandlingUnitConfiguration); 
                            cmd.ExecuteNonQuery();
                            md_vw_materialhandlingunitconfigurationspost.ixMaterialHandlingUnitConfiguration = (Int64)ixMaterialHandlingUnitConfiguration.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixMaterialHandlingUnitConfiguration"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_ChangeMaterialHandlingUnitConfigurations @ixMaterialHandlingUnitConfiguration = @p0, @ixMaterial = @p1, @nNestingLevel = @p2, @ixHandlingUnitType = @p3, @nQuantity = @p4, @nLength = @p5, @ixLengthUnit = @p6, @nWidth = @p7, @ixWidthUnit = @p8, @nHeight = @p9, @ixHeightUnit = @p10, @UserName = @p11", md_vw_materialhandlingunitconfigurationspost.ixMaterialHandlingUnitConfiguration, md_vw_materialhandlingunitconfigurationspost.ixMaterial, md_vw_materialhandlingunitconfigurationspost.nNestingLevel, md_vw_materialhandlingunitconfigurationspost.ixHandlingUnitType, md_vw_materialhandlingunitconfigurationspost.nQuantity, md_vw_materialhandlingunitconfigurationspost.nLength, md_vw_materialhandlingunitconfigurationspost.ixLengthUnit, md_vw_materialhandlingunitconfigurationspost.nWidth, md_vw_materialhandlingunitconfigurationspost.ixWidthUnit, md_vw_materialhandlingunitconfigurationspost.nHeight, md_vw_materialhandlingunitconfigurationspost.ixHeightUnit, md_vw_materialhandlingunitconfigurationspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.md_sp_DeleteMaterialHandlingUnitConfigurations @ixMaterialHandlingUnitConfiguration = @p0, @ixMaterial = @p1, @nNestingLevel = @p2, @ixHandlingUnitType = @p3, @nQuantity = @p4, @nLength = @p5, @ixLengthUnit = @p6, @nWidth = @p7, @ixWidthUnit = @p8, @nHeight = @p9, @ixHeightUnit = @p10, @UserName = @p11", md_vw_materialhandlingunitconfigurationspost.ixMaterialHandlingUnitConfiguration, md_vw_materialhandlingunitconfigurationspost.ixMaterial, md_vw_materialhandlingunitconfigurationspost.nNestingLevel, md_vw_materialhandlingunitconfigurationspost.ixHandlingUnitType, md_vw_materialhandlingunitconfigurationspost.nQuantity, md_vw_materialhandlingunitconfigurationspost.nLength, md_vw_materialhandlingunitconfigurationspost.ixLengthUnit, md_vw_materialhandlingunitconfigurationspost.nWidth, md_vw_materialhandlingunitconfigurationspost.ixWidthUnit, md_vw_materialhandlingunitconfigurationspost.nHeight, md_vw_materialhandlingunitconfigurationspost.ixHeightUnit, md_vw_materialhandlingunitconfigurationspost.UserName);
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
  

