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

    public class HandlingUnitsDB : DbContext
    {

        public HandlingUnitsDB(DbContextOptions<HandlingUnitsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<Statuses> Statuses { get; set; }
		public DbSet<StatusesPost> StatusesPost { get; set; }
		public DbSet<MeasurementSystems> MeasurementSystems { get; set; }
		public DbSet<MeasurementSystemsPost> MeasurementSystemsPost { get; set; }
		public DbSet<MeasurementUnitsOf> MeasurementUnitsOf { get; set; }
		public DbSet<MeasurementUnitsOfPost> MeasurementUnitsOfPost { get; set; }
		public DbSet<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
		public DbSet<UnitsOfMeasurementPost> UnitsOfMeasurementPost { get; set; }
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
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is HandlingUnitsPost)).ToList())
            {
                var tx_vw_handlingunitspost = e.Entity as HandlingUnitsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sHandlingUnit = cmd.CreateParameter();
                            sHandlingUnit.ParameterName = "p0";
                            sHandlingUnit.Value = tx_vw_handlingunitspost.sHandlingUnit;
                            var ixHandlingUnitType = cmd.CreateParameter();
                            ixHandlingUnitType.ParameterName = "p1";
                            ixHandlingUnitType.Value = tx_vw_handlingunitspost.ixHandlingUnitType;
                            var ixParentHandlingUnit = cmd.CreateParameter();
                            ixParentHandlingUnit.ParameterName = "p2";
                            ixParentHandlingUnit.Value = tx_vw_handlingunitspost.ixParentHandlingUnit;
                            var ixPackingMaterial = cmd.CreateParameter();
                            ixPackingMaterial.ParameterName = "p3";
                            ixPackingMaterial.Value = tx_vw_handlingunitspost.ixPackingMaterial;
                            var ixMaterialHandlingUnitConfiguration = cmd.CreateParameter();
                            ixMaterialHandlingUnitConfiguration.ParameterName = "p4";
                            ixMaterialHandlingUnitConfiguration.Value = tx_vw_handlingunitspost.ixMaterialHandlingUnitConfiguration;
                            var nLength = cmd.CreateParameter();
                            nLength.ParameterName = "p5";
                            nLength.Value = tx_vw_handlingunitspost.nLength;
                            var ixLengthUnit = cmd.CreateParameter();
                            ixLengthUnit.ParameterName = "p6";
                            ixLengthUnit.Value = tx_vw_handlingunitspost.ixLengthUnit;
                            var nWidth = cmd.CreateParameter();
                            nWidth.ParameterName = "p7";
                            nWidth.Value = tx_vw_handlingunitspost.nWidth;
                            var ixWidthUnit = cmd.CreateParameter();
                            ixWidthUnit.ParameterName = "p8";
                            ixWidthUnit.Value = tx_vw_handlingunitspost.ixWidthUnit;
                            var nHeight = cmd.CreateParameter();
                            nHeight.ParameterName = "p9";
                            nHeight.Value = tx_vw_handlingunitspost.nHeight;
                            var ixHeightUnit = cmd.CreateParameter();
                            ixHeightUnit.ParameterName = "p10";
                            ixHeightUnit.Value = tx_vw_handlingunitspost.ixHeightUnit;
                            var nWeight = cmd.CreateParameter();
                            nWeight.ParameterName = "p11";
                            nWeight.Value = tx_vw_handlingunitspost.nWeight;
                            var ixWeightUnit = cmd.CreateParameter();
                            ixWeightUnit.ParameterName = "p12";
                            ixWeightUnit.Value = tx_vw_handlingunitspost.ixWeightUnit;
                            var ixStatus = cmd.CreateParameter();
                            ixStatus.ParameterName = "p13";
                            ixStatus.Value = tx_vw_handlingunitspost.ixStatus;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p14";
                            UserName.Value = tx_vw_handlingunitspost.UserName;

                            var ixHandlingUnit = cmd.CreateParameter();
                            ixHandlingUnit.ParameterName = "p15";
                            ixHandlingUnit.DbType = DbType.Int64;
                            ixHandlingUnit.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreateHandlingUnits ");
                            sql.Append("@sHandlingUnit = @p0, ");
                            sql.Append("@ixHandlingUnitType = @p1, ");
                            if (tx_vw_handlingunitspost.ixParentHandlingUnit != null) { sql.Append("@ixParentHandlingUnit = @p2, "); }  
                            if (tx_vw_handlingunitspost.ixPackingMaterial != null) { sql.Append("@ixPackingMaterial = @p3, "); }  
                            if (tx_vw_handlingunitspost.ixMaterialHandlingUnitConfiguration != null) { sql.Append("@ixMaterialHandlingUnitConfiguration = @p4, "); }  
                            if (tx_vw_handlingunitspost.nLength != null) { sql.Append("@nLength = @p5, "); }  
                            if (tx_vw_handlingunitspost.ixLengthUnit != null) { sql.Append("@ixLengthUnit = @p6, "); }  
                            if (tx_vw_handlingunitspost.nWidth != null) { sql.Append("@nWidth = @p7, "); }  
                            if (tx_vw_handlingunitspost.ixWidthUnit != null) { sql.Append("@ixWidthUnit = @p8, "); }  
                            if (tx_vw_handlingunitspost.nHeight != null) { sql.Append("@nHeight = @p9, "); }  
                            if (tx_vw_handlingunitspost.ixHeightUnit != null) { sql.Append("@ixHeightUnit = @p10, "); }  
                            if (tx_vw_handlingunitspost.nWeight != null) { sql.Append("@nWeight = @p11, "); }  
                            if (tx_vw_handlingunitspost.ixWeightUnit != null) { sql.Append("@ixWeightUnit = @p12, "); }  
                            if (tx_vw_handlingunitspost.ixStatus != null) { sql.Append("@ixStatus = @p13, "); }  
                            if (tx_vw_handlingunitspost.UserName != null) { sql.Append("@UserName = @p14, "); }  
                            sql.Append("@ixHandlingUnit = @p15 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sHandlingUnit);
                            cmd.Parameters.Add(ixHandlingUnitType);
                            if (tx_vw_handlingunitspost.ixParentHandlingUnit != null) { cmd.Parameters.Add(ixParentHandlingUnit); }
                            if (tx_vw_handlingunitspost.ixPackingMaterial != null) { cmd.Parameters.Add(ixPackingMaterial); }
                            if (tx_vw_handlingunitspost.ixMaterialHandlingUnitConfiguration != null) { cmd.Parameters.Add(ixMaterialHandlingUnitConfiguration); }
                            if (tx_vw_handlingunitspost.nLength != null) { cmd.Parameters.Add(nLength); }
                            if (tx_vw_handlingunitspost.ixLengthUnit != null) { cmd.Parameters.Add(ixLengthUnit); }
                            if (tx_vw_handlingunitspost.nWidth != null) { cmd.Parameters.Add(nWidth); }
                            if (tx_vw_handlingunitspost.ixWidthUnit != null) { cmd.Parameters.Add(ixWidthUnit); }
                            if (tx_vw_handlingunitspost.nHeight != null) { cmd.Parameters.Add(nHeight); }
                            if (tx_vw_handlingunitspost.ixHeightUnit != null) { cmd.Parameters.Add(ixHeightUnit); }
                            if (tx_vw_handlingunitspost.nWeight != null) { cmd.Parameters.Add(nWeight); }
                            if (tx_vw_handlingunitspost.ixWeightUnit != null) { cmd.Parameters.Add(ixWeightUnit); }
                            if (tx_vw_handlingunitspost.ixStatus != null) { cmd.Parameters.Add(ixStatus); }
                            if (tx_vw_handlingunitspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixHandlingUnit); 
                            cmd.ExecuteNonQuery();
                            tx_vw_handlingunitspost.ixHandlingUnit = (Int64)ixHandlingUnit.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixHandlingUnit"), false);
						e.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangeHandlingUnits @ixHandlingUnit = @p0, @sHandlingUnit = @p1, @ixHandlingUnitType = @p2, @ixParentHandlingUnit = @p3, @ixPackingMaterial = @p4, @ixMaterialHandlingUnitConfiguration = @p5, @nLength = @p6, @ixLengthUnit = @p7, @nWidth = @p8, @ixWidthUnit = @p9, @nHeight = @p10, @ixHeightUnit = @p11, @nWeight = @p12, @ixWeightUnit = @p13, @ixStatus = @p14, @UserName = @p15", tx_vw_handlingunitspost.ixHandlingUnit, tx_vw_handlingunitspost.sHandlingUnit, tx_vw_handlingunitspost.ixHandlingUnitType, tx_vw_handlingunitspost.ixParentHandlingUnit, tx_vw_handlingunitspost.ixPackingMaterial, tx_vw_handlingunitspost.ixMaterialHandlingUnitConfiguration, tx_vw_handlingunitspost.nLength, tx_vw_handlingunitspost.ixLengthUnit, tx_vw_handlingunitspost.nWidth, tx_vw_handlingunitspost.ixWidthUnit, tx_vw_handlingunitspost.nHeight, tx_vw_handlingunitspost.ixHeightUnit, tx_vw_handlingunitspost.nWeight, tx_vw_handlingunitspost.ixWeightUnit, tx_vw_handlingunitspost.ixStatus, tx_vw_handlingunitspost.UserName);
                        e.State = EntityState.Unchanged;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeleteHandlingUnits @ixHandlingUnit = @p0, @sHandlingUnit = @p1, @ixHandlingUnitType = @p2, @ixParentHandlingUnit = @p3, @ixPackingMaterial = @p4, @ixMaterialHandlingUnitConfiguration = @p5, @nLength = @p6, @ixLengthUnit = @p7, @nWidth = @p8, @ixWidthUnit = @p9, @nHeight = @p10, @ixHeightUnit = @p11, @nWeight = @p12, @ixWeightUnit = @p13, @ixStatus = @p14, @UserName = @p15", tx_vw_handlingunitspost.ixHandlingUnit, tx_vw_handlingunitspost.sHandlingUnit, tx_vw_handlingunitspost.ixHandlingUnitType, tx_vw_handlingunitspost.ixParentHandlingUnit, tx_vw_handlingunitspost.ixPackingMaterial, tx_vw_handlingunitspost.ixMaterialHandlingUnitConfiguration, tx_vw_handlingunitspost.nLength, tx_vw_handlingunitspost.ixLengthUnit, tx_vw_handlingunitspost.nWidth, tx_vw_handlingunitspost.ixWidthUnit, tx_vw_handlingunitspost.nHeight, tx_vw_handlingunitspost.ixHeightUnit, tx_vw_handlingunitspost.nWeight, tx_vw_handlingunitspost.ixWeightUnit, tx_vw_handlingunitspost.ixStatus, tx_vw_handlingunitspost.UserName);
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
  

